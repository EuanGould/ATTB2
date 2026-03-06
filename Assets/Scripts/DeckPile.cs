using UnityEngine;

public class DeckPile : MonoBehaviour
{
    private void FixedUpdate()
    {
        PositionCards();
    }

    private void Awake()
    {
        InitialPositionCards();
    }

    private void PositionCards()
    {
        float screen_width = 2080;
        Vector2 destinaton = new Vector2(screen_width * 2, 0);

        CardBehaviour[] cards = GetComponentsInChildren<CardBehaviour>();

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<RectTransform>().anchoredPosition += 5 * Time.fixedDeltaTime * (destinaton - cards[i].GetComponent<RectTransform>().anchoredPosition);
        }
    }

    private void InitialPositionCards()
    {
        float screen_width = 2080;
        Vector2 destinaton = new Vector2(screen_width * 2, 0);

        CardBehaviour[] cards = GetComponentsInChildren<CardBehaviour>();

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<RectTransform>().anchoredPosition = destinaton;
        }
    }

    public void DrawCard()
    {
        
        if (GetComponentsInChildren<CardBehaviour>().Length == 0)
        {
            GameObject.FindGameObjectWithTag("DiscardPile").GetComponent<DiscardPile>().ShuffleIntoDeck();
        }

        CardBehaviour[] cards = GetComponentsInChildren<CardBehaviour>();
        cards[Random.Range(0, cards.Length - 1)].GetComponent<RectTransform>().SetParent(GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<RectTransform>());

        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ResetHandSelection();
    }
}
