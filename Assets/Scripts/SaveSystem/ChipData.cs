using UnityEngine;

namespace Scripts
{
    [System.Serializable]
    public class ChipData
    {
        public string chipType;
        public int    amount;

        // Constructor
        public ChipData(string type, int amount)
        {
            chipType    = type;
            this.amount = amount;
            
            Debug.Log("Chip amount created: " + chipType + " " + amount);
        }
        
        public void SetAmount(int amount)
        {
            this.amount += amount;
            Debug.Log("Chip amount updated: " + chipType + " " + amount);
        }
    }
}