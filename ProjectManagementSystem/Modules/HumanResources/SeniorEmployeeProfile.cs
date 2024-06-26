namespace ProjectManagmentSystem.ProjectManagementSystem.Modules.HumanResources;

public class SeniorEmployeeProfile : EmployeeProfile
{
    public SeniorEmployeeProfile(string login, string password, string dateOfEmployment, string post) : base(login,
        password, dateOfEmployment, post)
    {
        Login = login;
        Password = password;
        DateOfEmployment = dateOfEmployment;
        Post = post;
    }
}