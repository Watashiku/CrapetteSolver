using CrapetteSolver.Core;
using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.Tests.GameRules.Piles;

public class DrawPileTests
{
    [Fact]
    public void DrawPile_ShouldBeInitializedWithCorrectCards()
    {
        // Arrange
        var initialCards = new List<Card> { new(Rank.Ace, Suit.Spades), new(Rank.King, Suit.Hearts) };

        // Act
        var drawPile = new DrawPile(initialCards);

        // Assert
        Assert.Equal(2, drawPile.Count);
        Assert.Equal(initialCards, drawPile.Cards);
    }

    [Fact]
    public void ReconstituteFromCards_ShouldAddCardsAndClearPrevious()
    {
        // Arrange
        var drawPile = new DrawPile([]);
        var discardCards = new List<Card>
        {
            new(Rank.Two, Suit.Clubs),
            new(Rank.Three, Suit.Diamonds),
            new(Rank.Four, Suit.Hearts)
        };

        // Act
        drawPile.ReconstituteFromCards(discardCards);

        // Assert
        Assert.Equal(3, drawPile.Count);
        Assert.Equal(discardCards, drawPile.Cards);
    }

    [Fact]
    public void ReconstituteFromCards_ShouldThrowInvalidOperationException_WhenPileIsNotEmpty()
    {
        // Arrange
        var drawPile = new DrawPile([new(Rank.Ace, Suit.Spades)]);
        var discardCards = new List<Card> { new(Rank.Two, Suit.Clubs) };

        // Act & Assert
        _ = Assert.Throws<InvalidOperationException>(() => drawPile.ReconstituteFromCards(discardCards));
    }

    [Fact]
    public void RemoveTopCard_ShouldReturnNullWhenDrawPileIsEmpty()
    {
        // Arrange
        var drawPile = new DrawPile([]);

        // Act
        var card = drawPile.RemoveTopCard();

        // Assert
        Assert.Null(card);
    }
}