namespace ProjectManagementSystem.Modules.Keeper;

using ProjectManagementSystem.Modules.HumanResources;
using Task = ProjectManagementSystem.Modules.Task.Task;

public static class DataConverter
{
    public static ConvertedEmployeeData ConvertEmployeeData(List<EmployeeProfile> profiles)
    {
        return new ConvertedEmployeeData(profiles);
    }

    public static ConvertedTaskData ConvertTaskData(List<Task> tasks)
    {
        return new ConvertedTaskData(tasks);
    }
    
    public static ConvertedHistoryData ConvertHistoryData(List<string> historyStrings)
    {
        return new ConvertedHistoryData(historyStrings);
    }
}

