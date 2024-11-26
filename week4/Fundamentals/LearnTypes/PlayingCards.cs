using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.LearnTypes
{

    public record struct Card : IComparable<Card>
    {
       public CardSuit Suit { get; set; }
       public CardRank Rank { get; set; }

        public int CompareTo(Card other) => new RankOnlyComparer().Compare(this, other);
            
            //this.Rank.CompareTo(other.Rank);
        //{
            //int thisRank = (int)this.Rank;
            //int otherRank = (int)other.Rank;
            //return thisRank - otherRank;
        //}

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



    public  class RankOnlyComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
          return x.Rank.CompareTo(y.Rank);
        }
    }
    public class SuitRankComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            int valX = (int)x.Suit * 100 + (int)x.Rank;
            int valY = (int)y.Suit * 100 + (int)y.Rank;

            return valX.CompareTo(valY);
        }
    }

    public class DeckEqualityComparer : IEqualityComparer<Deck>
    {
        public bool Equals(Deck? x, Deck? y)
        {
            if (y == null) return false;
            for (int i = 0; i < 52; i++)
            {
                Card a = x.Deal(); // this mean
                Card b = y.Deal();
                if (a != b) return false;
            }
            return true;
        }

        public int GetHashCode([DisallowNull] Deck obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
