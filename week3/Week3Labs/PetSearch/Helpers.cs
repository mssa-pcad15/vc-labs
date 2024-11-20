using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSearchPart4
{
    internal class Helpers
    {
        internal static void PopulateOurAnimalsArray(ref string animalSpecies, ref string animalID, ref string animalAge, ref string animalPhysicalDescription, ref string animalPersonalityDescription, ref string animalNickname, ref string suggestedDonation, int maxPets, ref decimal decimalDonation, string[,] ourAnimals)
        {
            for (int i = 0; i < maxPets; i++)
            {
                switch (i)
                {
                    case 0:
                        animalSpecies = "dog";
                        animalID = "d1";
                        animalAge = "2";
                        animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 45 pounds. housebroken.";
                        animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
                        animalNickname = "lola";
                        suggestedDonation = "85.00";
                        break;

                    case 1:
                        animalSpecies = "dog";
                        animalID = "d2";
                        animalAge = "9";
                        animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
                        animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
                        animalNickname = "gus";
                        suggestedDonation = "49.99";
                        break;

                    case 2:
                        animalSpecies = "cat";
                        animalID = "c3";
                        animalAge = "1";
                        animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
                        animalPersonalityDescription = "friendly";
                        animalNickname = "snow";
                        suggestedDonation = "40.00";
                        break;

                    case 3:
                        animalSpecies = "cat";
                        animalID = "c4";
                        animalAge = "";
                        animalPhysicalDescription = "";
                        animalPersonalityDescription = "";
                        animalNickname = "lion";
                        suggestedDonation = "";

                        break;

                    default:
                        animalSpecies = "";
                        animalID = "";
                        animalAge = "";
                        animalPhysicalDescription = "";
                        animalPersonalityDescription = "";
                        animalNickname = "";
                        suggestedDonation = "";
                        break;

                }

                ourAnimals[i, 0] = "ID #: " + animalID;
                ourAnimals[i, 1] = "Species: " + animalSpecies;
                ourAnimals[i, 2] = "Age: " + animalAge;
                ourAnimals[i, 3] = "Nickname: " + animalNickname;
                ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
                ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;

                if (!decimal.TryParse(suggestedDonation, out decimalDonation))
                {
                    decimalDonation = 45.00m; // if suggestedDonation NOT a number, default to 45.00
                }
                ourAnimals[i, 6] = $"Suggested Donation: {decimalDonation:C2}";
            }
        }
    }
}
