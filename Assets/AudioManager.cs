using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;   // kéo AudioMixer vào đây
    public Slider volumeSlider;

    void Start()
    {
        // Lấy giá trị đã lưu trước đó (nếu có)
        float savedVolume = PlayerPrefs.GetFloat("volume", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
        
        // Lắng nghe sự kiện thay đổi
        volumeSlider.onValueChanged.AddListener(SetVolume);
        DontDestroyOnLoad(gameObject);

    }

    public void SetVolume(float volume)
    {
        // Dùng công thức log để âm thanh nghe tự nhiên hơn
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
    }
}
