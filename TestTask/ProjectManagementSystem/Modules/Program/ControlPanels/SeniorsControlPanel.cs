using ProjectManagementSystem.Modules.Keeper;

namespace ProjectManagementSystem.Modules.Program.ControlPanels;

using ProjectManagementSystem.Modules.HumanResources;
using ProjectManagementSystem.Modules.DataBase;
using Task = ProjectManagementSystem.Modules.Task.Task;
using TaskStatus = ProjectManagementSystem.Modules.Task.TaskStatus;

public class SeniorsControlPanel: ControlPanel
{
    private EmployeeDataBase _employees;
    private TaskDataBase _tasks;
    private DataKeeper _dataKeeper;
    private const string SeniorActions = $" Select action:\n" +
                                         $"  1) Register new senior employee\n" +
                                         $"  2) Register new junior employee\n" +
                                         $"  3) Create new Task\n" +
                                         $"  4) Assign task to junior employee\n" +
                                         $"  5) Show all tasks\n" +
                                         $"  6) Show junior employees\n" +
                                         $"  7) Show tasks history\n" +
                                         $"  8) Log out\n" +
                                         $" Enter: ";

    public SeniorsControlPanel( EmployeeDataBase employees, TaskDataBase tasks, DataKeeper dataKeeper)
    {
        _employees = employees;
        _tasks = tasks;
        _dataKeeper = dataKeeper;
    }

    protected override void ActionSelect()
    {
        bool inSystem = true;
        
        while (inSystem)
        {
            if (!CheckMainActionSelection(SeniorActions, out int number))
                continue;

            switch (number)
            {
                case 1:
                    TryRegisterSeniorEmployee();
                    break;

                case 2:
                    TryRegisterJuniorEmployee();
                    break;

                case 3:
                    TryCreateTask();
                    break;

                case 4:
                    TryAssignTask();
                    break;

                case 5:
                    TryShowAllTasks();
                    View.RequestAnyKeyInput();
                    break;

                case 6:
                    TryShowJunEmployees();
                    View.RequestAnyKeyInput();
                    break;

                case 7:
                    TryShowHistory();
                    View.RequestAnyKeyInput();
                    break;
                
                case 8:
                    inSystem = false;
                    LogOut();
                    break;

                default:
                    View.RequestAnyKeyInput("\n Invalid input");
                    continue;
            }
        }
    }

    private string[] TryRegisterEmployee()
    {
        View.ClearAndShowText("Register to the system\n");

        string[] logAndPassword = DataVerifier.VerifyLogAndPasswordLenght();

        if (logAndPassword != null)
        {
            if (_employees.CheckLoginUniqueness(logAndPassword[0]))
                return new[] { logAndPassword[0], logAndPassword[1] };
            
            View.RequestAnyKeyInput("This login is already in use");
        }

        return null;
    }

    private void TryRegisterSeniorEmployee()
    {
        string[] logAndPassword;

        if ((logAndPassword = TryRegisterEmployee()) != null)
        {
            _employees.Add(new SeniorEmployeeProfile(logAndPassword[0], logAndPassword[1],
                DateTime.Now.ToString("G"), EmployeePost.Senior.ToString()));
            View.RequestAnyKeyInput(
                $"{logAndPassword[0]} is successfully registered in the system as Senior employee");
        }
    }

    private void TryRegisterJuniorEmployee()
    {
        string[] logAndPassword;

        if ((logAndPassword = TryRegisterEmployee()) != null)
        {
            _employees.Add(new JuniorEmployeeProfile(logAndPassword[0], logAndPassword[1],
                DateTime.Now.ToString("G"), EmployeePost.Junior.ToString()));
            View.RequestAnyKeyInput(
                $"{logAndPassword[0]} is successfully registered in the system as Junior employee");
        }
    }

    private void TryCreateTask()
    {
        View.ClearAndShowText("Task creation\n\n");

        string[] titleAndDescription = DataVerifier.VerifyTitleAndDescriptionLenght();

        if (titleAndDescription != null)
        {
            Task newTask = new Task(titleAndDescription[0], titleAndDescription[1], DateTime.Now.ToString("G"),
                TaskStatus.NotDefined.ToString(), null);
            _tasks.Add(newTask);
            _dataKeeper.AddStringToHistory($" [{DateTime.Now:G}] Task number {newTask.Number} has been created");
            View.RequestAnyKeyInput($"\n Task number {newTask.Number} has been successfully created");
        }
    }

