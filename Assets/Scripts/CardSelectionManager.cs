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

    private void ResetHandSelection()
    {
        // call whenever the number of cards in hand changes

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

    public override void OnSingleButtonHeld()
    {
        // called when inputManager detects a held input
        cards_in_hand[currentSelected].Play();
        cards_in_hand.RemoveAt(currentSelected);
        ResetHandSelection();
        cards_in_hand[currentSelected].Select();
    }

    public override void OnSingleButtonTapped()
    {
        // called when inputManager detects a tap input
        cards_in_hand[currentSelected].Deselect();
        currentSelected += 1;
        currentSelected %= cards_in_hand.Count;
        cards_in_hand[currentSelected].Select();
    }

    private List<CardBehaviour> GetHand()
    {
        // gets all cards that are a child of this object
        return new List<CardBehaviour>(GetComponentsInChildren<CardBehaviour>());
    }
}
