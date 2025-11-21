using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public string characterName; // Chiến binh, Cung thủ, Pháp sư
    public string nextSceneName = "MapScene"; // Scene tiếp theo

    public void OnSelectCharacter()
    {
        // Lưu nhân vật đã chọn
        PlayerPrefs.SetString("SelectedCharacter", characterName);
        PlayerPrefs.Save();

        // Chuyển scene
        SceneManager.LoadScene(nextSceneName);
    }
}
