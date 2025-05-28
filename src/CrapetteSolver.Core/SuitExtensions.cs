namespace CrapetteSolver.Core;

public static class SuitExtensions
{
    public static bool IsRed(this Suit suit) => suit is Suit.Hearts or Suit.Diamonds;
    public static bool IsBlack(this Suit suit) => suit is Suit.Clubs or Suit.Spades;
}