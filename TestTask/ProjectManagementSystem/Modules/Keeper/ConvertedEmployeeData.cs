using ProjectManagementSystem.Modules.HumanResources;

namespace ProjectManagementSystem.Modules.Keeper;

public class ConvertedEmployeeData
{
    public List<EmployeeProfile> ConvertedProfiles { get; private set; }
    
    public ConvertedEmployeeData(List<EmployeeProfile> convertedProfiles)
    {
        ConvertedProfiles = convertedProfiles;
    }
}