using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.LearnTypes
{

    public struct Card
    {
       public CardSuit Suit { get; set; }
       public CardRank Rank { get; set; }

        public override string ToString()
        {
            string result = string.Empty;
            switch (Suit)
            {
                case CardSuit.Spade:
                    result = "♠";
                    break;
                case CardSuit.Heart:
                    result = "♥";
                    break;
                case CardSuit.Club:
                    result = "♣";
                    break;
                case CardSuit.Diamond:
                    result = "♦";
                    break;
                default:
                    break;
            }

            result += (int)Rank;
            return result;
        }
    }

    public enum CardRank
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
    public enum CardSuit
    {
        Spade = 1,
        Heart = 2,
        Club = 3,
        Diamond = 4
    }
}
