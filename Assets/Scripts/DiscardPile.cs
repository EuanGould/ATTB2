using UnityEngine;

public class DiscardPile : MonoBehaviour
{

    private void FixedUpdate()
    {
        PositionCards();
    }

    private void PositionCards()
    {
        float screen_width = 2080;
        Vector2 destinaton = new Vector2(screen_width * -2, 0);

        CardBehaviour[] cards = GetComponentsInChildren<CardBehaviour>();

        for (int i = 0; i < cards.Length; i++)
        {
            //if (cards[i] == GameObject.FindGameObjectWithTag("EnemiesLayer").GetComponent<EnemyManager>().current_card)
            //{
            //    destinaton = GameObject.FindGameObjectWithTag("ActiveZone").GetComponent<RectTransform>().anchoredPosition;
            //}

            cards[i].GetComponent<RectTransform>().anchoredPosition += 5 * Time.fixedDeltaTime * (destinaton - cards[i].GetComponent<RectTransform>().anchoredPosition);

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
