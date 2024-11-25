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
            string expected = "♠11";
            //Act
            string actual = aceOfSpade.ToString();
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod] //1
        public void ADeckShouldAlwaysHave52Cards()
        {
            //Arrange
        }

        [TestMethod] //2
        public void NewDeckOfCardWillHaveCardsInSuitToRankOrder() //Spade A-13, Heart A-13
        {
            //Arrange
        }
        [TestMethod] //5
        public void ADeckCanBeShuffled()
        { 
            //Arrange
        }

        [TestMethod] //3
        public void DealACardShouldReturnNextCardInTheDeck()//Should return the next card in Deck
        {
            //Arrange
        }

        [TestMethod] //4
        public void PrintsCardsInDeck() //
        {
            //Arrange
        }
    }
}
