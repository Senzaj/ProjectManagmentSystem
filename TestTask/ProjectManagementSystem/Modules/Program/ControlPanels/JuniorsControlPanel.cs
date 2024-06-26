namespace ProjectManagementSystem.Modules.Program.ControlPanels;

using ProjectManagementSystem.Modules.Keeper;
using ProjectManagementSystem.Modules.DataBase;
using ProjectManagementSystem.Modules.HumanResources;
using Task = ProjectManagementSystem.Modules.Task.Task;

public class JuniorsControlPanel : ControlPanel
{
    private DataKeeper _dataKeeper;
    private JuniorEmployeeProfile _currentJunProfile;
    
    private const string JuniorActions = $" Select action:\n" +
                                         $"  1) Change the task status to the following\n" +
                                         $"  2) Show assigned tasks\n" +
                                         $"  3) Log out\n" +
                                         $" Enter: ";

    public JuniorsControlPanel(DataKeeper dataKeeper)
    {
        _dataKeeper = dataKeeper;
    }

    protected override void ActionSelect()
    {
        _currentJunProfile = (JuniorEmployeeProfile)_currentProfile;
        bool inSystem = true;

        while (inSystem)
        {
            if (!CheckMainActionSelection(JuniorActions, out int number))
                continue;

            switch (number)
            {
                case 1:
                    ChangeTaskStatus();
                    break;

                case 2:
                    TryShowTasks();
                    View.RequestAnyKeyInput();
                    break;

                case 3:
                    inSystem = false;
                    LogOut();
                    break;

                default:
                    View.RequestAnyKeyInput("\n Invalid input");
                    continue;
            }
        }
    }

    private void ChangeTaskStatus()
    {
        TryShowTasks();
        string input = View.RequestStringInput($"\nEnter number of task: ");

        if (CheckActionSelection(input, out int number))
        {
            Task task = _currentJunProfile.GetAssignedTasks().FirstOrDefault(task => number == task.Number);

            if (task != null)
            {
                if (task.TrySetNextStatus())
                {
                    _dataKeeper.SaveTasks();
                    View.ClearAndShowText("Updated assigned tasks");
                    TryShowTasks();
                    _dataKeeper.AddStringToHistory(
                        $" [{DateTime.Now:G}] {_currentJunProfile.Login} changed the status" +
                        $" of task number {task.Number} to {task.Status}");
                    View.RequestAnyKeyInput(
                        $"\n task number {task.Number} status has been successfully changed to {task.Status}");
                }
                else
                    View.RequestAnyKeyInput($"\n task number {task.Number} already has the status {task.Status}");
            }
            else
                View.RequestAnyKeyInput("\n Invalid input");
        }
        else
            View.RequestAnyKeyInput("\n Invalid input");
    }

    private void TryShowTasks()
    {
        List<Task> assignedTasks = _currentJunProfile.GetAssignedTasks();

        if (assignedTasks.Count > 0)
        {
            string[,] tasksPreparedForTable = new string[assignedTasks.Count, ConstantValues.AssignedTableColumnsCount];
            int index = 0;

            foreach (Task task in assignedTasks)
            {
                tasksPreparedForTable[index, 0] = task.Number.ToString();
                tasksPreparedForTable[index, 1] = task.Title;
                tasksPreparedForTable[index, 2] = task.Description;
                tasksPreparedForTable[index, 3] = task.Status;
                tasksPreparedForTable[index++, 4] = task.CreationDate;
            }
            
            View.ShowAssignedTasksTable(tasksPreparedForTable);
        }
        else
        {
            View.RequestAnyKeyInput("\n You have no assigned tasks");
            ActionSelect();
        }
    }
}