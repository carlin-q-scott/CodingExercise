using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodingExcerise
{
    public class CardDeck : ICardDeck, IEnumerable<IPlayingCard>
    {
        private IPlayingCard[] _currentCardState;

        private readonly int _numSuits;
        private readonly int _suitSize;
        private readonly int _numJokers;

        public int DeckSize => (_numSuits * _suitSize) + _numJokers;
        
        public IEnumerator<IPlayingCard> GetEnumerator()
        {
            return _currentCardState.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// A way to analyze the deck by eye
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var s = String.Join(",\n", _currentCardState.Partition(13).Select(cardSet => String.Join(", ", cardSet.Select(card => $"\n\t{card.SuitValue} of {card.Suit}"))));
            return s;
        }

        public CardDeck(int suits = 4, int suitSize = 13, int numJokers = 0) 
        {
            Guard.IsValid(() => suits, suits, s => s > 0, $"{nameof(suits)} must be greater than zero");
            Guard.IsValid(() => suitSize, suitSize, s => s > 0, $"{nameof(suitSize)} must be greater than zero");
            Guard.IsValid(() => suits, suits, s => s >= 0, $"{nameof(numJokers)} must be greater than or equal to zero");

            _numSuits = suits;
            _suitSize = suitSize;
            _numJokers = numJokers;

            // we know how big the deck is at this point
            var arrayOfCards = new IPlayingCard[DeckSize];
            
            // build the intial state of the array
            var cardIndex = 0;
            for (int suit = 0; suit < suits; suit++)
                for (int suitValue = 1; suitValue < suitSize+1; suitValue++)
                    arrayOfCards[cardIndex++] = new PlayingCard((PlayingCardSuit)suit, (PlayingCardValue)suitValue);
            
            for(int j = 0; j < _numJokers; j++)
                arrayOfCards[cardIndex++] = new PlayingCard(PlayingCardSuit.Joker, PlayingCardValue.Joker);

            _currentCardState = arrayOfCards;

        }

        /// <summary>
        /// Shuffle is really just a "random" sort...
        /// </summary>
        public void Shuffle() => _currentCardState = _currentCardState.OrderBy(c => Guid.NewGuid()).ToArray();
        
        /// <summary>
        /// Sort using linq
        /// </summary>
        public void Sort() => _currentCardState = _currentCardState.OrderBy(c => c.Suit).ThenBy(c => c.SuitValue).ToArray();
    }
}