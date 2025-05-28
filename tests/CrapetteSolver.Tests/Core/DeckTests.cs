using CrapetteSolver.Core;
using CrapetteSolver.Tests.TestHelpers;

namespace CrapetteSolver.Tests.Core;

public class DeckTests
{
    [Fact] // XUnit attribute for a test method
    public void CreateStandard52CardDeck_ShouldContain52UniqueCards()
    {
        // Arrange
        var randomService = new TestRandomService(0);

        // Act
        var deck = Deck.CreateStandard52CardDeck(randomService);

        // Assert
        Assert.NotNull(deck);
        Assert.Equal(52, deck.Count);

        var distinctCards = deck.Cards.Distinct().ToList();
        Assert.Equal(52, distinctCards.Count);

        foreach (var suit in Enum.GetValues<Suit>())
        {
            foreach (var rank in Enum.GetValues<Rank>())
            {
                Assert.Contains(new Card(rank, suit), distinctCards);
            }
        }
    }

    [Fact]
    public void Shuffle_ShouldReorderCardsButMaintainContent()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var shuffleSequence = new TestRandomService(51, 0, 50, 1, 0, 1);

        // Act
        var initialOrderForShuffleTest = Deck.CreateStandard52CardDeck(randomService);
        var deckForShuffleTest = Deck.CreateStandard52CardDeck(randomService);

        deckForShuffleTest.Shuffle();

        // Assert
        Assert.Equal(initialOrderForShuffleTest.Cards.OrderBy(c => c.ToString()), deckForShuffleTest.Cards.OrderBy(c => c.ToString()));
        Assert.Equal(52, deckForShuffleTest.Count);

        Assert.NotEqual(initialOrderForShuffleTest.Cards, deckForShuffleTest.Cards);
    }

    [Fact]
    public void DrawCard_ShouldReturnNullWhenDeckIsEmpty()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var emptyDeck = new Deck(randomService, []);

        // Act
        var card = emptyDeck.DrawCard();

        // Assert
        Assert.Null(card);
        Assert.Equal(0, emptyDeck.Count);
    }

    [Fact]
    public void DrawCard_ShouldReturnTopCardAndReduceCount()
    {
        // Arrange
        var card1 = new Card(Rank.Ace, Suit.Spades);
        var card2 = new Card(Rank.King, Suit.Hearts);
        var initialCards = new List<Card> { card1, card2 };
        var randomService = new TestRandomService(0);
        var deck = new Deck(randomService, initialCards);

        // Act
        var drawnCard = deck.DrawCard();

        // Assert
        Assert.NotNull(drawnCard);
        Assert.Equal(card1, drawnCard);
        Assert.Equal(1, deck.Count);
        Assert.Equal(card2, deck.Cards[0]);
    }

    [Fact]
    public void DrawCards_ShouldThrowArgumentOutOfRangeException_WhenCountIsNegative()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var deck = Deck.CreateStandard52CardDeck(randomService);

        // Act & Assert
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => deck.DrawCards(-1));
    }

    [Fact]
    public void DrawCards_ShouldThrowInvalidOperationException_WhenNotEnoughCards()
    {
        // Arrange
        var card1 = new Card(Rank.Ace, Suit.Spades);
        var initialCards = new List<Card> { card1 };
        var randomService = new TestRandomService(0);
        var deck = new Deck(randomService, initialCards);

        // Act & Assert
        _ = Assert.Throws<InvalidOperationException>(() => deck.DrawCards(2));
    }

    [Fact]
    public void DrawCards_ShouldReturnCorrectNumberOfCardsAndReduceCount()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var deck = Deck.CreateStandard52CardDeck(randomService);
        var expectedCards = deck.Cards.Take(5).ToList();

        // Act
        var drawnCards = deck.DrawCards(5);

        // Assert
        Assert.Equal(5, drawnCards.Count);
        Assert.Equal(expectedCards, drawnCards);
        Assert.Equal(52 - 5, deck.Count);
    }
}