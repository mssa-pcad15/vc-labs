using Fundamentals.LearnTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalsTest
{
    [TestClass]
    public class WarGameTest
    {
        [TestMethod]
        public void GamePlayThrough() {
            Dealer dealer = new Dealer("Mad Dealer");
            Player player = new Player("Cool Player");
            player.ShouldSurrender = true;
            WarGame aGame = new(dealer);
            //dealer.JoinGame(aGame); // removed dealer.
            player.JoinGame(aGame);
            player.PlaceBet(10);

            Assert.IsTrue(aGame.Winner != null);
            Assert.IsTrue(dealer.Balance + player.Balance == 0);
        }

            
        [TestMethod]
        public void GamePlayThrough10TimesAlwaysSurrender() {
            List<Person> winners = new List<Person>();
            for (int i = 0; i < 100; i++)
            {
                Dealer dealer = new Dealer("Mad Dealer");
                Player player = new Player("Cool Player");
                player.ShouldSurrender = true;
                WarGame aGame = new(dealer);
                //dealer.JoinGame(aGame); // removed dealer.
                player.JoinGame(aGame);
                player.PlaceBet(10);
                Assert.IsTrue(dealer.Balance + player.Balance == 0);
                winners.Add(aGame.Winner);
            }
            Assert.IsTrue(winners.Count == 100);
            int dealerWinCount = winners.Where((Person p) => p is Dealer).Count();
            int playerWinCount = winners.Where(p => p is Player).Count();
            Debug.WriteLine("---------------");
            Debug.WriteLine($"{dealerWinCount} times dealer win.");
            Debug.WriteLine($"{playerWinCount} times player win.");

        }
    }
}
