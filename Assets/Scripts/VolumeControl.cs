using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioSource audioSource;  // Tham chiếu tới AudioManager
    public Button volumeButton;      // Nút mở Slider
    public Slider volumeSlider;      // Thanh điều chỉnh âm lượng
    private bool isSliderVisible = false;

    void Start()
    {
        // Ẩn Slider lúc đầu
        volumeSlider.gameObject.SetActive(false);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Load âm lượng trước đó nếu có
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume", 1.0f);
        SetVolume(volumeSlider.value);

        // Bắt sự kiện click Button
        volumeButton.onClick.AddListener(ToggleSlider);
    }

    void ToggleSlider()
    {
        isSliderVisible = !isSliderVisible;
        volumeSlider.gameObject.SetActive(isSliderVisible);
    }

    void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("GameVolume", volume); // Lưu lại âm lượng
        }
    }
}
