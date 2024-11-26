using Fundamentals.LearnTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalsTest
{
    [TestClass]
    public class PlayingCardsTest
    {
        [TestMethod]
        public void CardToStringPrintsUnicode()
        {
            //Arrange
            Card aceOfSpade = new Card() { Rank = CardRank.Ace, Suit = CardSuit.Spade };
            string expected = "♠1";
            //Act
            string actual = aceOfSpade.ToString();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod] //1
        public void ADeckShouldAlwaysHave52Cards()
        {
            //add a new public class in Fundamentals project called Deck.
            // The class Deck must have a static int member NumberOfCards that is assigned 52

            //Arrange
            int actual = Deck.NumberOfCards;
            int expected = 52;
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod] //2
        public void NewDeckOfCardWillHaveCardsInSuitToRankOrder() //Spade A-13, Heart A-13
        {   // declare private Card[] that has capacity of NumberOfCards

            // in the constructor of Deck class, use 2 loops, where the outer loop enumerate Suite
            // and inner loop enumerate Rank to popuplate the private array

            // lets add a public readonly bool property called IsInNewDeckOrder

            // Add a method called GetCard that returns a Card and accept an int as index of array
            //Arrange
            Deck newDeck = new Deck(); //instantiate a new Deck object, which calls the constructor

            Card actual =  newDeck.GetCardByIndex(0); //Get the first card in the array. !! leaky implementation
            Card expected = new Card { Suit = CardSuit.Spade, Rank = CardRank.Ace }; //first card is ace of spade
            Assert.AreEqual(actual, expected); // two variable of struct type is compared by value, so if two cards of the same suit and rank, they are equal.

            Card actualLast = newDeck.GetCardByIndex(51); //Get the last card in the array. !! leaky implementation
            Card expectedLast = new Card { Suit = CardSuit.Diamond, Rank = CardRank.King }; //last card is king of diamond
            Assert.AreEqual(actualLast, expectedLast);

        }
        [TestMethod] //5
        public void ADeckCanBeShuffled()
        {
            //Arrange
            Deck deckA = new Deck();
            Deck deckB = new Deck();
            Deck deckC = new Deck();
            Deck deckD = new Deck();

            deckA.Shuffle();

            Assert.AreNotEqual(deckA, deckB);// this is ???
            Assert.AreEqual(deckC, deckD);
            Assert.IsFalse(deckA.IsInNewDeckOrder);
            
        }

        [TestMethod] //3
        public void DealACardShouldReturnNextCardInTheDeck()//Should return the next card in Deck
        {
            // initialize a private field to serve as counter to number of cards dealt
            // write an instance method that deals the next card
            // provide a method called Deal() that returns a card according to the counter
            // also provide a get property that indicates how many cards left in the deck

            //Arrange
            Deck deckA = new Deck();
            //Act
            _ = deckA.Deal(); // _ means discard
            _ = deckA.Deal();
            Card thirdCard = deckA.Deal(); //thats the third card
            //Assert
            Card expected = new Card { Rank = CardRank.Three, Suit = CardSuit.Spade };
            Assert.AreEqual(expected, thirdCard);
            Assert.AreEqual(deckA.RemainingCards, 49);
        }
        [TestMethod()] //3B
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CallDealBeyondLastCardShouldThrowIndexOutOfRangeException()//Should return the next card in Deck
        {
            var deckA = new Deck();
            for (int i = 0; i < 53; i++) { _ = deckA.Deal(); }
        }

            [TestMethod] //4
        public void PrintsCardsInDeck() //this is not hard work but busy work.
        {
            //Arrange
        }
    }
}
