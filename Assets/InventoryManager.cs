using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public Image icon;
        public TMP_Text quantityText;
        public int quantity = 0;
        public Sprite itemSprite;
    }

    public List<InventorySlot> slots;

    public void AddItem(Sprite itemSprite)
    {
        // Nếu item đã có, tăng số lượng
        foreach (var slot in slots)
        {
            if (slot.itemSprite == itemSprite)
            {
                slot.quantity++;
                slot.quantityText.text = "x" + slot.quantity;
                return;
            }
        }

        // Nếu item chưa có, thêm vào slot trống (chưa có itemSprite)
        foreach (var slot in slots)
        {
            if (slot.itemSprite == null)
            {
                slot.itemSprite = itemSprite;
                slot.icon.sprite = itemSprite;
                slot.icon.enabled = true; // đảm bảo icon hiện
                slot.quantity = 1;
                slot.quantityText.text = "x1";
                return;
            }
        }

        Debug.LogWarning("❗ Không còn ô trống trong túi đồ!");
    }
}
