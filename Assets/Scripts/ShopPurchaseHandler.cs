using UnityEngine;
using TMPro; // BẮT BUỘC: Thư viện cho TextMeshPro
using UnityEngine.UI; // Giữ lại nếu bạn cần Button hoặc các UI cổ điển khác

// Script này được gán vào MỖI NÚT "MUA" (Múa)
public class ShopPurchaseHandler : MonoBehaviour
{
    // --- Các biến cần gán trong Inspector ---

    [Header("Cấu hình Thuốc")]
    [Tooltip("Chi phí để mua 1 đơn vị thuốc.")]
    public int itemCost = 200;

    [Tooltip("ID của loại thuốc này (ví dụ: 0=Đỏ, 1=Xanh, 2=Lá, 3=Cam).")]
    public int potionTypeID; 

    // --- Tham chiếu UI & Dữ liệu ---

    [Header("Tham chiếu")]
    [Tooltip("Kéo TextMeshProUGUI (số lượng) từ panel bên trái vào đây.")]
    public TextMeshProUGUI quantityText; 

    [Tooltip("Kéo GameObject có script PlayerDataController vào đây.")]
    public PlayerDataController playerData; 
    
    // ------------------------------------------------------------------

    // Phương thức chính: Được gọi khi người chơi nhấn nút "Mua"
    public void AttemptPurchase()
    {
        // 1. Kiểm tra tham chiếu Player Data
        if (playerData == null)
        {
            Debug.LogError("LỖI: Chưa gán tham chiếu 'Player Data' trên nút " + gameObject.name + "!");
            return;
        }

        // 2. Cố gắng trừ tiền (Hàm DeductCoins sẽ kiểm tra đủ tiền hay không)
        if (playerData.DeductCoins(itemCost))
        {
            // Trừ tiền thành công

            // 3. Thêm thuốc vào kho
            playerData.AddPotion(potionTypeID, 1);
            
            // 4. Cập nhật hiển thị số lượng trên UI
            UpdateQuantityUI();

            Debug.Log($"[MUA HÀNG]: Mua thành công Potion ID {potionTypeID}. Tiền còn lại: {playerData.currentCoins}");
            
            // TODO: (Tùy chọn) Thêm hiệu ứng âm thanh/hình ảnh mua hàng thành công
        }
        else
        {
            // Không đủ tiền
            Debug.Log($"[MUA HÀNG]: Không đủ {itemCost} đồng để mua Potion ID {potionTypeID}. Bạn chỉ có {playerData.currentCoins} đồng.");
            
            // TODO: (Tùy chọn) Hiện thông báo "Không đủ tiền" trên màn hình.
        }
    }

    // Cập nhật giá trị hiển thị trên UI
    private void UpdateQuantityUI()
    {
        if (quantityText != null)
        {
            // Lấy số lượng mới nhất từ Player Data
            int currentCount = playerData.GetPotionCount(potionTypeID);
            
            // Gán giá trị mới vào Text (đã +1)
            quantityText.text = currentCount.ToString(); 
        }
        else
        {
            Debug.LogError("LỖI: Chưa gán tham chiếu 'Quantity Text' trên nút " + gameObject.name + "!");
        }
    }

    /* * Lưu ý quan trọng:
     * Script PlayerDataController.cs cũng phải được đảm bảo đã tạo
     * và được gắn vào một GameObject trong scene.
     */
}