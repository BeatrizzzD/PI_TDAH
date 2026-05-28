using NUnit.Framework;

public class EventManagerLogicTests
{
    [Test]
    public void GenerateEvents_CreatesCorrectNumberOfEvents()
    {
        var events = EventManagerLogic.GenerateEvents();
        Assert.GreaterOrEqual(events.Count, Constants.MinEvents);
        Assert.LessOrEqual(events.Count, Constants.MaxEvents);
    }

    [Test]
    public void GenerateEvents_AllEventsWithinPhaseDuration()
    {
        var events = EventManagerLogic.GenerateEvents();
        foreach (var e in events)
            Assert.Less(e.TriggerTime + e.Duration, Constants.PhaseDuration);
    }

    [Test]
    public void GenerateEvents_NoOverlappingEvents()
    {
        var events = EventManagerLogic.GenerateEvents();
        for (int i = 1; i < events.Count; i++)
            Assert.GreaterOrEqual(events[i].TriggerTime, events[i - 1].TriggerTime + events[i - 1].Duration);
    }
}
