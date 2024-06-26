namespace ProjectManagementSystem.Modules.Keeper;

using ProjectManagementSystem.Modules.HumanResources;
using Task = ProjectManagementSystem.Modules.Task.Task;
using System.Text.Json;

public class DataKeeper
{
    private const string EmployeesFileName = "employess.json";
    private string _employeesFile;

    private const string TasksFileName = "tasks.json";
    private string _tasksFile;
    
    private const string HistoryFileName = "history.json";
    private string _historyFile;

    private List<Task> _tasks;
    private List<string> _history;

    public DataKeeper()
    {
        FileStream employeesFileStream = new FileStream(EmployeesFileName, FileMode.OpenOrCreate);
        _employeesFile = employeesFileStream.Name;
        employeesFileStream.Close();

        FileStream tasksFileStream = new FileStream(TasksFileName, FileMode.OpenOrCreate);
        _tasksFile = tasksFileStream.Name;
        tasksFileStream.Close();

        FileStream historyFileStream = new FileStream(HistoryFileName, FileMode.OpenOrCreate);
        _historyFile = historyFileStream.Name;
        historyFileStream.Close();

        _history = TryGetHistory();

        if (_history == null)
            _history = new List<string>();
    }

    public void UpdateTasksList(List<Task> tasks)
    {
        _tasks = tasks;
        SaveTasks();
    }
    
    public void SaveEmployees(List<EmployeeProfile> profiles)
    {
        ConvertedEmployeeData convertedEmployeeData = DataConverter.ConvertEmployeeData(profiles);
        
        string jsonString = JsonSerializer.Serialize(convertedEmployeeData);
        File.WriteAllText(_employeesFile, jsonString);
    }

    public List<EmployeeProfile> TryGetEmployees()
    {
        string jsonString = File.ReadAllText(_employeesFile);
            
        if (!string.IsNullOrEmpty(jsonString))
        {
            ConvertedEmployeeData? convertedEmployeeData =
                JsonSerializer.Deserialize<ConvertedEmployeeData>(jsonString);

            List<EmployeeProfile> profiles = convertedEmployeeData.ConvertedProfiles;

            return profiles;
        }
        
        return null;
    }
    
    public void SaveTasks()
    {
        ConvertedTaskData convertedTaskData = DataConverter.ConvertTaskData(_tasks);
        
        string jsonString = JsonSerializer.Serialize(convertedTaskData);
        File.WriteAllText(_tasksFile, jsonString);
    }

    public List<Task> TryGetTasks()
    {
        string jsonString = File.ReadAllText(_tasksFile);
            
        if (!string.IsNullOrEmpty(jsonString))
        {
            ConvertedTaskData? convertedTasksData =
                JsonSerializer.Deserialize<ConvertedTaskData>(jsonString);

            List<Task> tasks = convertedTasksData.ConvertedTasks;

            return tasks;
        }
        
        return null;
    }

    public void AddStringToHistory(string line)
    {
        _history.Add(line);
        SaveHistory(_history);
    }
    
    public void SaveHistory(List<string> history)
    {
        ConvertedHistoryData convertedHistoryData = DataConverter.ConvertHistoryData(history);
        
        string jsonString = JsonSerializer.Serialize(convertedHistoryData);
        File.WriteAllText(_historyFile, jsonString);
    }

    public List<string> TryGetHistory()
    {
        string jsonString = File.ReadAllText(_historyFile);
            
        if (!string.IsNullOrEmpty(jsonString))
        {
            ConvertedHistoryData? convertedHistoryData =
                JsonSerializer.Deserialize<ConvertedHistoryData>(jsonString);

            List<string> history = convertedHistoryData.ConvertedHistory;

            return history;
        }
        
        return null;
    }
}