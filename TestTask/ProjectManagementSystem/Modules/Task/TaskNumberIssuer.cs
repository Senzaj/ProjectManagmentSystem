namespace ProjectManagementSystem.Modules.Task;

public static class TaskNumberIssuer
{
    private const int StartNumber = 1;
    private const int Difference = 1;
    private static int _nextNumber = StartNumber;

    public static int SetNumber()
    {
        int returnedNumber = _nextNumber;
        _nextNumber += Difference;
        return returnedNumber;
    }
}