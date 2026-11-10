
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, GrabableItem
{
    InputAction inventoryAction;

    [Header("Inventory Settings")]
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private int slotCount = 16;

    private bool InventoryActive;

    public ItemSlot[] itemSlot;

    [Header("Item Detail Panels")]
    [SerializeField] private ItemDetailsPanel hoverDetailsPanel;
    [SerializeField] private SelectedItemPanel selectedItemPanel;


    void Start()
    {
        CreateSlots();

        InventoryCanvas.SetActive(false);

        inventoryAction = InputSystem.actions.FindAction("Inventory");

        InventoryActive = false;

        // Hide detail panels initially
        if (hoverDetailsPanel != null)
        {
            hoverDetailsPanel.gameObject.SetActive(false);
        }

        if (selectedItemPanel != null)
        {
            selectedItemPanel.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if (inventoryAction.triggered && InventoryCanvas != null)
        {
            InventoryActive = !InventoryActive;

            InventoryCanvas.SetActive(InventoryActive);

            Time.timeScale = InventoryActive ? 0f : 1f;

            Debug.Log(InventoryActive
                ? "Inventory is active"
                : "Inventory is deactivated");
        }
    }


    // CREATE INVENTORY SLOTS
    private void CreateSlots()
    {
        ItemSlot slotPrefab = Resources.Load<ItemSlot>("ItemSlot");

        if (slotPrefab == null)
        {
            Debug.LogError("ItemSlot prefab was not found in a Resources folder.", this);
            itemSlot = new ItemSlot[0];
            return;
        }

        Transform parent = InventoryCanvas.transform;

        GridLayoutGroup grid = InventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);

        if (grid != null)
        {
            parent = grid.transform;
        }

        itemSlot = new ItemSlot[slotCount];

        for (int i = 0; i < itemSlot.Length; i++)
        {
            // Create slot
            itemSlot[i] = Instantiate(slotPrefab, parent);

            // Connect hover and selected panels
            itemSlot[i].SetupPanels(
                hoverDetailsPanel,
                selectedItemPanel
            );
        }
    }


    public void AddItem(ItemDetails item)
    {
        Debug.Log("Item added to inventory: " + item.ItemName +
                  " Quantity: " + item.Quantity);

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogWarning("Inventory has no slots!");
            return;
        }

        // Stack same items
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].CanStackWith(item.ItemName))
            {
                itemSlot[i].AddQuantity(item.Quantity);
                return;
            }
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }

        Debug.LogWarning("Inventory is full, '" + item.ItemName + "' was lost.");
    }


    public void DeselectAllSlot()
    {
        foreach (ItemSlot slot in itemSlot)
        {
            if (slot == null) continue;

            slot.isItemSelected = false;

            if (slot.SelectedPanel != null)
            {
                slot.SelectedPanel.SetActive(false);
            }
        }
    }
}
