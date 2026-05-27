using UnityEngine;
using System;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private ChoiceMinigame choiceMinigame;
    [SerializeField] private EmailMinigame emailMinigame;
    [SerializeField] private DeliveryMinigame deliveryMinigame;
    [SerializeField] private GameObject overlay;

    public void StartMinigame(TaskData task, Action<bool> onComplete)
    {
        overlay.SetActive(true);
        GetMinigame(task.Type).StartMinigame(task, result =>
        {
            overlay.SetActive(false);
            onComplete(result);
        });
    }

    public void InterruptCurrentMinigame()
    {
        if (choiceMinigame.isActiveAndEnabled)        choiceMinigame.Interrupt();
        else if (emailMinigame.isActiveAndEnabled)    emailMinigame.Interrupt();
        else if (deliveryMinigame.isActiveAndEnabled) deliveryMinigame.Interrupt();
    }

    private MinigameBase GetMinigame(TaskType type) => type switch
    {
        TaskType.Choice   => choiceMinigame,
        TaskType.Email    => emailMinigame,
        TaskType.Delivery => deliveryMinigame,
        _                 => choiceMinigame
    };
}
