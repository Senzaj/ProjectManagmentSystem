namespace ProjectManagmentSystem.ProjectManagementSystem.Modules.HumanResources;

using Task = global::ProjectManagmentSystem.ProjectManagementSystem.Modules.Task.Task;

public class JuniorEmployeeProfile: EmployeeProfile
{
    public int AssignedTasks => _assignedTasks.Count;

    private List<Task> _assignedTasks;

    public JuniorEmployeeProfile(string login, string password, string dateOfEmployment, string post) : base(login, password,
        dateOfEmployment, post)
    {
        Login = login;
        Password = password;
        DateOfEmployment = dateOfEmployment;
        Post = post;
        _assignedTasks = new List<Task>();
    }

    public void AssignTask(Task task)
    {
        _assignedTasks.Add(task);
    }

    public List<Task> GetAssignedTasks()
    {
        return _assignedTasks;
    }
}