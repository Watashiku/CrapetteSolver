namespace CrapetteSolver.Core;

public class Deck(IRandomService randomService, IEnumerable<Card> initialCards)
{
    private readonly IRandomService _randomService = randomService ?? throw new ArgumentNullException(nameof(randomService));
    private readonly List<Card> _cards = [.. initialCards];
    public IReadOnlyList<Card> Cards => _cards;
    public int Count => _cards.Count;

    public static Deck CreateStandard52CardDeck(IRandomService randomService)
    {
        List<Card> cards = [];
        foreach (var suit in Enum.GetValues<Suit>())
        {
            foreach (var rank in Enum.GetValues<Rank>())
            {
                cards.Add(new Card(rank, suit));
            }
        }
        var deck = new Deck(randomService, cards);
        deck.Shuffle();
        return deck;
    }

    public void Shuffle()
    {
        var n = _cards.Count;
        while (n > 1)
        {
            n--;
            var k = _randomService.Next(n + 1);
            (_cards[n], _cards[k]) = (_cards[k], _cards[n]);
        }
    }

    public Card? DrawCard()
    {
        if (_cards.Count == 0)
        {
            return null;
        }
        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public List<Card> DrawCards(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");
        }

        if (_cards.Count < count)
        {
            throw new InvalidOperationException($"Not enough cards in the deck to draw {count} cards. Requested: {count}, Available: {_cards.Count}.");
        }

        var drawnCards = _cards.Take(count).ToList();
        _cards.RemoveRange(0, count);
        return drawnCards;
    }
}
