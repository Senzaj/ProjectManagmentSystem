namespace ProjectManagementSystem.Modules.Program.ControlPanels;

using ProjectManagementSystem.Modules.HumanResources;

public abstract class ControlPanel
{
    public Action<EmployeeProfile> LoggedOut;

    protected EmployeeProfile _currentProfile;

    public void LogIn(EmployeeProfile employeeProfile)
    {
        _currentProfile = employeeProfile;
        ActionSelect();
    }

    protected abstract void ActionSelect();

    protected bool CheckMainActionSelection(string Actions, out int number)
    {
        View.ClearAndShowText($"\nWelcome, {_currentProfile.Login}!\n\n");
            
        string input = View.RequestStringInput(Actions);

        if (Int32.TryParse(input, out number))
            return true;
        
        return false;
    }
    
    protected bool CheckActionSelection(string input, out int number)
    {
        if (Int32.TryParse(input, out number))
            return true;
        
        return false;
    }

    protected void LogOut()
    {
        LoggedOut.Invoke(_currentProfile);
    }
}