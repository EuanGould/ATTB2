using UnityEngine;

public class DeckPile : MonoBehaviour
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
            cards[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(screen_size * 2, screen_size * -2);
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
