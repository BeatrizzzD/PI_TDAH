using UnityEngine;
using System;

public abstract class MinigameBase : MonoBehaviour
{
    protected Action<bool> onComplete;
    protected TaskData taskData;
    protected float timeRemaining;

    public virtual void StartMinigame(TaskData task, Action<bool> callback)
    {
        taskData = task;
        onComplete = callback;
        timeRemaining = task.Duration;
        gameObject.SetActive(true);
        OnMinigameStart();
    }

    protected abstract void OnMinigameStart();

    protected virtual void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f) Complete(false);
    }

    protected void Complete(bool success)
    {
        gameObject.SetActive(false);
        onComplete?.Invoke(success);
    }

    public virtual void Interrupt() => Complete(false);
}
