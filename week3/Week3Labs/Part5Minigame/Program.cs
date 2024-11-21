using System;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] states = {"('-')", "(^-^)", "(X_X)"};
string[] foods = {"@@@@@", "$$$$$", "#####"};

// Current player string displayed in the Console
string player = states[0];

// Index of the current food
int food = 0;
bool isTerminalResized=false;
InitializeGame();
while (!shouldExit && !TerminalResized())
{
    if (IsPlayerSick())
    {
        FreezePlayer();
    }
    if (IsPlayerFast()) { //player is fast when player  is (^-^)
        Move(endIfNonDirectionalKey: false, step: 3); 
    }
    else
    {
        Move(endIfNonDirectionalKey: false);
    }


}

bool IsPlayerFast()
{
    return player == states[1];
}

bool IsPlayerSick()
{
    return player == states[2];
}

Console.Clear();

if (isTerminalResized)
{ Console.WriteLine("Console was resized. Program exiting."); }
else
{ Console.WriteLine("Thank you for playing!"); }




// Returns true if the Terminal was resized 
bool TerminalResized() 
{
    isTerminalResized = (height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5);
    return isTerminalResized;
}

// Displays random food at a random location
void ShowFood() 
{
    // Update food to a random index
    food = random.Next(0, foods.Length);

    // Update food position to a random location
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    // Display the food at the location
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Changes the player to match the food consumed
void ChangePlayer() 
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporarily stops the player from moving
void FreezePlayer() 
{
    System.Threading.Thread.Sleep(1000);
    player = states[0];
}

// Reads directional input from the Console and moves the player
void Move(bool endIfNonDirectionalKey=false,int step=1) 
{
    //if (TerminalResized()) {
    //    Console.WriteLine("Console was resized. Program exiting.");
    //    System.Environment.Exit(0);
    //}
    

    int lastX = playerX;
    int lastY = playerY;
    
    switch (Console.ReadKey(true).Key) 
    {
        case ConsoleKey.UpArrow:
            playerY--; 
            break;
		case ConsoleKey.DownArrow: 
            playerY++; 
            break;
		case ConsoleKey.LeftArrow:  
            playerX-= step; 
            break;
		case ConsoleKey.RightArrow: 
            playerX+= step; 
            break;
		case ConsoleKey.Escape:     
            shouldExit = true; 
            break;
        default:
            shouldExit = endIfNonDirectionalKey;
            break;
    }

    // Clear the characters at the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++) 
    {
        Console.Write(" ");
    }

    // Keep player position within the bounds of the Terminal window
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
    playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

    //Does player overlap with food?
    if (PlayerOverlapsFood(playerX, playerY, foodX, foodY)) {
        ShowFood();
    }

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);

}

bool PlayerOverlapsFood(int playerX, int playerY, int foodX, int foodY)
{
    if (playerX+player.Length > foodX && playerX < foodX + foods[food].Length && playerY == foodY)
    {
        //erase food
        Console.SetCursorPosition(foodX, foodY);
        Console.Write(new string(' ', foods[food].Length));

        //change player appearance
        player = states[food];
        return true;
    }
    return false;
}

// Clears the console, displays the food and player
void InitializeGame() 
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}