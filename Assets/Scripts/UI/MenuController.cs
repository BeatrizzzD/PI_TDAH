using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button phase1Button;
    [SerializeField] private Button phase2Button;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameSceneManager sceneManager;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private SettingsPanel settingsPanel;

    private void Start()
    {
        phase1Button.onClick.AddListener(() => sceneManager.GoToPhase1());
        phase2Button.onClick.AddListener(() => sceneManager.GoToPhase2());
        creditsButton.onClick.AddListener(() => creditsPanel.SetActive(true));
        settingsButton.onClick.AddListener(() => settingsPanel.Toggle());
    }
}
