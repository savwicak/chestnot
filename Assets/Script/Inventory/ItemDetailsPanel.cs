
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image itemIcon;

    [Header("Mouse Settings")]
    [SerializeField] private Vector2 mouseOffset = new Vector2(15f, -15f);

    private RectTransform panelRect;
    private RectTransform canvasRect;
    private Canvas canvas;

    private void Awake()
    {
        panelRect = GetComponent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRect = canvas.transform as RectTransform;
        }
    }

    private void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        if (canvas == null || panelRect == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : canvas.worldCamera,
            out Vector2 localMousePosition
        );

        panelRect.localPosition = localMousePosition + mouseOffset;
    }

    public void ShowDetails(ItemSlot slot)
    {
        if (slot.ItemDetails == null) return;

        ItemDetails details = slot.ItemDetails;

        itemNameText.text = details.ItemName;
        descriptionText.text = details.Description;

        itemIcon.sprite = details.ItemIcon;
        itemIcon.enabled = details.ItemIcon != null;

        gameObject.SetActive(true);
    }

    public void HideDetails()
    {
        gameObject.SetActive(false);
    }
}
