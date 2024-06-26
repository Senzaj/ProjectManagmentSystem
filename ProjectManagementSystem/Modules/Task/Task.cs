using TaskStatus = ProjectManagmentSystem.ProjectManagementSystem.Modules.Task.TaskStatus;

namespace ProjectManagmentSystem.ProjectManagementSystem.Modules.Task;

public class Task
{
    public int Number { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Status { get; private set; }
    public string CreationDate { get; private set; }
    public string JuniorLogin { get; private set; }
    
    public Task(string title, string description, string creationDate, string status, string juniorLogin)
    {
        Number = TaskNumberIssuer.SetNumber();
        Status = status;
        Title = title;
        Description = description;
        CreationDate = creationDate;
        JuniorLogin = juniorLogin;
    }

    public void SetJunLogin(string login)
    {
        JuniorLogin = login;
    }
    
    public void Define()
    {
        Status = TaskStatus.Defined.ToString();
    }

    public bool TrySetNextStatus()
    {
        if (Status == TaskStatus.Defined.ToString())
        {
            GetStarted();
            return true;
        }

        if (Status == TaskStatus.InProgress.ToString())
        {
            Complete();
            return true;
        }

        return false;
    }
    
    private void GetStarted()
    {
        Status = TaskStatus.InProgress.ToString();
    }
    
    private void Complete()
    {
        Status = TaskStatus.Done.ToString();
    }
}