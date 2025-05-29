using CrapetteSolver.Core;
using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.Tests.GameRules.Piles;

public class FoundationPileTests
{
    [Fact]
    public void FoundationPile_ShouldBeInitializedEmpty()
    {
        // Arrange & Act
        var pile = new FoundationPile();

        // Assert
        Assert.True(pile.IsEmpty);
        Assert.Equal(0, pile.Count);
    }

    [Fact]
    public void CanPlaceCard_ShouldAllowPlacingAceWhenPileIsEmpty()
    {
        // Arrange
        var pile = new FoundationPile();
        var ace = new Card(Rank.Ace, Suit.Spades);

        // Act
        var canPlace = pile.CanPlaceCard(ace);

        // Assert
        Assert.True(canPlace);
    }

    [Fact]
    public void CanPlaceCard_ShouldNotAllowPlacingNonAceWhenPileIsEmpty()
    {
        // Arrange
        var pile = new FoundationPile();
        var king = new Card(Rank.King, Suit.Spades);

        // Act
        var canPlace = pile.CanPlaceCard(king);

        // Assert
        Assert.False(canPlace);
    }

    [Fact]
    public void CanPlaceCard_ShouldAllowPlacingNextRankOfSameSuit()
    {
        // Arrange
        var pile = new FoundationPile();
        pile.AddCard(new Card(Rank.Ace, Suit.Clubs));

        var twoOfClubs = new Card(Rank.Two, Suit.Clubs);

        // Act
        var canPlace = pile.CanPlaceCard(twoOfClubs);

        // Assert
        Assert.True(canPlace);
    }

    [Fact]
    public void CanPlaceCard_ShouldNotAllowPlacingDifferentSuit()
    {
        // Arrange
        var pile = new FoundationPile();
        pile.AddCard(new Card(Rank.Ace, Suit.Clubs));
        var twoOfSpades = new Card(Rank.Two, Suit.Spades);

        // Act
        var canPlace = pile.CanPlaceCard(twoOfSpades);

        // Assert
        Assert.False(canPlace);
    }

    [Fact]
    public void CanPlaceCard_ShouldNotAllowPlacingIncorrectRank()
    {
        // Arrange
        var pile = new FoundationPile();
        pile.AddCard(new Card(Rank.Ace, Suit.Clubs));
        var threeOfClubs = new Card(Rank.Three, Suit.Clubs);

        // Act
        var canPlace = pile.CanPlaceCard(threeOfClubs);

        // Assert
        Assert.False(canPlace);
    }
}