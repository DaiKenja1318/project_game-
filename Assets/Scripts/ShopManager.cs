using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    public InventoryManager inventoryManager;   // Tham chiếu đến InventoryManager
    public ItemData[] shopItems;                // Danh sách vật phẩm trong shop

    [Header("Player Gold")]
    public int playerGold = 1000;               // Số tiền hiện tại của người chơi
    public TMP_Text goldText;                   // Text hiển thị số tiền trên UI

    void Start()
    {
        // Cập nhật hiển thị ban đầu
        UpdateGoldText();

        // Gắn sự kiện OnClick cho các nút Mua
        Button[] buyButtons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buyButtons.Length && i < shopItems.Length; i++)
        {
            int index = i;
            buyButtons[i].onClick.AddListener(() => BuyItem(index));
        }
    }

    public void BuyItem(int index)
    {
        if (index < 0 || index >= shopItems.Length) return;

        ItemData item = shopItems[index];

        // Kiểm tra đủ tiền không
        if (playerGold >= item.cost)
        {
            playerGold -= item.cost;
            UpdateGoldText();

            Debug.Log($"Mua thành công: {item.itemName}, còn lại {playerGold} vàng.");
            inventoryManager.AddItem(item);
        }
        else
        {
            Debug.Log("Không đủ tiền để mua " + item.itemName);
            // (Tùy chọn: bạn có thể thêm hiệu ứng cảnh báo UI ở đây)
        }
    }

    void UpdateGoldText()
    {
        if (goldText != null)
            goldText.text = playerGold.ToString();
    }
}
