using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Kéo thả bảng Option (ví dụ: một Panel hoặc Canvas) vào biến này trong Inspector
    public GameObject optionsPanel;

    // Tên của Scene chọn Slot Lưu (VD: "SaveSelectScene")
    public string saveSelectSceneName = "SaveSelectScene";

    private void Start()
    {
        // Đảm bảo bảng Option ban đầu bị ẩn khi vào Menu chính
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    // --- Chức năng Nút START ---
    public void StartGame()
    {
        // Hàm này sẽ chuyển sang Scene chọn phần lưu hành trình
        SceneManager.LoadScene(saveSelectSceneName);
    }

    // --- Chức năng Nút OPTION (Để mở/đóng bảng) ---
    // HÀM NÀY ĐƯỢC GIỮ LẠI VÀ SỬ DỤNG CHO CẢ NÚT "OPTION" VÀ NÚT "CLOSE"
    public void ToggleOptionsPanel()
    {
        // Kiểm tra xem bảng Option có tồn tại không
        if (optionsPanel != null)
        {
            // Đảo ngược trạng thái hiện tại (nếu đang ẩn thì hiện, nếu đang hiện thì ẩn)
            bool isActive = optionsPanel.activeSelf;
            optionsPanel.SetActive(!isActive);
        }
    }

    // --- Chức năng Nút QUIT ---
    public void QuitGame()
    {
        // Chức năng thoát game (Chỉ hoạt động khi chạy build game)
        Application.Quit();

        // (Tùy chọn: Dùng cho Editor)
#if UNITY_EDITOR
            Debug.Log("Game Quit!");
            // Nếu bạn dùng phiên bản Unity mới hơn, có thể dùng UnityEditor.EditorApplication.ExitPlaymode();
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}