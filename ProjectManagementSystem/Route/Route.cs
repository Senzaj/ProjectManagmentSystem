using ProjectManagmentSystem.ProjectManagementSystem.Modules.DataBase;
using ProjectManagmentSystem.ProjectManagementSystem.Modules.Keeper;
using ProjectManagmentSystem.ProjectManagementSystem.Modules.Program;
using ProjectManagmentSystem.ProjectManagementSystem.Modules.Program.ControlPanels;

namespace ProjectManagmentSystem.ProjectManagementSystem.Route;

internal class Route
{
    private static EmployeeDataBase _employeeDataBase;
    private static TaskDataBase _taskDataBase;
    private static AuthorizationPanel _authorizationPanel;
    private static JuniorsControlPanel _juniorsControlPanel;
    private static SeniorsControlPanel _seniorsControlPanel;
    private static DataKeeper _dataKeeper;

    private static void Main()
    {
        _dataKeeper = new DataKeeper();
        _taskDataBase = new TaskDataBase(_dataKeeper);
        _employeeDataBase = new EmployeeDataBase(_dataKeeper, _taskDataBase);
        _seniorsControlPanel = new SeniorsControlPanel( _employeeDataBase, _taskDataBase, _dataKeeper);
        _juniorsControlPanel = new JuniorsControlPanel(_dataKeeper);

        _authorizationPanel =
            new AuthorizationPanel(_employeeDataBase, _juniorsControlPanel, _seniorsControlPanel);
        _authorizationPanel.Authorization();
    }
}