namespace ProjectManagementSystem.Modules.Keeper;

using Task = ProjectManagementSystem.Modules.Task.Task;

public class ConvertedTaskData
{
    public List<Task> ConvertedTasks { get; private set; }
    
    public ConvertedTaskData(List<Task> convertedTasks)
    {
        ConvertedTasks = convertedTasks;
    }
}