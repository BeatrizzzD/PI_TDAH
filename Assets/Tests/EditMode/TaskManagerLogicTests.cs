using NUnit.Framework;

public class TaskManagerLogicTests
{
    [Test]
    public void GenerateTasks_Creates10Tasks()
    {
        var tasks = TaskManagerLogic.GenerateTasks(isPhase2: false);
        Assert.AreEqual(Constants.TotalTasks, tasks.Count);
    }

    [Test]
    public void GenerateTasks_SetsSpawnTimeEvery30Seconds()
    {
        var tasks = TaskManagerLogic.GenerateTasks(isPhase2: false);
        Assert.AreEqual(0f, tasks[0].SpawnTime, 0.001f);
        Assert.AreEqual(30f, tasks[1].SpawnTime, 0.001f);
        Assert.AreEqual(60f, tasks[2].SpawnTime, 0.001f);
    }

    [Test]
    public void GenerateTasks_AllTasksHaveCorrectPoints()
    {
        var tasks = TaskManagerLogic.GenerateTasks(isPhase2: false);
        foreach (var task in tasks)
            Assert.AreEqual(Constants.PointsPerTask, task.PointsReward);
    }
}
