using System;
using System.Collections;
using UnityEngine;

public abstract class GameEventBase : MonoBehaviour
{
    protected EventData currentEvent;
    private Action onComplete;

    public void StartEvent(EventData data, Action callback)
    {
        currentEvent = data;
        onComplete = callback;
        OnEventStart();
        StartCoroutine(AutoComplete(data.Duration));
    }

    protected abstract void OnEventStart();
    protected virtual void OnEventEnd() { }

    private IEnumerator AutoComplete(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndEvent();
    }

    protected void EndEvent()
    {
        OnEventEnd();
        onComplete?.Invoke();
    }
}
