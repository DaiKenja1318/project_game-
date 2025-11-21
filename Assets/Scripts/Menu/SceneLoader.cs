using UnityEngine;
using UnityEngine.SceneManagement; // Quan trọng: Thêm namespace này

public class SceneLoader : MonoBehaviour
{
    // Hàm này được dùng để tải Scene (Bạn đã có sẵn)
    public void LoadSpecificScene(string sceneName)
    {
        // Tải Scene theo tên được truyền vào
        SceneManager.LoadScene(sceneName);
    }

    // ⭐ HÀM MỚI: Dùng để Thoát Game ⭐
    public void QuitGame()
    {
        // Ghi chú: Application.Quit() chỉ hoạt động khi game được Build (exe, apk, v.v.). 
        // Nó KHÔNG hoạt động trong Editor của Unity.
        Application.Quit();

        // Đoạn code dưới đây chỉ dùng để kiểm tra trong Unity Editor.
#if UNITY_EDITOR
            Debug.Log("Game đã được yêu cầu thoát.");
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}