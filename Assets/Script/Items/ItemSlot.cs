
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int maxStackSize = 99;
    [SerializeField] public GameObject SelectedPanel;

    public bool isItemSelected;
    private InventoryManager inventoryManager;

    public ItemDetails ItemDetails => itemDetails;
    private ItemDetails itemDetails;

    public void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();

        UpdateUI();
    }

    public void AddItem(ItemDetails item)
    {
        itemDetails = item;
        itemName = item.ItemName;
        itemIcon = item.ItemIcon;

        AddQuantity(item.Quantity);
    }

    public bool CanStackWith(string otherItemName)
    {
        return isFull && itemName == otherItemName
            && itemQuantity < maxStackSize;
    }

    public void AddQuantity(int amount)
    {
        itemQuantity += amount;

        if (itemQuantity > maxStackSize)
        {
            itemQuantity = maxStackSize;
        }

        isFull = itemQuantity > 0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        bool hasItem = itemQuantity > 0;

        if (itemImage != null)
        {
            itemImage.sprite = itemIcon;
            itemImage.enabled = hasItem && itemIcon != null;
        }

        if (quantityText != null)
        {
            quantityText.text = hasItem ? itemQuantity.ToString() : "";
            quantityText.enabled = hasItem;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
    }

    public void RightClick()
    {
        Debug.Log("Right Click!");
    }

    public void LeftClick()
    {
        if (!isFull) return;

        inventoryManager.DeselectAllSlot();

        SelectedPanel.SetActive(true);
        isItemSelected = true;

        Debug.Log("Left Click!");
    }
}
