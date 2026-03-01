using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : InputtableBehaviour
{
    public CardBehaviour current_card;
    
    private enum State
    {
        Selecting,
        Watching
    }

    // internal variables
    private int currentSelected = 0;

    private List<EnemyBehaviour> enemies_in_fight;

    State state = State.Selecting;

    void Awake()
    {
        enemies_in_fight = GetEnemies();
        ResetEnemySelection();
        enemies_in_fight[currentSelected].Deselect();
    }

    public override void OnSingleButtonHeld()
    {
        if (state == State.Selecting)
        {
            current_card.targetPayoff(enemies_in_fight[currentSelected]);
            GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().inputtable = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>();
            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ResetHandSelection();
            enemies_in_fight[currentSelected].Deselect();
        }
    }

    private void SanitiseCurrentSelection()
    {
        if (enemies_in_fight.Count <= currentSelected)
        {
            currentSelected = enemies_in_fight.Count - 1;
        }
        else if (currentSelected < 0)
        {
            currentSelected = 0;
        }

        enemies_in_fight[currentSelected].Select();
    }

    public void ResetEnemySelection()
    {
        if (new List<EnemyBehaviour>(GetComponentsInChildren<EnemyBehaviour>()).Count == 0)
        {
            Debug.Log("VICTORY");
        }

        // call whenever the number of enemies changes
        enemies_in_fight = GetEnemies();

        SanitiseCurrentSelection();
    }

    public override void OnSingleButtonTapped()
    {
        if (state == State.Selecting)
        {
            // called when inputManager detects a tap input
            enemies_in_fight[currentSelected].Deselect();
            currentSelected += 1;
            currentSelected %= enemies_in_fight.Count;
            enemies_in_fight[currentSelected].Select();

            ResetEnemySelection();
        }
    }

    private List<EnemyBehaviour> GetEnemies()
    {
        // gets all enemies that are a child of this object
        return new List<EnemyBehaviour>(GetComponentsInChildren<EnemyBehaviour>());

    }
}
