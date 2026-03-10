using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : InputtableBehaviour
{
    public CardBehaviour current_card;

    [SerializeField] private List<GameObject> waves;

    [SerializeField] private GameObject enemy_graveyard;

    private GameObject enemies_in_wait;
    private int wave_index = 0;

    private enum State
    {
        Selecting,
        Watching
    }

    // internal variables
    private int currentSelected = 0;
    private int cardsOffered = 0;

    private List<EnemyBehaviour> enemies_in_fight;

    State state = State.Selecting;

    void Awake()
    {
        enemies_in_fight = GetEnemies();
        ResetEnemySelection();
        enemies_in_fight[currentSelected].Deselect();
        PositionEnemies();

        enemies_in_wait = waves[wave_index];
    }

    public override void OnSingleButtonHeld()
    {
        if (state == State.Selecting)
        {
            state = State.Watching;
            Invoke("InvokableTargetPayoff", 0.2f);
        }
    }

    private void InvokableTargetPayoff()
    {
        current_card.targetPayoff(enemies_in_fight[currentSelected]);
        GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().inputtable = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>();
        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ResetHandSelection();
        if (enemies_in_fight.Count > currentSelected)
        {
            enemies_in_fight[currentSelected].Deselect();
        }
        current_card = null;
        state = State.Selecting;
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

    private void PositionEnemies()
    {
        float screen_size = 2080;
        float gap = screen_size * 0.7f / enemies_in_fight.Count;

        for (int i = 0; i < enemies_in_fight.Count; i++)
        {
            enemies_in_fight[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(-screen_size * 0.35f + gap * (i + 0.5f), -320);
        }
    }

    public void EnemyDeath(EnemyBehaviour enemy)
    {
        enemy.InvokeDeath(0.3f);
        enemy.gameObject.transform.SetParent(enemy_graveyard.transform);

        if (enemies_in_fight.Count <= 1)
        {
            cardsOffered = 0;
            GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ShuffleAwayAll();
            GameObject.FindGameObjectWithTag("CardAddingUI").GetComponent<CardAddingUIBehaviour>().InvokeOfferChoice();
        }

        enemies_in_fight = GetEnemies();
    }

    public void DeathCheckAll()
    {
        foreach (EnemyBehaviour enemy in GetComponentsInChildren<EnemyBehaviour>())
        {
            enemy.DeathCheck();
        }
    }

    public void GoNext()
    {
        cardsOffered++;

        if (cardsOffered >= 3)
        {
            NextWave();
        }
        else
        {
            GameObject.FindGameObjectWithTag("CardAddingUI").GetComponent<CardAddingUIBehaviour>().InvokeOfferChoice();
        }
    }

    private void NextWave()
    {
        foreach (EnemyBehaviour new_enemy in enemies_in_wait.GetComponentsInChildren<EnemyBehaviour>())
        {
            new_enemy.transform.SetParent(transform);
            new_enemy.OnSpawning();
        }

        wave_index++;
        enemies_in_wait = waves[wave_index];

        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>().ResetPlayerStats();

        enemies_in_fight = GetEnemies();
        PositionEnemies();

        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().DrawFreshHand();
    }
}
