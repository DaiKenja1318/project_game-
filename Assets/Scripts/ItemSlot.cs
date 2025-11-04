using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [Header("References")]
    public Image itemImage;       // Gán Image của ô
    public TMP_Text countText;    // Gán Text (TMP)

    [Header("Item Info")]
    public string itemName = "";
    public int count = 0;

    public bool IsEmpty => string.IsNullOrEmpty(itemName);

    public void SetItem(ItemData item)
    {
        itemName = item.itemName;
        count = 1;
        itemImage.sprite = item.icon;
        itemImage.enabled = true;
        countText.text = "x1";
    }

    public void AddCount()
    {
        count++;
        countText.text = "x" + count;
    }

    public void ClearSlot()
    {
        itemName = "";
        count = 0;
        itemImage.sprite = null;
        itemImage.enabled = false;
        countText.text = "";
    }
}
