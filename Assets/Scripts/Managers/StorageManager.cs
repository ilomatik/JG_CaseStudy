using System.Collections.Generic;
using System.IO;
using System.Linq;
using Constants;
using Scripts;
using UnityEngine;

namespace Managers
{
    public class StorageManager : MonoBehaviour
    {
        public PlayerData _player;

        public void Initialize(string playerName)
        {
            if (!LoadPlayerData())
            {
                CreatePlayerData(playerName);
            }
        }

        private void CreatePlayerData(string playerName)
        {
            List<ChipData> playerChips = new List<ChipData>();
            
            _player = new PlayerData(GetPlayerIdCount(), playerName, playerChips, 0, 0, 0, System.DateTime.Now.ToString());
            
            IncrementPlayerId();
            
            string json = JsonUtility.ToJson(_player);
            SavePlayerData(json);
        }

        private void SavePlayerData(string json)
        {
            string path = Application.persistentDataPath + "/playerData.json";
            File.WriteAllText(path, json);
            Debug.Log("Player data saved to: " + path);
        }

        private bool LoadPlayerData()
        {
            string path = Application.persistentDataPath + "/playerData.json";
            
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                _player = JsonUtility.FromJson<PlayerData>(json); 
                ChipDataListWrapper wrapper = JsonUtility.FromJson<ChipDataListWrapper>("{\"chips\":" + json + "}");

                Debug.Log("Player data loaded: " + _player._playerName);
                
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

        private int GetPlayerIdCount()
        {
            if (PlayerPrefs.HasKey(GameConstant.PLAYER_ID_COUNTER))
            {
                return PlayerPrefs.GetInt(GameConstant.PLAYER_ID_COUNTER);
            }
            else
            {
                PlayerPrefs.SetInt(GameConstant.PLAYER_ID_COUNTER, 0);
                return 0;
            }
        }

        private void IncrementPlayerId()
        {
            int playerId = GetPlayerIdCount();
            playerId++;
            PlayerPrefs.SetInt(GameConstant.PLAYER_ID_COUNTER, playerId);
        }
        
        public void IncrementGamesPlayed()
        {
            _player._gamesPlayed++;
            SavePlayerData(JsonUtility.ToJson(_player));
        }

        public void IncrementGamesWon()
        {
            _player._gamesWon++;
            SavePlayerData(JsonUtility.ToJson(_player));
        }

        public void IncrementGamesLost()
        {
            _player._gamesLost++;
            SavePlayerData(JsonUtility.ToJson(_player));
        }
    }
    
    [System.Serializable]
    public class ChipDataListWrapper
    {
        public List<ChipData> chips;
    }
}