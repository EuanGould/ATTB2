using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CardSelectionManager : InputtableBehaviour
{

    // internal variables
    private int currentSelected = 0;

    private List<CardBehaviour> cards_in_hand;

    private enum State
    {
        Selecting,
        Watching
    }
    
    void Awake()
    {
        cards_in_hand = GetHand();
        ResetHandSelection();
    }

    private void PositionCards()
    {
        float screen_size = Screen.width;
        float gap = screen_size * 0.7f / cards_in_hand.Count;

        for (int i = 0; i < cards_in_hand.Count; i++)
        {
            cards_in_hand[i].GetComponent<RectTransform>().anchoredPosition = new Vector2 (-screen_size * 0.35f + gap * (i + 0.5f), 150 + cards_in_hand[i].GetYOffset());
        }
    }

    private void SanitiseCurrentSelection()
    {
        if (new List<CardBehaviour>(GetComponentsInChildren<CardBehaviour>()).Count == 0)
        {
            GameObject.FindGameObjectWithTag("DeckPile").GetComponent<DeckPile>().DrawCard();
        }

        if (cards_in_hand.Count <= currentSelected)
        {
            currentSelected = cards_in_hand.Count - 1;
        }
        else if (currentSelected < 0)
        {
            currentSelected = 0;
        }

        cards_in_hand[currentSelected].Select();
    }

    public void ResetHandSelection()
    {
        if (new List<CardBehaviour>(GetComponentsInChildren<CardBehaviour>()).Count == 0)
        {
            GameObject.FindGameObjectWithTag("DeckPile").GetComponent<DeckPile>().DrawCard();
        }

        // call whenever the number of cards in hand changes
        cards_in_hand = GetHand();
        
        SanitiseCurrentSelection();
        PositionCards();
    }

    public override void OnSingleButtonHeld()
    {
        // called when inputManager detects a held input
        cards_in_hand[currentSelected].Play();
        cards_in_hand.RemoveAt(currentSelected);
        SanitiseCurrentSelection();
        PositionCards();
        cards_in_hand[currentSelected].Select();
    }

    public override void OnSingleButtonTapped()
    {
        // called when inputManager detects a tap input
        cards_in_hand[currentSelected].Deselect();
        currentSelected += 1;
        currentSelected %= cards_in_hand.Count;
        cards_in_hand[currentSelected].Select();

        ResetHandSelection();
    }

    private List<CardBehaviour> GetHand()
    {

        // gets all cards that are a child of this object
        return new List<CardBehaviour>(GetComponentsInChildren<CardBehaviour>());
    }
}
