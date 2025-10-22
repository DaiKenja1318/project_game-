using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("References")]
    public ItemSlot[] slots;   // Các ô ItemSort trong PanelInventory

    public void AddItem(ItemData newItem)
    {
        // 1️⃣ Nếu item đã tồn tại trong ô nào đó → cộng dồn
        foreach (var slot in slots)
        {
            if (slot.itemName == newItem.itemName)
            {
                slot.AddCount();
                return;
            }
        }

        // 2️⃣ Nếu chưa có → tìm ô trống đầu tiên
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.SetItem(newItem);
                return;
            }
        }

        Debug.Log("Không còn ô trống trong túi đồ!");
    }
}
