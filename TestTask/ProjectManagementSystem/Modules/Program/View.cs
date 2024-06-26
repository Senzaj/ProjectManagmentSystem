namespace ProjectManagementSystem.Modules.Program;

public static class View
{
    public static void ClearAndShowText(string text)
    {
        Console.Clear();
        Console.Write(text);
    }

    public static void ShowText(string text)
    {
        Console.WriteLine(text);
    }

    public static string RequestStringInput(string text)
    {
        Console.Write(text);
        return Console.ReadLine();
    }

    public static void RequestAnyKeyInput(string text)
    {
        Console.WriteLine(text + "\n Press any key");
        Console.ReadKey();
    }
    
    public static void RequestAnyKeyInput()
    {
        Console.WriteLine("\n Press any key");
        Console.ReadKey();
    }

    public static void ShowHistory(List<string> history)
    {
        ClearAndShowText("History of changing tasks:\n");

        foreach (string line in history)
            Console.WriteLine(line);
    }

    public static void ShowEmployeesTable(string[,] employeesData)
    {
        Console.Clear();
        Console.WriteLine("All juniors employees:\n");
        DrawLine(ConstantValues.EmployeesTableWidth);
        FillRow(ConstantValues.EmployeesTableWidth, "Login", "Date of employment", "Number of tasks");
        DrawLine(ConstantValues.EmployeesTableWidth);

        for (int i = 0; i < employeesData.GetLength(0); i++)
            FillRow(ConstantValues.EmployeesTableWidth, employeesData[i, 0], employeesData[i, 1], employeesData[i, 2]);

        DrawLine(ConstantValues.EmployeesTableWidth);
    }

    public static void ShowAllTasksTable(string[,] tasksData)
    {
        Console.Clear();
        Console.WriteLine("All tasks:\n");
        DrawLine(ConstantValues.TasksTableWidth);
        FillRow(ConstantValues.TasksTableWidth,
            "Number", "Title", "Description", "Status", "Creation date", "Assigned to");
        DrawLine(ConstantValues.TasksTableWidth);

        for (int i = 0; i < tasksData.GetLength(0); i++)
            FillRow(ConstantValues.TasksTableWidth,
                tasksData[i, 0], tasksData[i, 1], tasksData[i, 2],
                tasksData[i, 3], tasksData[i, 4], tasksData[i, 5]);
        
        DrawLine(ConstantValues.TasksTableWidth);
    }
    
    public static void ShowAssignedTasksTable(string[,] tasksData)
    {
        Console.Clear();
        Console.WriteLine("Assigned tasks:\n");
        DrawLine(ConstantValues.TasksTableWidth);
        FillRow(ConstantValues.TasksTableWidth,
            "Number", "Title", "Description", "Status", "Creation date");
        DrawLine(ConstantValues.TasksTableWidth);

        for (int i = 0; i < tasksData.GetLength(0); i++)
            FillRow(ConstantValues.TasksTableWidth,
                tasksData[i, 0], tasksData[i, 1], tasksData[i, 2],
                tasksData[i, 3], tasksData[i, 4]);
        
        DrawLine(ConstantValues.TasksTableWidth);
    }
    
    public static void ShowNotDefinedTasksTable(string[,] tasksData)
    {
        Console.Clear();
        Console.WriteLine("NotDefined tasks:\n");
        DrawLine(ConstantValues.TasksTableWidth);
        FillRow(ConstantValues.TasksTableWidth,
            "Number", "Title", "Description", "Creation date");
        DrawLine(ConstantValues.TasksTableWidth);

        for (int i = 0; i < tasksData.GetLength(0); i++)
            FillRow(ConstantValues.TasksTableWidth,
                tasksData[i, 0], tasksData[i, 1], tasksData[i, 2], tasksData[i, 3]);
        
        DrawLine(ConstantValues.TasksTableWidth);
    }

    static void DrawLine(int width)
    {
        Console.WriteLine(new string('-', width));
    }

    static void FillRow(int constantWidth, params string[] columns)
    {
        int width = (constantWidth - columns.Length) / columns.Length;
        string row = columns.Aggregate("|", (current, column) => current + AlignText(column, width) + "|");
        Console.WriteLine(row);
    }

    static string AlignText(string text, int width)
    {
        text = text.Length > width
            ? text.Substring(ConstantValues.TableStartIndex, width - ConstantValues.TableMinDistanceToEdge) + "..."
            : text;
        return string.IsNullOrEmpty(text)
            ? new string(' ', width)
            : text.PadRight(width - (width - text.Length) / ConstantValues.TableDivider).PadLeft(width);
    }
}