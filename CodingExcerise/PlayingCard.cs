namespace CodingExcerise
{
    public class PlayingCard : IPlayingCard
    {
        public PlayingCard(PlayingCardSuit suit, PlayingCardValue suitValue)
        {
            Suit = suit;
            SuitValue = suitValue;
        }

        public PlayingCardSuit Suit { get; }

        public PlayingCardValue SuitValue { get; }

    }
}