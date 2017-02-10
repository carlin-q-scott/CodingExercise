using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodingExcerise
{
    public class CardDeck : ICardDeck
    {
        private List<IPlayingCard> _currentCardState;
        private Random _random; //Todo: how random would two different decks be when compared to eachother?
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
               
            // try to randomize the seed a bit each time
            var guidSeed = Regex.Replace(Guid.NewGuid().ToString("N"), "[a-z]*", string.Empty)
                                .Substring(0, int.MaxValue.ToString().Length / 2);

            _random = new Random(int.Parse(guidSeed));

            _numSuits = suits;
            _suitSize = suitSize;
            _numJokers = numJokers;

            // we know how big the deck is at this point
            var cards = new List<IPlayingCard>(DeckSize);
            
            // build the intial state of the array
            for (int suit = 0; suit < suits; suit++)
                for (int suitValue = 1; suitValue < suitSize+1; suitValue++)
                    cards.Add(new PlayingCard((PlayingCardSuit)suit, (PlayingCardValue)suitValue));
            
            for(int j = 0; j < _numJokers; j++)
                cards.Add(new PlayingCard(PlayingCardSuit.Joker, PlayingCardValue.Joker));

            _currentCardState = cards;

        }

        //Todo: What are the trade-offs between this and fisher-yates shuffle?
        /// <summary>
        /// Shuffle is really just a "random" sort...
        /// </summary>
        public void Shuffle() => _currentCardState = _currentCardState.OrderBy(c => _random.Next()).ToList();
        
        /// <summary>
        /// Sort using linq
        /// </summary>
        public void Sort() => _currentCardState = _currentCardState.OrderBy(c => c.Suit).ThenBy(c => c.SuitValue).ToList();

    }
}