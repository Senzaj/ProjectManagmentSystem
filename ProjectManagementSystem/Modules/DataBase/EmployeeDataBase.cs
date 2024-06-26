using ProjectManagmentSystem.ProjectManagementSystem.Modules.HumanResources;
using ProjectManagmentSystem.ProjectManagementSystem.Modules.Keeper;

namespace ProjectManagmentSystem.ProjectManagementSystem.Modules.DataBase;

using Task = global::ProjectManagmentSystem.ProjectManagementSystem.Modules.Task.Task;

public class EmployeeDataBase
{
    private List<EmployeeProfile> _employees;
    private DataKeeper _dataKeeper;
    private TaskDataBase _taskDataBase;
    
    public Action EnteredInvalidLogin;
    public Action EnteredInvalidPassword;

    public EmployeeDataBase(DataKeeper dataKeeper, TaskDataBase taskDataBase)
    {
        _taskDataBase = taskDataBase;
        _dataKeeper = dataKeeper;
        _employees = _dataKeeper.TryGetEmployees();


        if (_employees != null)
        {
            List<EmployeeProfile> tempEmployees = new List<EmployeeProfile>();

            foreach (EmployeeProfile employee in _employees)
            {
                if (employee.Post == EmployeePost.Senior.ToString())
                {
                    tempEmployees.Add(new SeniorEmployeeProfile(employee.Login, employee.Password,
                        employee.DateOfEmployment, employee.Post));
                }
                else
                {
                    JuniorEmployeeProfile jun = new JuniorEmployeeProfile(employee.Login, employee.Password,
                        employee.DateOfEmployment, employee.Post);
                    
                    tempEmployees.Add(jun);

                    List<Task> assignedTasks = _taskDataBase.TryGetTasksByLogin(jun.Login);

                    if (assignedTasks != null)
                    {
                        foreach (Task task in assignedTasks)
                        {
                            jun.AssignTask(task);
                        }
                    }
                }
            }

            _employees = tempEmployees;

            return;
        }

        EmployeeProfile senior =
            new SeniorEmployeeProfile("Admin", "1234", DateTime.Now.ToString("G"),
                EmployeePost.Senior.ToString());
        _employees = new List<EmployeeProfile>() { senior };
        _dataKeeper.SaveEmployees(_employees);
    }

    public void Add(EmployeeProfile? employeeProfile)
    {
        _employees.Add(employeeProfile);
        _dataKeeper.SaveEmployees(_employees);
    }
    
    public void Remove(EmployeeProfile? employeeProfile)
    {
        _employees.Remove(employeeProfile);
        _dataKeeper.SaveEmployees(_employees);
    }

    public bool CheckLoginUniqueness(string login)
    {
        return _employees.All(employee => !employee.Login.Equals(login));
    }

    public JuniorEmployeeProfile TryGetJunEmployee(string login)
    {
        return GetJunEmployees().FirstOrDefault(jun => login.Equals(jun.Login));
    }
    
    public EmployeeProfile? TryGetEmployeeProfile(string login, string password)
    {
        bool _isLoginExist = false;
        
        foreach (var employee in _employees)
        {
            if (employee.Login.Equals(login) )
            {
                _isLoginExist = true;
                
                if (employee.Password.Equals(password))
                    return employee;

                EnteredInvalidPassword?.Invoke();
                break;
            }
        }

        if (!_isLoginExist)
            EnteredInvalidLogin?.Invoke();
        
        return null;
    }

    public List<JuniorEmployeeProfile> GetJunEmployees()
    {
        List<JuniorEmployeeProfile> juniors = new List<JuniorEmployeeProfile>();

        foreach (EmployeeProfile employee in _employees)
        {
            if (employee.GetType() == typeof(JuniorEmployeeProfile))
                juniors.Add((JuniorEmployeeProfile)employee);
        }

        return juniors;
    }
}