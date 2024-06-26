namespace ProjectManagementSystem.Modules.Program;

using ProjectManagementSystem.Modules.DataBase;
using ProjectManagementSystem.Modules.HumanResources;
using ProjectManagementSystem.Modules.Program.ControlPanels;

public class AuthorizationPanel
{
    private EmployeeDataBase _employeeDataBase;
    private JuniorsControlPanel _juniorsControlPanel;
    private SeniorsControlPanel _seniorsControlPanel;

    public AuthorizationPanel(EmployeeDataBase employeeDataBase, JuniorsControlPanel junControlPanel,
        SeniorsControlPanel senControlPanel)
    {
        _employeeDataBase = employeeDataBase;
        _juniorsControlPanel = junControlPanel;
        _seniorsControlPanel = senControlPanel;
    }

    public void Authorization()
    {
        while (true)
        {
            View.ClearAndShowText("log in to the system\n");
            
            string[] logAndPassword = DataVerifier.VerifyLogAndPasswordLenght();
            
            if (logAndPassword == null)
                continue;
            
            _employeeDataBase.EnteredInvalidLogin += OnEnteredInvalidLogin;
            _employeeDataBase.EnteredInvalidPassword += OnEnteredInvalidPassword;
            EmployeeProfile? employeeProfile = _employeeDataBase.TryGetEmployeeProfile(logAndPassword[0], logAndPassword[1]);
            _employeeDataBase.EnteredInvalidLogin -= OnEnteredInvalidLogin;
            _employeeDataBase.EnteredInvalidPassword -= OnEnteredInvalidPassword;

            if (employeeProfile != null)
            {
                EnterEmployeeProfile(employeeProfile);
                break;
            }
        }
    }

    private void OnEnteredInvalidLogin()
    {
        View.RequestAnyKeyInput("\nInvalid Login");
    }
    
    private void OnEnteredInvalidPassword()
    {
        View.RequestAnyKeyInput("\nInvalid Password");
    }

    private void EnterEmployeeProfile(EmployeeProfile profile)
    {
        if (profile.GetType() == typeof(SeniorEmployeeProfile))
            EnterSeniorEmployeeProfile(profile);
        else
            EnterJuniorEmployeeProfile(profile);
    }

    private void EnterSeniorEmployeeProfile(EmployeeProfile profile)
    {
        _seniorsControlPanel.LoggedOut += ReAuthorization;
        _seniorsControlPanel.LogIn(profile);
    }

    private void EnterJuniorEmployeeProfile(EmployeeProfile profile)
    {
        _juniorsControlPanel.LoggedOut += ReAuthorization;
        _juniorsControlPanel.LogIn(profile);
    }

    private void ReAuthorization(EmployeeProfile profile)
    {
        if (profile.GetType() == typeof(SeniorEmployeeProfile))
            _seniorsControlPanel.LoggedOut -= ReAuthorization;
        if (profile.GetType() == typeof(JuniorEmployeeProfile))
            _juniorsControlPanel.LoggedOut -= ReAuthorization;
        
        Authorization();
    }
}