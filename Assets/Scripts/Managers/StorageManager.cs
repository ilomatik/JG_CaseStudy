using System.Collections.Generic;
using System.IO;
using System.Linq;
using Scripts;
using UnityEngine;

namespace Managers
{
    public class StorageManager : MonoBehaviour
    {
        public PlayerData _player;
        
        public void CreatePlayerData(string playerName)
        {
            List<ChipData> playerChips = new List<ChipData>();
            
            _player = new PlayerData(playerName, playerChips, 0, 0, 0, System.DateTime.Now.ToString());
            
            string json = JsonUtility.ToJson(_player);
            SavePlayerData(json);
        }

        private void SavePlayerData(string json)
        {
            string path = Application.persistentDataPath + "/playerData.json";
            File.WriteAllText(path, json);
            Debug.Log("Player data saved to: " + path);
        }
        
        public bool LoadPlayerData()
        {
            string path = Application.persistentDataPath + "/playerData.json";
            
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _player = JsonUtility.FromJson<PlayerData>(json); 
                ChipDataListWrapper wrapper = JsonUtility.FromJson<ChipDataListWrapper>("{\"chips\":" + json + "}");

                Debug.Log("Player data loaded: " + _player._playerName);
                
                // Çip verilerini yazdırma
                foreach (ChipData chip in wrapper.chips)
                {
                    Debug.Log($"Chip Type: {chip.chipType}, Amount: {chip.amount}");
                }
                
                return true;
            }
            else
            {
                Debug.Log("No saved player data found.");
                return false;
            }
        }
        
        public void UpdateChipAmount(string chipType, int amountChange)
        {
            ChipData chipData = _player._chips.FirstOrDefault(chip => chip.chipType == chipType);
            
            if (chipData != null)
            {
                chipData.SetAmount(amountChange);
            }
            else
            {
                ChipData newChip = new ChipData(chipType, amountChange);
                _player.AddChip(newChip);
            }

            string json = JsonUtility.ToJson(_player);
            
            SavePlayerData(json);
        }
        
        [ContextMenu("Clear Player Data")]
        public void ClearPlayerData()
        {
            string path = Application.persistentDataPath + "/playerData.json";
            File.Delete(path);
            Debug.Log("Player data deleted.");
        }

        private void OnApplicationQuit()
        {
            string json = JsonUtility.ToJson(_player);
            SavePlayerData(json);
        }
    }
    
    [System.Serializable]
    public class ChipDataListWrapper
    {
        public List<ChipData> chips;
    }
}