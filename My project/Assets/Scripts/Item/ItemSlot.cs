
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outline;
    
    public Inventory inventory;

    public int index;
    public int quantity;

    private void OnEnable()
    {
        if(outline != null)
        outline.enabled = false;
    }

    
    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.ItemIcon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnClick()
    {
        Debug.Log("ItemSlot Clicked");
        inventory.SelectItem(this);
    }
}
