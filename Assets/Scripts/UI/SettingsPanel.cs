using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    private const string VolumeKey = "MasterVolume";

    [SerializeField] private GameObject panelObject;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(VolumeKey, 1f);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        closeButton.onClick.AddListener(() => panelObject.SetActive(false));
    }

    private void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(VolumeKey, value);
        AudioListener.volume = value;
    }

    public void Toggle()
    {
        panelObject.SetActive(!panelObject.activeSelf);
    }

    public static float GetSavedVolume() => PlayerPrefs.GetFloat(VolumeKey, 1f);
}