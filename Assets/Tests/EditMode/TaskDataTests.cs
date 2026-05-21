using NUnit.Framework;

public class TaskDataTests
{
    [Test]
    public void NewTask_StartsInactive()
    {
        var task = new TaskData();
        Assert.IsFalse(task.IsActive);
        Assert.IsFalse(task.IsCompleted);
        Assert.IsFalse(task.IsFailed);
    }

    [Test]
    public void NewTask_HasDefaultPoints()
    {
        var task = new TaskData();
        Assert.AreEqual(Constants.PointsPerTask, task.PointsReward);
    }
}
