namespace ProjectManagmentSystem.ProjectManagementSystem.Modules.HumanResources;

public class EmployeeProfile
{
    public string Login { get; protected set; }
    public string Password { get; protected set; }
    public string DateOfEmployment { get; protected set; }

    public string Post { get; protected set; }
    
    public EmployeeProfile(string login, string password, string dateOfEmployment, string post)
    {
        Login = login;
        Password = password;
        DateOfEmployment = dateOfEmployment;
        Post = post;
    }
}