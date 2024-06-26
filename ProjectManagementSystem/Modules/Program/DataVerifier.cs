namespace ProjectManagmentSystem.ProjectManagementSystem.Modules.Program;

public static class DataVerifier
{
    public static string[] VerifyLogAndPasswordLenght()
    {
        string login = View.RequestStringInput("Enter login: ");

        if (!string.IsNullOrEmpty(login) && login.Length >= ConstantValues.MinLoginLenght
                                         && login.Length <= ConstantValues.MaxLoginLenght)
        {
            string password = View.RequestStringInput("Enter password: ");

            if (!string.IsNullOrEmpty(password) && password.Length >= ConstantValues.MinPasswordLenght 
                                                && password.Length <= ConstantValues.MaxPasswordLenght)
            {
                return new []{login, password};
            }
            
            View.RequestAnyKeyInput($"\nInvalid Password\n (the number of characters in the password" +
                                    " must be between" +
                                    $" {ConstantValues.MinPasswordLenght} and {ConstantValues.MaxPasswordLenght})");
        }
        else
            View.RequestAnyKeyInput($"\nInvalid Login\n (the number of characters in the login" +
                                    " must be between" +
                                    $" {ConstantValues.MinLoginLenght} and {ConstantValues.MaxLoginLenght})");

        return null;
    }

    public static string[] VerifyTitleAndDescriptionLenght()
    {
        string title = View.RequestStringInput("Enter task title: ");

        if (!string.IsNullOrEmpty(title) && title.Length >= ConstantValues.MinTitleLenght
                                         && title.Length <= ConstantValues.MaxTitleLenght)
        {
            string description = View.RequestStringInput("Enter task description: ");

            if (!string.IsNullOrEmpty(description) && description.Length >= ConstantValues.MinDescriptionLenght 
                                                   && description.Length <= ConstantValues.MaxDescriptionLenght)
            {
                return new[] { title, description };
            }

            View.RequestAnyKeyInput("\n Invalid input\n (the number of characters in the description" +
                                    " must be between" +
                                    $" {ConstantValues.MinDescriptionLenght} and {ConstantValues.MaxDescriptionLenght})");
        }
        else
            View.RequestAnyKeyInput($"\n Invalid input\n (the number of characters in the title" +
                                    " must be between" +
                                    $" {ConstantValues.MinTitleLenght} and {ConstantValues.MaxTitleLenght})");

        return null;
    }
}