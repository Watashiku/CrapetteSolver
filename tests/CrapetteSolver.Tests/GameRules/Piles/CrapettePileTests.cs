using CrapetteSolver.Core;
using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.Tests.GameRules.Piles;

public class CrapettePileTests
{
    [Fact]
    public void CrapettePile_ShouldBeInitializedWithCorrectCards()
    {
        // Arrange
        var initialCards = new List<Card> { new(Rank.Ace, Suit.Spades), new(Rank.King, Suit.Hearts) };

        // Act
        var crapettePile = new CrapettePile(initialCards);

        // Assert
        Assert.Equal(2, crapettePile.Count);
        Assert.Equal(initialCards, [.. crapettePile.Cards]);
    }

    [Fact]
    public void RemoveTopCard_ShouldReturnNullWhenCrapettePileIsEmpty()
    {
        // Arrange
        var crapettePile = new CrapettePile([]);

        // Act
        var card = crapettePile.RemoveTopCard();

        // Assert
        Assert.Null(card);
    }

    [Fact]
    public void RemoveTopCard_ShouldReturnTopCardAndReduceCount()
    {
        // Arrange
        var card1 = new Card(Rank.Two, Suit.Hearts);
        var card2 = new Card(Rank.Three, Suit.Hearts);
        var crapettePile = new CrapettePile([card1, card2]);

        // Act
        var removedCard = crapettePile.RemoveTopCard();

        // Assert
        Assert.Equal(card2, removedCard);
        Assert.Equal(1, crapettePile.Count);
        Assert.Equal(card1, crapettePile.GetTopCard());
    }
}