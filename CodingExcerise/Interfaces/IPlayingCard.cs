namespace CodingExcerise
{
    public interface IPlayingCard
    {
        PlayingCardSuit Suit { get; }

        PlayingCardValue SuitValue { get; } //CardValue, not SuitValue

    }
}