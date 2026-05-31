using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private DialogueBoxEvent dialogueBoxEvent;
    [SerializeField] private ImpulseWalkEvent impulseWalkEvent;
    [SerializeField] private TextBlurEvent textBlurEvent;

    private List<EventData> events;
    private bool isRunning;

    public void Initialize()
    {
        events = EventManagerLogic.GenerateEvents();
        isRunning = true;
    }

    private void Update()
    {
        if (!isRunning || GameManager.Instance?.State == null) return;
        if (GameManager.Instance.State.IsEventActive) return;

        float time = GameManager.Instance.State.TimeElapsed;

        foreach (var evt in events)
        {
            if (!evt.HasFired && time >= evt.TriggerTime)
            {
                TriggerEvent(evt);
                break;
            }
        }
    }

    private void TriggerEvent(EventData evt)
    {
        evt.HasFired = true;
        GameManager.Instance.OnEventStarted(evt);
        GetHandler(evt.Type).StartEvent(evt, () => GameManager.Instance.OnEventEnded());
    }

    private GameEventBase GetHandler(EventType type) => type switch
    {
        EventType.DialogueBoxes => dialogueBoxEvent,
        EventType.ImpulseWalk   => impulseWalkEvent,
        EventType.TextBlur      => textBlurEvent,
        _                       => dialogueBoxEvent
    };
}
