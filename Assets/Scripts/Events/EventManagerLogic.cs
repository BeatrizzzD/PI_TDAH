using System.Collections.Generic;

public static class EventManagerLogic
{
    private static readonly EventType[] eventTypes =
        { EventType.DialogueBoxes, EventType.ImpulseWalk, EventType.TextBlur };

    public static List<EventData> GenerateEvents()
    {
        var events = new List<EventData>();
        float slotDuration = Constants.PhaseDuration / Constants.MaxEvents;
        float eventDuration = slotDuration * 0.4f;

        for (int i = 0; i < Constants.MaxEvents; i++)
        {
            float triggerTime = i * slotDuration + slotDuration * 0.1f;
            events.Add(new EventData
            {
                Id = i,
                Type = eventTypes[i % eventTypes.Length],
                TriggerTime = triggerTime,
                Duration = eventDuration
            });
        }

        return events;
    }
}
