using UnityEngine;

public class DiscardPile : MonoBehaviour
{

    private void FixedUpdate()
    {
        PositionCards();
    }

    private void PositionCards()
    {
        float screen_size = Screen.width;
        CardBehaviour[] cards = GetComponentsInChildren<CardBehaviour>();

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(screen_size * -2, screen_size * -2);
        }
    }

    public void ShuffleIntoDeck()
    {
        CardBehaviour[] cards = GetComponentsInChildren<CardBehaviour>();
        GameObject deck_pile = GameObject.FindGameObjectWithTag("DeckPile");

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<RectTransform>().SetParent(deck_pile.GetComponent<RectTransform>());
        }
    }
}
