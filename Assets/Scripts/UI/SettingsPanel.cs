using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    private const string VolumeKey = "MasterVolume";

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(VolumeKey, 1f);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        gameObject.SetActive(false);
    }

    private void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(VolumeKey, value);
        AudioListener.volume = value;
    }

    public void Toggle() => gameObject.SetActive(!gameObject.activeSelf);

    public static float GetSavedVolume() => PlayerPrefs.GetFloat(VolumeKey, 1f);
}
