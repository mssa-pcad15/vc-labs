using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.LearnTypes
{
    public class Deck:IEquatable<Deck>
    {
        //public static int NumberOfCards = 52;
        public const int NumberOfCards = 52; //either one works, what is the difference?
        public bool IsInNewDeckOrder { get; private set; } //flag indicates whether the deck had been shuffled
        public int RemainingCards => NumberOfCards - currentCard; //RemainingCards is calculated from NumberOfCards-currentCards

        private Card[] _cards = new Card[NumberOfCards]; // private array of Card
        private int currentCard = 0; //this is the pointer to the current card in the deck
        
        

        public Deck() { // this is constructor, we will create 52 card in Suit/Rank order and set the IsInNewDeckOrder flag to true

            int cardPosition = 0; //this is used to point at each card position
            //outer loop, using int indexer 
            for (int i = 1; i <=4 ; i++)
            {
                //inner loop, using foreach syntax
                foreach (CardRank r in Enum.GetValues<CardRank>()) { 
                    this._cards[cardPosition++] = new Card { Suit = (CardSuit) i , Rank = r };
                }

            }
            IsInNewDeckOrder = true;
        }
        public Card GetCardByIndex(int index) => _cards[index]; //use a fat arrow to return card by index

        public Card Deal() => GetCardByIndex(currentCard++); //we already have a GetCardByIndex, let's reuse it.

        public Card[] Deal(int howManyCard) //can you write a test for this?
        {
            Card[] cards = new Card[howManyCard]; //ready a new array of card based on how many cards we want
            for (int i = 0; i < howManyCard; i++) {
                cards[i] = this.Deal();
            }
            return cards;
        }

        public void Shuffle() {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int from = random.Next(52);
                int to = random.Next(52);
                (_cards[from], _cards[to]) = (_cards[to], _cards[from]); //tuple
            }
            IsInNewDeckOrder = false;
        }

        public bool Equals(Deck? other)
        {
            if (other == null) return false;
            for (int i = 0; i < NumberOfCards; i++)
            {
                Card a = this.Deal();
                Card b = other.Deal();
                if (a != b) return false;
            }
            return true;
        }
    }
}
