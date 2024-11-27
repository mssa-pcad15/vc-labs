using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.LearnTypes
{
    public class WarGame
    {
        private Dealer _dealer;
        private Player _player;
        private int _wager;
        private WarGameCards _shoe;


        public Dealer Dealer { get => _dealer; internal set 
            { 
                _dealer = value;
                DealerReady?.Invoke(this, EventArgs.Empty);
            } }

        public Player Player { get => _player; internal set 
            { 
                _player = value;
                PlayerReady?.Invoke(this, EventArgs.Empty);
            } }

        private Person _winner;

        public Person Winner
        {
            get { return _winner; }
            set { _winner = value;
                HasWinner?.Invoke(this, value);
            }
        }

        public int Wager
        {
            get { return _wager; }
            set {
                if (Player == null || Dealer == null) throw new Exception("Missing participant");
                _wager = value;
                PlacedWager?.Invoke(this, EventArgs.Empty);
            }
        }

        public string GameResult { get; private set; }

        public event EventHandler<EventArgs> GameInitialized;
        public event EventHandler<EventArgs> DealerReady;
        public event EventHandler<EventArgs> PlayerReady;
        public event EventHandler<EventArgs> PlacedWager;
        public event EventHandler<EventArgs> HandDealt;
        public event EventHandler<Person> HasWinner;
        public event EventHandler<EventArgs> GotoWar;
        public event EventHandler<EventArgs> GameOver;

        public WarGame()
        {
            this._shoe = new WarGameCards(); //in future revision, the shoe would be a reference to Table's Property
            Dealer.LetsGo += (o, e) =>
            {
                Dealer.Hand.Add(this._shoe.Deal());
                Player.Hand.Add(this._shoe.Deal());
                HandDealt?.Invoke(this, EventArgs.Empty);
            };
            Dealer.WagerProcessed += (o, e) =>
            {
                string result = string.Empty;
                if (_winner is Dealer d)
                {
                    result = $"House win, dealer {Dealer.Name} has balance of {Dealer.Balance}, player {Player.Name} has balance of {Player.Balance}";
                }
                else
                {
                    result = $"Player win, dealer {Dealer.Name} has balance of {Dealer.Balance}, player {Player.Name} has balance of {Player.Balance}";
                }
                this.GameResult = result;
                GameOver?.Invoke(this, EventArgs.Empty);
            };

            GameInitialized?.Invoke(this, EventArgs.Empty);
        }

        internal void War()
        {
            Player.DoubleDown += (o, e) =>
            {
                Dealer.Hand.Add(_shoe.Deal());
                Player.Hand.Add(_shoe.Deal());
                Wager = Wager * 2;
                HandDealt?.Invoke(this,EventArgs.Empty);
            };
            Player.Surrender += (o, e) =>
            {
                HasWinner?.Invoke(this, Dealer);
            };

            GotoWar?.Invoke(this, EventArgs.Empty);

        }
    }

    public class Person { 

        internal List<Card> Hand { get; set; }
        public string Name { get; }

        public Person(string name)
        {
            Name = name;
            this.Hand = new List<Card>();
        }
    };

    public class Dealer(string Name) : Person(Name) 
    {
        private WarGame _currentGame;

        public int Balance { get; private set; }

        public event EventHandler<EventArgs> LetsGo;
        public event EventHandler<EventArgs> WagerProcessed;

        public void JoinGame(WarGame theGame) {
            theGame.Dealer = this;
            this._currentGame = theGame;
            this._currentGame.PlacedWager += (o,e) => LetsGo?.Invoke(this, EventArgs.Empty);
            this._currentGame.HandDealt += (o, e) =>
            {
                Card dealerCard = this._currentGame.Dealer.Hand.Last();
                Card playerCard = this._currentGame.Player.Hand.Last();
                if (dealerCard.Rank == playerCard.Rank) this._currentGame.War();
                if (dealerCard.Rank > playerCard.Rank) this._currentGame.Winner = this;
                if (dealerCard.Rank < playerCard.Rank) this._currentGame.Winner = _currentGame.Player;
            };
            this._currentGame.HasWinner += (o, winner) =>
            {
                if (winner is Dealer d) { this.Balance += _currentGame.Wager; } 
                else { 
                    this.Balance -= _currentGame.Wager;
                    this._currentGame.Player.ReceiveWinning(_currentGame.Wager*2);
                };

                WagerProcessed?.Invoke(this, EventArgs.Empty);
            };
        }
    }

    public class Player(string Name) : Person(Name)
    {
        private WarGame _currentGame;
        public int Balance { get; private set; }

        public event EventHandler<EventArgs> DoubleDown;
        public event EventHandler<EventArgs> Surrender;

        public bool ShouldSurrender = false;
        public void JoinGame(WarGame theGame)
        {
            theGame.Player = this;
            this._currentGame=theGame;
            this._currentGame.GotoWar += (o, e) =>
            {
                if (ShouldSurrender)
                {
                    Surrender?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    this.Balance -= _currentGame.Wager;
                    DoubleDown?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public void PlaceBet(int wager)
        {
            this.Balance -= wager;
            this._currentGame.Wager = wager;
        }
        public void ReceiveWinning(int winning) {
            this.Balance += winning;
        }
    }
}
