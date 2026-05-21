using NUnit.Framework;

public class GameStateTests
{
    [Test]
    public void NewGameState_StartsWithZeroPoints()
    {
        var state = new GameState();
        Assert.AreEqual(0, state.CurrentPoints);
        Assert.AreEqual(0, state.TasksCompleted);
    }

    [Test]
    public void IsVictory_WhenFiveOrMoreTasksCompleted()
    {
        var result = new PhaseResult { TasksCompleted = 5 };
        Assert.IsTrue(result.IsVictory);
    }

    [Test]
    public void IsDefeat_WhenFewerThanFiveTasksCompleted()
    {
        var result = new PhaseResult { TasksCompleted = 4 };
        Assert.IsFalse(result.IsVictory);
    }
}
