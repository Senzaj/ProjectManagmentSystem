using ProjectManagmentSystem.ProjectManagementSystem.Modules.HumanResources;

namespace ProjectManagmentSystem.ProjectManagementSystem.Modules.Keeper;

public class ConvertedEmployeeData
{
    public List<EmployeeProfile> ConvertedProfiles { get; private set; }
    
    public ConvertedEmployeeData(List<EmployeeProfile> convertedProfiles)
    {
        ConvertedProfiles = convertedProfiles;
    }
}