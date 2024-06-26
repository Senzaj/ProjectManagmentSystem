namespace ProjectManagementSystem.Modules.Keeper;

public class ConvertedHistoryData
{
    public List<string> ConvertedHistory { get; private set; }
    
    public ConvertedHistoryData(List<string> convertedHistory)
    {
        ConvertedHistory = convertedHistory;
    }
}