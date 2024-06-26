using ProjectManagementSystem.Modules.Keeper;

namespace ProjectManagementSystem.Modules.DataBase;

using Task = ProjectManagementSystem.Modules.Task.Task;
using TaskStatus = ProjectManagementSystem.Modules.Task.TaskStatus;

public class TaskDataBase
{
    private List<Task> _tasks;
    private DataKeeper _dataKeeper;

    public TaskDataBase(DataKeeper dataKeeper)
    {
        _dataKeeper = dataKeeper;
        _tasks = _dataKeeper.TryGetTasks();

        if (_tasks == null)
            _tasks = new List<Task>();
        
        _dataKeeper.UpdateTasksList(_tasks);
    }

    public void Add(Task task)
    {
        _tasks.Add(task);
        _dataKeeper.UpdateTasksList(_tasks);
    }

    public void Remove(Task task)
    {
        _tasks.Remove(task);
        _dataKeeper.UpdateTasksList(_tasks);
    }

    public List<Task> TryGetTasksByLogin(string login)
    {
        if (_tasks.Count > 0)
        {
            List<Task> assignedTasks = new List<Task>();

            foreach (Task task in _tasks)
            {
                if (task.JuniorLogin != null && task.JuniorLogin.Equals(login))
                    assignedTasks.Add(task);
            }

            if (assignedTasks.Count > 0)
                return assignedTasks;
            
            return null;
        }

        return null;
    }

    public List<Task> GetAllTasks()
    {
        return _tasks;
    }

    public List<Task> GetNotDefinedTasks()
    {
        return _tasks.Where(task => task.Status == TaskStatus.NotDefined.ToString()).ToList();
    }

    public Task TryGetTaskByNumber(int number)
    {
        return _tasks.FirstOrDefault(task => number == task.Number);
    }
}
