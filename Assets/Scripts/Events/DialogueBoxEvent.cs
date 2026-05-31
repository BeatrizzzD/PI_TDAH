using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxEvent : GameEventBase
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Button closeButton;

    protected override void OnEventStart()
    {
        dialoguePanel.SetActive(true);
        closeButton.onClick.AddListener(OnCloseClicked);
    }

    private void OnCloseClicked()
    {
        EndEvent();
    }

    protected override void OnEventEnd()
    {
        dialoguePanel.SetActive(false);
        closeButton.onClick.RemoveListener(OnCloseClicked);
    }
}
