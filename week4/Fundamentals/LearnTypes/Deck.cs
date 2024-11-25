using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.LearnTypes
{
    public class Deck
    {
        //public static int NumberOfCards = 52;
        public const int NumberOfCards = 52; //either one works, what is the difference?
        public bool IsInNewDeckOrder { get; } //flag indicates whether the deck had been shuffled
        public int RemainingCards => NumberOfCards - currentCard; //RemainingCards is simply NumberOfCards-currentCards

        private Card[] _cards = new Card[NumberOfCards]; // private array of Card
        private int currentCard = 0;
        
        

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
        public Card GetCardByIndex(int index) => _cards[index]; //use a fatarrow to retunr card by index

        public Card Deal() => GetCardByIndex(currentCard++); //we already have a GetCardByIndex, let's reuse it.


    }
}
