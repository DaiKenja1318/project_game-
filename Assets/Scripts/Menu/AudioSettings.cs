using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider volumeSlider;
    private const string VolumeKey = "MasterVolume";

    void Awake()
    {
        // Kiểm tra xem đã có Slider chưa. Nếu chưa có, vẫn phải tải âm lượng đã lưu
        if (volumeSlider == null)
        {
            LoadVolume();
        }
    }

    private void Start()
    {
        // Tải âm lượng đã lưu và cập nhật UI khi Scene khởi động
        LoadVolume();
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }
    }

    private void LoadVolume()
    {
        // 1. Tải mức âm lượng đã lưu (mặc định là 1.0f nếu chưa lưu)
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1.0f);

        // 2. Cập nhật giá trị của Slider UI
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }

        // 3. Áp dụng âm lượng vào hệ thống âm thanh Unity
        AudioListener.volume = savedVolume;
    }

    public void SetMasterVolume(float volume)
    {
        // Áp dụng âm lượng vào AudioListener (ảnh hưởng đến toàn bộ âm thanh)
        AudioListener.volume = volume;

        // Lưu giá trị âm lượng mới lại (từ 0.0f đến 1.0f)
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }
}