using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;

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
    
    State state = State.Selecting;

    void Awake()
    {
        cards_in_hand = GetHand();
        DrawFreshHand();
    }

    private void FixedUpdate()
    {
        PositionCards();
    }

    private void PositionCards()
    {
        float screen_size = 2080;
        float gap = screen_size * 0.7f / cards_in_hand.Count;

        for (int i = 0; i < cards_in_hand.Count; i++)
        {
            cards_in_hand[i].GetComponent<RectTransform>().anchoredPosition += 5 * Time.fixedDeltaTime * ((new Vector2(-screen_size * 0.35f + gap * (i + 0.5f), 150 + cards_in_hand[i].GetYOffset())) - cards_in_hand[i].GetComponent<RectTransform>().anchoredPosition);
        }
    }

    public void DeselectAll()
    {
        cards_in_hand[currentSelected].Deselect();
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
        if (new List<CardBehaviour>(GetComponentsInChildren<CardBehaviour>()).Count == 0 && state == State.Selecting)
        {
            GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>().addTotalTime(5);
            GameObject.FindGameObjectWithTag("DeckPile").GetComponent<DeckPile>().DrawCard();
        }

        // call whenever the number of cards in hand changes
        cards_in_hand = GetHand();
        
        SanitiseCurrentSelection();
    }

    public void DrawNewCard()
    {
        GameObject.FindGameObjectWithTag("DeckPile").GetComponent<DeckPile>().DrawCard();
        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().UpdateDeckAndDiscardPileText();
    }

    public void FinishResolution()
    {
        cards_in_hand.RemoveAt(currentSelected);
        SanitiseCurrentSelection();
        state = State.Selecting;
    }

    public override void OnSingleButtonHeld()
    {
        if (state == State.Selecting)
        {
            // called when inputManager detects a held input
            state = State.Watching;
            cards_in_hand[currentSelected].RunCost();
            cards_in_hand[currentSelected].InvokePlay();
        }
    }

    public override void OnSingleButtonTapped()
    {
        if (state == State.Selecting)
        {
            // called when inputManager detects a tap input
            cards_in_hand[currentSelected].Deselect();
            currentSelected += 1;
            currentSelected %= cards_in_hand.Count;
            cards_in_hand[currentSelected].Select();

            ResetHandSelection();
        }
    }

    private List<CardBehaviour> GetHand()
    {

        // gets all cards that are a child of this object
        return new List<CardBehaviour>(GetComponentsInChildren<CardBehaviour>());
    }

    public void ResetDeck()
    {
        state = State.Watching;
        ShuffleAwayAll();

        foreach(CardBehaviour card in GameObject.FindGameObjectWithTag("DeckPile").GetComponentsInChildren<CardBehaviour>())
        {
            card.ResetValues();
        }
    }

    public void ShuffleAwayAll()
    {
        foreach (CardBehaviour card in cards_in_hand)
        {
            card.Deselect();
            card.transform.SetParent(GameObject.FindGameObjectWithTag("DeckPile").transform);
        }

        foreach (CardBehaviour card in GameObject.FindGameObjectWithTag("DiscardPile").transform.GetComponentsInChildren<CardBehaviour>())
        {
            card.Deselect();
            card.transform.SetParent(GameObject.FindGameObjectWithTag("DeckPile").transform);
        }

    }

    public void UpdateDeckAndDiscardPileText()
    {
        TextMeshProUGUI deckPileText = GameObject.FindGameObjectWithTag("DeckPileText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI discardPileText = GameObject.FindGameObjectWithTag("DiscardPileText").GetComponent<TextMeshProUGUI>(); ;

        deckPileText.text = GameObject.FindGameObjectWithTag("DeckPile").transform.childCount.ToString() + " Cards in Deck";
        discardPileText.text = GameObject.FindGameObjectWithTag("DiscardPile").transform.childCount.ToString() + " Cards in Discard";
    }

    public void DrawFreshHand(int amount = 5)
    {
        ShuffleAwayAll();

        cards_in_hand = GetHand();

        for (int i = 0; i < amount; i++)
        {
            DrawNewCard();
        }

        UpdateDeckAndDiscardPileText();
    }

    public bool getWatching()
    {
        return state == State.Watching;
    }

    public void setWatching(bool watching)
    {
        if (watching)
        {
            state = State.Watching;
        }
        else
        {
            state = State.Selecting;
        }
    }
}
