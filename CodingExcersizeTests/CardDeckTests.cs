using System;
using System.Collections.Generic;
using System.Linq;
using CodingExcerise;
using FluentAssertions;
using FunctionalExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingExcersizeTests
{
    [TestClass]
    public class CardDeckTests
    {
        #region Test constants
        private const string FullDeckString = @"
        One of Spades,
        Two of Spades,
        Three of Spades,
        Four of Spades,
        Five of Spades,
        Six of Spades,
        Seven of Spades,
        Eight of Spades,
        Nine of Spades,
        Ten of Spades,
        Jack of Spades,
        Queen of Spades,
        King of Spades,

        One of Clubs,
        Two of Clubs,
        Three of Clubs,
        Four of Clubs,
        Five of Clubs,
        Six of Clubs,
        Seven of Clubs,
        Eight of Clubs,
        Nine of Clubs,
        Ten of Clubs,
        Jack of Clubs,
        Queen of Clubs,
        King of Clubs,

        One of Hearts,
        Two of Hearts,
        Three of Hearts,
        Four of Hearts,
        Five of Hearts,
        Six of Hearts,
        Seven of Hearts,
        Eight of Hearts,
        Nine of Hearts,
        Ten of Hearts,
        Jack of Hearts,
        Queen of Hearts,
        King of Hearts,

        One of Diamonds,
        Two of Diamonds,
        Three of Diamonds,
        Four of Diamonds,
        Five of Diamonds,
        Six of Diamonds,
        Seven of Diamonds,
        Eight of Diamonds,
        Nine of Diamonds,
        Ten of Diamonds,
        Jack of Diamonds,
        Queen of Diamonds,
        King of Diamonds";

        private const string FullDeckStringWithJokers = @"

        One of Spades,
        Two of Spades,
        Three of Spades,
        Four of Spades,
        Five of Spades,
        Six of Spades,
        Seven of Spades,
        Eight of Spades,
        Nine of Spades,
        Ten of Spades,
        Jack of Spades,
        Queen of Spades,
        King of Spades,

        One of Clubs,
        Two of Clubs,
        Three of Clubs,
        Four of Clubs,
        Five of Clubs,
        Six of Clubs,
        Seven of Clubs,
        Eight of Clubs,
        Nine of Clubs,
        Ten of Clubs,
        Jack of Clubs,
        Queen of Clubs,
        King of Clubs,

        One of Hearts,
        Two of Hearts,
        Three of Hearts,
        Four of Hearts,
        Five of Hearts,
        Six of Hearts,
        Seven of Hearts,
        Eight of Hearts,
        Nine of Hearts,
        Ten of Hearts,
        Jack of Hearts,
        Queen of Hearts,
        King of Hearts,

        One of Diamonds,
        Two of Diamonds,
        Three of Diamonds,
        Four of Diamonds,
        Five of Diamonds,
        Six of Diamonds,
        Seven of Diamonds,
        Eight of Diamonds,
        Nine of Diamonds,
        Ten of Diamonds,
        Jack of Diamonds,
        Queen of Diamonds,
        King of Diamonds,
        Joker of Joker,
        Joker of Joker";

        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_BadParameters_ThrowsExceptions()
        {
            var deck = new CardDeck(-2,-45, -3);
            // should throw argument exceptions
        }

        [TestMethod]
        public void Constructor_AllDefaultParameters_CreatesAStandard52CardDeck()
        {
            var deck = new CardDeck();
            deck.DeckSize.Should().Be(52);
        }

        [TestMethod]
        public void Constructor_AllDefaultParameters_PlayingWithAFullDeck()
        {
            var deck = new CardDeck();
            deck.ToString().RemoveAllWhiteSpace().ShouldBeEquivalentTo(FullDeckString.RemoveAllWhiteSpace());
        }

        [TestMethod]
        public void Constructor_AllDefaultParametersAndJokers_PlayingWithAFullDeckContainingJokers()
        {
            var deck = new CardDeck(numJokers:2);
            deck.ToString().RemoveAllWhiteSpace().ShouldBeEquivalentTo(FullDeckStringWithJokers.RemoveAllWhiteSpace());
        }

        [TestMethod]
        public void Shuffle_GivenCurrentState_ShouldNeverBeInitialState()
        {
            var deck = new CardDeck();
            
            deck.Shuffle();

            deck.ToString().RemoveAllWhiteSpace().Should().NotBe(FullDeckString.RemoveAllWhiteSpace());
        }

        // complicated for a test but how do you test a shuffle?
        [TestMethod]
        public void Shuffle_MultipleShuffles_ShouldYieldDifferentResultsEachTime()
        {
            
            var previousShuffles = new List<string>();
            var deck = new CardDeck();
            
            var numShuffles = 100;

            for (int i = 0; i < numShuffles; i++)
            {
                deck.Shuffle();
                previousShuffles.Add(deck.ToString().RemoveAllWhiteSpace());
            }
            
            previousShuffles.ForEach(s => s.Dump());

            // no previous shuffle should contain the shuffle result I'm looking at
            previousShuffles.All(ps => !previousShuffles.ExceptOne(ps).Any(p => p.Equals(ps) ) )
                .Should()
                .BeTrue("Shuffling should yield different results every time");
        }

        [TestMethod]
        public void Sort_GivenShuffledState_ShouldReturnInitialState()
        {
            var deck = new CardDeck();

            deck.Shuffle();
            deck.Sort();

            deck.ToString().RemoveAllWhiteSpace().ShouldBeEquivalentTo(FullDeckString.RemoveAllWhiteSpace());
        }



    }
}