    private void TryAssignTask()
    {
        TryShowNotDefinedTasks();
        string input = View.RequestStringInput($"\nEnter number of task: ");

        if (CheckActionSelection(input, out int number))
        {
            Task task = _tasks.TryGetTaskByNumber(number);

            if (task != null)
            {
                if (task.Status == TaskStatus.NotDefined.ToString())
                {
                    TryShowJunEmployees();
                    string login = View.RequestStringInput($"\nEnter login of employee: ");

                    JuniorEmployeeProfile jun = _employees.TryGetJunEmployee(login);

                    if (jun != null)
                    {
                        jun.AssignTask(task);
                        task.SetJunLogin(jun.Login);
                        task.Define();
                        _dataKeeper.SaveTasks();
                        _dataKeeper.AddStringToHistory(
                            $" [{DateTime.Now:G}] Task number {task.Number} has been assigned to {jun.Login}");
                        View.RequestAnyKeyInput(
                            $"\n Task Number {task.Number} is successfully assigned to {jun.Login}");
                    }
                    else
                    {
                        View.RequestAnyKeyInput("\n Invalid input");
                        ActionSelect();
                    }
                }
                else
                    View.RequestAnyKeyInput($"\n Task number {task.Number} is already defined");
            }
            else
                View.RequestAnyKeyInput("\n Invalid input");
        }
        else
            View.RequestAnyKeyInput("\n Invalid input");
    }

    private void TryShowAllTasks()
    {
        List<Task> allTasks = _tasks.GetAllTasks();

        if (allTasks.Count > 0)
        {
            string[,] tasksPreparedForTable = new string[allTasks.Count, ConstantValues.AllTasksTableColumnsCount];
            int index = 0;

            foreach (Task task in allTasks)
            {
                tasksPreparedForTable[index, 0] = task.Number.ToString();
                tasksPreparedForTable[index, 1] = task.Title;
                tasksPreparedForTable[index, 2] = task.Description;
                tasksPreparedForTable[index, 3] = task.Status;
                tasksPreparedForTable[index, 4] = task.CreationDate;

                if (string.IsNullOrEmpty(task.JuniorLogin))
                    tasksPreparedForTable[index, 5] = "-";
                else
                    tasksPreparedForTable[index, 5] = task.JuniorLogin;

                index++;
            }

            View.ShowAllTasksTable(tasksPreparedForTable);
        }
        else
        {
            View.RequestAnyKeyInput("No tasks have been created");
            ActionSelect();
        }
    }
    
    private void TryShowNotDefinedTasks()
    {
        List<Task> notDefinedTasks = _tasks.GetNotDefinedTasks();

        if (notDefinedTasks.Count > 0)
        {
            string[,] tasksPreparedForTable =
                new string[notDefinedTasks.Count, ConstantValues.NotDefinedTableColumnsCount];
            int index = 0;

            foreach (Task task in notDefinedTasks)
            {
                tasksPreparedForTable[index, 0] = task.Number.ToString();
                tasksPreparedForTable[index, 1] = task.Title;
                tasksPreparedForTable[index, 2] = task.Description;
                tasksPreparedForTable[index++, 3] = task.CreationDate;
            }

            View.ShowNotDefinedTasksTable(tasksPreparedForTable);
        }
        else
        {
            View.RequestAnyKeyInput("\n All tasks are defined");
            ActionSelect();
        }
    }

    private void TryShowJunEmployees()
    {
        List<JuniorEmployeeProfile> juniors = _employees.GetJunEmployees();

        if (juniors.Count > 0)
        {
            string[,] juniorsPreparedForTable = new string[juniors.Count, ConstantValues.EmployeeTableColumnsCount];
            int index = 0;

            foreach (JuniorEmployeeProfile junior in juniors)
            {
                juniorsPreparedForTable[index, 0] = junior.Login;
                juniorsPreparedForTable[index, 1] = junior.DateOfEmployment;
                juniorsPreparedForTable[index++, 2] = junior.AssignedTasks.ToString();
            }

            View.ShowEmployeesTable(juniorsPreparedForTable);
        }
        else
        {
            View.RequestAnyKeyInput("\n Not a single junior employee was hired");
            ActionSelect();
        }
    }

    private void TryShowHistory()
    {
        List<string> history = _dataKeeper.TryGetHistory();

        if (history != null)
            View.ShowHistory(history);
        else
            View.ClearAndShowText("Task history is empty");
    }
}