namespace CrapetteSolver.Core;

public record Card(Rank Rank, Suit Suit)
{
    public override string ToString() => $"{Rank} of {Suit}";
}
