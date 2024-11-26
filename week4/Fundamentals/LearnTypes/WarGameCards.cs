using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace Fundamentals.LearnTypes
{
    public class WarGameCards
    {
        //
        //Constructor to initialize 2 batches of 6 Decks Shuffled

        //Make 1 Batch to be In-Play
        //Make Other Batch Stand By

        //Deal method deals 1 card from the In-Play Batch

        //Swap batch at 90% cards dealt

        //Reinitialize the now standby Batch.
        private Card[][] _batches = new Card[2][];
        public Card[][] Batches { get { return _batches; } }
        public Card[] InPlayBatch { get; private set; }
        public Card[] StandbyBatch { get; private set; }

        public event EventHandler SwapBatch;
        private int currentCardPointer = 0;
        public WarGameCards()
        {
            InitializeBatches();
            ShuffleBatch(_batches![0]);
            ShuffleBatch(_batches![1]);
            InPlayBatch = _batches![0];
            StandbyBatch = _batches![1];
            this.SwapBatch += WarGameCards_SwapBatch; //subscribe the method WarGameCards_SwapBatch to the event SwapBatch
        }

        private void WarGameCards_SwapBatch(object? sender, EventArgs e)
        {
            (InPlayBatch, StandbyBatch) = (StandbyBatch, InPlayBatch);
            currentCardPointer = 0;
        }

        public Card Deal() {
            int cardIndex = currentCardPointer++;
            Card nextCard =InPlayBatch[cardIndex];
            if (cardIndex > 0.9* InPlayBatch.Length) {
                SwapBatch?.Invoke(this,new EventArgs()); //this will raise event
            }
            return nextCard;
        }

        private void InitializeBatches()
        {
            _batches[0] = new Card[6 * Deck.NumberOfCards];
            _batches[1] = new Card[6 * Deck.NumberOfCards];
            int cardPosition = 0;
            for (int i = 1; i <= 6; i++)
            {
                Deck aDeck = new Deck();
                Deck anotherDeck = new Deck();

                aDeck.Shuffle();
                anotherDeck.Shuffle();
                for (int j = 1; j <= 52; j++)
                {
                    _batches[0][cardPosition] = aDeck.Deal();
                    _batches[1][cardPosition] = anotherDeck.Deal();
                    cardPosition++;
                }
            }
        }

        private void ShuffleBatch(Card[] cards)
        {
            int numOfCards = cards.Length;
            Random random = new Random();
            for (int i = 0; i < numOfCards*5; i++) {
                int from = random.Next(0, numOfCards);
                int to = random.Next(0, numOfCards);
                (cards[from], cards[to]) = (cards[to], cards[from]);
            }
        }
    }
}