using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.LearnTypes
{
    class InternalClassSample { }; // access modifier on class under namespace : internal or public
    public class WarGame
    {
        //sealed - marks a member or a class as the end of inheritance.

        //!! black box reuse !!
        private class InnerClass { };//access modifier on nested class can be public, internal, protected, private. Can Not Exceed the visibility of Containing class

        private Dealer _dealer;
        private Player _player;
        private int _wager;
        private WarGameCards _shoe;
         

        public Dealer Dealer { get => _dealer; internal set 
            { 
                _dealer = value;
                this.GameOver += _dealer.OnGameOver;
                DealerReady?.Invoke(this, EventArgs.Empty);
            } }

        public Player Player { get => _player; internal set 
            { 
                _player = value;
                this.GameOver += _player.OnGameOver;
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

        public WarGame(Dealer dealer)
        {
            this.Dealer= dealer;
            dealer.JoinGame(this);

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
                this.Winner = Dealer;
            };

            GotoWar?.Invoke(this, EventArgs.Empty);

        }
    }

    public abstract class Person { //this is base class

        internal List<Card> Hand { get; set; }
        public int Balance { get; protected set; }
        public string Name { get; }

        public Person(string name)
        {
            Name = name;
            this.Hand = new List<Card>();
        }

        public abstract void OnGameOver(object? sender,EventArgs e); // abstract method is a concept that will be enforced in derive class.

        public virtual string HowDoIFee => "Good";
    };

    public class Dealer(string Name) : Person(Name) 
    {
        private WarGame _currentGame;


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
                    this._currentGame.Player.ReceiveWinning(_currentGame.Wager * 2);
                };

                WagerProcessed?.Invoke(this, EventArgs.Empty);
            };
        }
        public override void OnGameOver(object? sender, EventArgs e)
        {
            //should reconcile with the Table Reserve
            Debug.Write("Dealer sees game over");
        }

    }

    public class Player(string Name) : Person(Name)
    {

        private WarGame _currentGame;
        private List<WarGame> _gameHistory= new List<WarGame>();

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

        public override void OnGameOver(object? sender, EventArgs e)
        {
            _gameHistory.Add(_currentGame);
        }
        public override string HowDoIFee => this.Balance>0? "Feels good man.":"Feels bad man.";
    }
}
