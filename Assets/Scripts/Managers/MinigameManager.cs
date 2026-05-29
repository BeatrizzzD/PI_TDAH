using UnityEngine;
using System;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private ChoiceMinigame choiceMinigame;
    [SerializeField] private EmailMinigame emailMinigame;
    [SerializeField] private DeliveryMinigame deliveryMinigame;
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject background;

    private PlayerController player;

    public void StartMinigame(TaskData task, Action<bool> onComplete)
    {
        overlay.SetActive(true);
        // Delivery roda sobre o mapa visível; Choice/Email escurecem a tela
        background.SetActive(task.Type != TaskType.Delivery);
        SetPlayerInput(false);

        GetMinigame(task.Type).StartMinigame(task, result =>
        {
            overlay.SetActive(false);
            SetPlayerInput(true);
            onComplete(result);
        });
    }

    public void InterruptCurrentMinigame()
    {
        if (choiceMinigame.isActiveAndEnabled)        choiceMinigame.Interrupt();
        else if (emailMinigame.isActiveAndEnabled)    emailMinigame.Interrupt();
        else if (deliveryMinigame.isActiveAndEnabled) deliveryMinigame.Interrupt();
    }

    private void SetPlayerInput(bool enabled)
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
        player?.SetInputEnabled(enabled);
    }

    private MinigameBase GetMinigame(TaskType type) => type switch
    {
        TaskType.Choice   => choiceMinigame,
        TaskType.Email    => emailMinigame,
        TaskType.Delivery => deliveryMinigame,
        _                 => choiceMinigame
    };
}
