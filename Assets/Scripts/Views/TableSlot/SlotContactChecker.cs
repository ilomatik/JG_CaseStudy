using UnityEngine;
using Views;

public class SlotContactChecker : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Chip")) return;
        
        ChipView chip = other.transform.GetComponent<ChipView>();

        if (chip == null) return;
            
        int value = chip.Value;
        Debug.Log($"Chip value: {value}");
    }
}