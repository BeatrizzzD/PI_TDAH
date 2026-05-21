using NUnit.Framework;

public class ConstantsTests
{
    [Test]
    public void PhaseDuration_Is300Seconds()
    {
        Assert.AreEqual(300f, Constants.PhaseDuration);
    }

    [Test]
    public void TasksToWin_IsHalfOfTotalTasks()
    {
        Assert.AreEqual(Constants.TotalTasks / 2, Constants.TasksToWin);
    }

    [Test]
    public void TotalPoints_Is1000()
    {
        Assert.AreEqual(1000, Constants.TotalTasks * Constants.PointsPerTask);
    }
}
