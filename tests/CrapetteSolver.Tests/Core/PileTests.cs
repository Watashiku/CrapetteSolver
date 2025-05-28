using CrapetteSolver.Core;

namespace CrapetteSolver.Tests.Core;

public class PileTests
{
    [Fact]
    public void Pile_ShouldBeInitializedEmptyByDefault()
    {
        // Arrange & Act
        var pile = new TestablePile();

        // Assert
        Assert.Equal(0, pile.Count);
        Assert.True(pile.IsEmpty);
        Assert.Empty(pile.Cards);
    }

    [Fact]
    public void AddCard_ShouldIncreaseCountAndAddCardToTop()
    {
        // Arrange
        var pile = new TestablePile();
        var card = new Card(Rank.Queen, Suit.Clubs);

        // Act
        pile.AddCard(card);

        // Assert
        Assert.Equal(1, pile.Count);
        Assert.False(pile.IsEmpty);
        Assert.Equal(card, pile.GetTopCard());
    }

    [Fact]
    public void AddCard_ShouldInvokeCardAddedEvent()
    {
        // Arrange
        var pile = new TestablePile();
        var card = new Card(Rank.Jack, Suit.Diamonds);
        Card? eventArgsCard = null;
        pile.CardAdded += (sender, c) => eventArgsCard = c.Card;

        // Act
        pile.AddCard(card);

        // Assert
        Assert.Equal(card, eventArgsCard);
    }

    [Fact]
    public void RemoveTopCard_ShouldReduceCountAndReturnTopCard()
    {
        // Arrange
        var card1 = new Card(Rank.Two, Suit.Hearts);
        var card2 = new Card(Rank.Three, Suit.Hearts);
        var pile = new TestablePile();
        pile.AddCard(card1);
        pile.AddCard(card2);

        // Act
        var removedCard = pile.RemoveTopCard();

        // Assert
        Assert.Equal(card2, removedCard);
        Assert.Equal(1, pile.Count);
        Assert.Equal(card1, pile.GetTopCard());
    }

    [Fact]
    public void RemoveTopCard_ShouldReturnNullWhenPileIsEmpty()
    {
        // Arrange
        var pile = new TestablePile();

        // Act
        var removedCard = pile.RemoveTopCard();

        // Assert
        Assert.Null(removedCard);
        Assert.True(pile.IsEmpty);
    }

    [Fact]
    public void RemoveTopCard_ShouldInvokeCardRemovedEvent()
    {
        // Arrange
        var card = new Card(Rank.Four, Suit.Spades);
        var pile = new TestablePile();
        pile.AddCard(card);
        Card? eventArgsCard = null;
        pile.CardRemoved += (sender, c) => eventArgsCard = c.Card;

        // Act
        _ = pile.RemoveTopCard();

        // Assert
        Assert.Equal(card, eventArgsCard);
    }

    [Fact]
    public void GetTopCard_ShouldReturnTopCardWithoutRemovingIt()
    {
        // Arrange
        var card1 = new Card(Rank.Five, Suit.Clubs);
        var card2 = new Card(Rank.Six, Suit.Clubs);
        var pile = new TestablePile();
        pile.AddCard(card1);
        pile.AddCard(card2);

        // Act
        var topCard = pile.GetTopCard();

        // Assert
        Assert.Equal(card2, topCard);
        Assert.Equal(2, pile.Count);
    }

    [Fact]
    public void GetTopCard_ShouldReturnNullWhenPileIsEmpty()
    {
        // Arrange
        var pile = new TestablePile();

        // Act
        var topCard = pile.GetTopCard();

        // Assert
        Assert.Null(topCard);
    }

    [Fact]
    public void Clear_ShouldRemoveAllCardsAndSetCountToZero()
    {
        // Arrange
        var pile = new TestablePile();
        pile.AddCard(new Card(Rank.Seven, Suit.Diamonds));
        pile.AddCard(new Card(Rank.Eight, Suit.Diamonds));

        // Act
        pile.Clear();

        // Assert
        Assert.Equal(0, pile.Count);
        Assert.True(pile.IsEmpty);
    }

    [Fact]
    public void Clear_ShouldInvokePileClearedEvent()
    {
        // Arrange
        var pile = new TestablePile();
        pile.AddCard(new Card(Rank.Nine, Suit.Hearts));
        var eventFired = false;
        pile.PileCleared += (sender, args) => eventFired = true;

        // Act
        pile.Clear();

        // Assert
        Assert.True(eventFired);
    }
}