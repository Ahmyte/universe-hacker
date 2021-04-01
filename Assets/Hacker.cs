using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Game configuration data
    const string menuHint = "You can type menu at any time.";
    string[] level1Passwords = {"ball", "sofa", "door", "web", "pizza" };
    string[] level2Passwords = {"cerebro", "claws", "glasses", "lecture", "mutant"};
    string[] level3Passwords = { "shield", "thrusters", "lightning", "augmentation", "demolition" };


    //Game state
    int level;
    string password;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    void Start()
    {
        ShowMainMenu("Hello Player!");
    }

    void ShowMainMenu(string greeting)
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine(greeting);
        Terminal.WriteLine("Where would you like to break into?\n\n" +
                            "Press 1 for Parker's apartment\n" +
                            "Press 2 for Xavier's School\n" +
                            "Press 3 for Avengers Tower\n\n" +
                            "Enter your selection: ");
    }

    void OnUserInput(string input)
    {
        if (input == "menu") //We can always go back to main menu.
        {
            ShowMainMenu("Hello again sir.");
        }
        else if (input == "quit" || input == "close" || input == "exit")
        {
            Terminal.WriteLine("If on the web close the tab.");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        } 
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if(isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "616")
        {
            Terminal.WriteLine("Welcome to our game One Above All!");
        }
        else
        {
            Terminal.WriteLine("You have entered an invalid input.");
            Terminal.WriteLine(menuHint);
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number.");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch(level)
        {
            case 1:
                Terminal.WriteLine("Have a comic...");
                Terminal.WriteLine(@"
 _______              
 \\     \
  \\TASM \        
   \\_____\
    \)_____)    
                        ");
                break;
            case 2:
                Terminal.WriteLine("Have a comic...");
                Terminal.WriteLine(@"
 _______              
 \\     \
  \\X-MEN\        
   \\_____\
    \)_____)    
                        ");
                Terminal.WriteLine("Beware trespasser,\na greater challenge is ahead.");
                break;
            case 3:
                Terminal.WriteLine("Have something special...");
                Terminal.WriteLine(@"
 _______              
 \\     \
  \Jarvis\        
   \\_____\
    \)_____)    
                        ");
                break;
            default:
                Terminal.WriteLine("Invalid level reached.");
                break;
        }     
    }
}
