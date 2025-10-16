using UnityEngine;
using TMPro; // Thêm thư viện TextMeshPro

// Gắn script này vào GameObject GameManager (hoặc Player)
public class PlayerDataController : MonoBehaviour
{
    // Biến để lưu trữ số tiền (đã có)
    public int currentCoins = 1000; 

    // MẢNG LƯU TỒN KHO (Giữ nguyên như trước)
    private int[] potionCounts = new int[4] { 0, 0, 0, 0 }; 

    // BIẾN MỚI: Tham chiếu đến Text hiển thị số tiền
    [Header("UI References")]
    public TextMeshProUGUI coinTextDisplay; // Kéo Text (TMP) hiển thị 100 vào đây

    private void Start()
    {
        // Cập nhật UI ngay khi game bắt đầu để hiển thị số tiền ban đầu
        UpdateCoinDisplay();
    }

    // PHƯƠNG THỨC MỚI: Cập nhật hiển thị số tiền lên UI
    public void UpdateCoinDisplay()
    {
        if (coinTextDisplay != null)
        {
            // Hiển thị giá trị currentCoins lên Text
            coinTextDisplay.text = currentCoins.ToString();
        }
        else
        {
            Debug.LogError("Chưa gán coinTextDisplay trong PlayerDataController!");
        }
    }

    // --------------------------------------------------------------------------------
    // Các phương thức cũ (DeductCoins, AddPotion, GetPotionCount)

    // Sửa lại phương thức trừ tiền để gọi UpdateCoinDisplay() sau khi trừ.
    public bool DeductCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            
            // GỌI HÀM CẬP NHẬT UI SAU KHI TIỀN BỊ TRỪ
            UpdateCoinDisplay(); 
            
            return true; 
        }
        return false; 
    }
    
    // ... Giữ nguyên các hàm AddPotion và GetPotionCount ...
    public void AddPotion(int id, int amount)
    {
        if (id >= 0 && id < potionCounts.Length)
        {
            potionCounts[id] += amount;
        }
    }
    public int GetPotionCount(int id)
    {
        if (id >= 0 && id < potionCounts.Length)
        {
            return potionCounts[id];
        }
        return 0;
    }
}