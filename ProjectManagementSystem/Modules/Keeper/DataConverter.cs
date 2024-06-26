using ProjectManagmentSystem.ProjectManagementSystem.Modules.HumanResources;

namespace ProjectManagmentSystem.ProjectManagementSystem.Modules.Keeper;

using Task = global::ProjectManagmentSystem.ProjectManagementSystem.Modules.Task.Task;

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

