namespace CrapetteSolver.Core;

public class Hand
{
    private readonly List<Card> _cards;

    public Hand() => _cards = [];

    public void AddCard(Card card) => _cards.Add(card);

    public void AddCards(IEnumerable<Card> cards) => _cards.AddRange(cards);

    public bool RemoveCard(Card card) => _cards.Remove(card);

    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

    public int Count => _cards.Count;
}