using PetSearchPart4;
using System;

// ourAnimals array will store the following: 
string animalSpecies = "";
string animalID = "";
string animalAge = "";
string animalPhysicalDescription = "";
string animalPersonalityDescription = "";
string animalNickname = "";
string suggestedDonation = "";

// variables that support data entry
int maxPets = 8;
string? readResult;
string menuSelection = "";
decimal decimalDonation = 0.00m;

// array used to store runtime data
string[,] ourAnimals = new string[maxPets, 7];

// Use a helper function to populate the Animals Array
Helpers.PopulateOurAnimalsArray(ref animalSpecies, ref animalID, ref animalAge, ref animalPhysicalDescription, ref animalPersonalityDescription, ref animalNickname, ref suggestedDonation, maxPets, ref decimalDonation, ourAnimals);
// also serves as a layer of indirection

// top-level menu options
do
{
    // NOTE: the Console.Clear method is throwing an exception in debug sessions
    Console.Clear();

    Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
    Console.WriteLine(" 1. List all of our current pet information");
    Console.WriteLine(" 2. Display all dogs with a specified characteristic");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }

    // switch-case to process the selected menu option
    switch (menuSelection)
    {
        case "1":
            // list all pet info
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    Console.WriteLine();
                    for (int j = 0; j < 7; j++)
                    {
                        Console.WriteLine(ourAnimals[i, j].ToString());
                    }
                }
            }

            Console.WriteLine("\r\nPress the Enter key to continue");
            readResult = Console.ReadLine();

            break;

        case "2":
            // #1 Display all dogs with a multiple search characteristics

            string[]? dogCharacteristics = default; //need array of string for multiple search terms

            while (dogCharacteristics == null) //while loop ensures dogCharacteristics array is properly filled in.
            {
                // #2 have user enter multiple comma separated characteristics to search for
                Console.WriteLine($"\r\nEnter dog characteristics to search for, separated by commas.");
                readResult = Console.ReadLine(); // read user typed line into readResult
                if (readResult != null) // user typed something
                {
                    dogCharacteristics = readResult.ToLower().Split(",");//turn the line to lowercase and split on comma
                    Console.WriteLine();
                }
            }

            bool noMatchesDog = true;
            string dogDescription = "";
            
            // #4 update to "rotating" animation with countdown
            string[] searchingIcons = {" - ", " / ", " | "," \\ "}; //change the animation to spining bar from . .. ...

            // loop ourAnimals array to search for matching animals
            for (int i = 0; i < maxPets; i++) // this for loop iterates all pets
            {

                if (ourAnimals[i, 1].Contains("dog"))//only search against rows that are dogs, skip cats
                {
                    
                    // Search combined descriptions and report results
                    dogDescription = ourAnimals[i, 4] + "\r\n" + ourAnimals[i, 5]; // combine Physical and Personality Descriptions in to one string
                    
                    for (int animationLoops = 5; animationLoops > -1 ; animationLoops--)//display spining bar 6 times
                    {
                    // #5 update "searching" message to show countdown 
                        foreach (string icon in searchingIcons)
                        {
                            Console.Write($"\rsearching our dog {ourAnimals[i, 3]} for {string.Join(",", dogCharacteristics)} {icon}");
                            Thread.Sleep(250);//pause for 250ms
                        }
                        
                        Console.Write($"\r{new String(' ', Console.BufferWidth)}");
                    }
                    
                    // #3a iterate submitted characteristic terms and search description for each term
                    foreach (string dogCharacteristic in dogCharacteristics)
                    {
                        if (dogDescription.Contains(dogCharacteristic.Trim())) // didn't trim the term earlier, do it now
                        {
                            // #3b update message to reflect term 
                            // #3c set a flag "this dog" is a match
                            Console.WriteLine($"\nOur dog {ourAnimals[i, 3]} is a match! Search Term : {dogCharacteristic}");
                            noMatchesDog = false;
                        }
                    }
                    Console.WriteLine($"Dog Description: \n {dogDescription}");
                }
            }

            if (noMatchesDog)
            {
                Console.WriteLine($"None of our dogs are a match found for: { string.Join(",", dogCharacteristics)}");
            }

            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();

            break;

        default:
            break;
    }

} while (menuSelection != "exit");

