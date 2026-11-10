
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button equipButton;

    private ItemSlot selectedSlot;

    private void Start()
    {
        equipButton.onClick.AddListener(EquipItem);
    }

    public void ShowSelectedItem(ItemSlot slot)
    {
        selectedSlot = slot;

        itemIcon.sprite = slot.itemIcon;
        itemIcon.enabled = slot.itemIcon != null;

        UpdateEquipButton();

        gameObject.SetActive(true);
    }

    private void EquipItem()
    {
        if (selectedSlot == null) return;

        if (selectedSlot.isEquipped)
        {
            selectedSlot.SetEquipped(false);
            Debug.Log("Unequipped: " + selectedSlot.itemName);
        }
        else
        {
            selectedSlot.SetEquipped(true);
            Debug.Log("Equipped: " + selectedSlot.itemName);
        }

        UpdateEquipButton();
    }

    private void UpdateEquipButton()
    {
        if (equipButton == null || selectedSlot == null) return;

        var buttonText = equipButton.GetComponentInChildren<TMPro.TMP_Text>();

        if (buttonText != null)
        {
            buttonText.text = selectedSlot.isEquipped
                ? "Unequip"
                : "Equip";
        }
    }
}
