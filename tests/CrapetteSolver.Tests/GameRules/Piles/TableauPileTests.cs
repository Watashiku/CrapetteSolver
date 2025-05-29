using CrapetteSolver.Core;
using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.Tests.GameRules.Piles;

public class TableauPileTests
{
    [Fact]
    public void TableauPile_ShouldBeInitializedEmpty()
    {
        // Arrange & Act
        var pile = new TableauPile();

        // Assert
        Assert.True(pile.IsEmpty);
        Assert.Equal(0, pile.Count);
    }

    [Fact]
    public void CanPlaceCard_ShouldAllowPlacingAnyCardWhenPileIsEmpty()
    {
        // Arrange
        var pile = new TableauPile();
        var card = new Card(Rank.Eight, Suit.Diamonds);

        // Act
        var canPlace = pile.CanPlaceCard(card);

        // Assert
        Assert.True(canPlace);
    }

    [Fact]
    public void CanPlaceCard_ShouldAllowPlacingNextLowerRankOfDifferentColor()
    {
        // Arrange
        var pile = new TableauPile();
        pile.AddCard(new Card(Rank.Queen, Suit.Spades));    // Noir
        var cardToPlace = new Card(Rank.Jack, Suit.Hearts); // Rouge

        // Act
        var canPlace = pile.CanPlaceCard(cardToPlace);

        // Assert
        Assert.True(canPlace);
    }

    [Fact]
    public void CanPlaceCard_ShouldNotAllowPlacingSameColor()
    {
        // Arrange
        var pile = new TableauPile();
        pile.AddCard(new Card(Rank.Queen, Suit.Spades));    // Noir
        var cardToPlace = new Card(Rank.Jack, Suit.Clubs);  // Noir

        // Act
        var canPlace = pile.CanPlaceCard(cardToPlace);

        // Assert
        Assert.False(canPlace);
    }

    [Fact]
    public void CanPlaceCard_ShouldNotAllowPlacingIncorrectRank()
    {
        // Arrange
        var pile = new TableauPile();
        pile.AddCard(new Card(Rank.Queen, Suit.Spades));    // Q
        var cardToPlace = new Card(Rank.Ten, Suit.Hearts);  // 10

        // Act
        var canPlace = pile.CanPlaceCard(cardToPlace);

        // Assert
        Assert.False(canPlace);
    }
}