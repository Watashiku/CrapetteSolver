namespace CrapetteSolver.Core;

public class CardEventArgs(Card card) : EventArgs
{
    public Card Card { get; } = card;
}
