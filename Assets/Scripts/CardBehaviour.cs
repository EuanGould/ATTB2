using Unity.VisualScripting;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    // adjustable
    [SerializeField] float enlarge_factor = 1.2f;
    [SerializeField] float vertical_offset_on_selection = 200f;
    [SerializeField] int time_cost = 0;

    // internal
    private bool selected = false;
    private Vector2 initialSize;
    private float y_offset = 0f;

    private float play_delay = 0.1f;

    public EnemyBehaviour[] GetEnemies()
    {
        return GameObject.FindGameObjectWithTag("EnemiesLayer").GetComponentsInChildren<EnemyBehaviour>();
    }

    public void Select()
    {
        selected = true;
        y_offset = vertical_offset_on_selection;
    }

    public void Deselect()
    {
        selected = false;
        transform.localScale = initialSize;
        y_offset = 0f;
    }

    public bool GetSelected()
    {
        return selected;
    }

    public float GetYOffset()
    {
        return y_offset;
    }

    public virtual void Play()
    {
        print("Hello World");
        FinishPlaying();
    }

    public void InvokePlay()
    {
        Invoke("Play", play_delay);
        play_delay = 0.1f;
    }

    public virtual void targetPayoff(EnemyBehaviour enemy)
    {
        print("Targeted: " + enemy.gameObject.name);
    }

    public void DrawNewCard()
    {
        GameObject.FindGameObjectWithTag("DeckPile").GetComponent<DeckPile>().DrawCard();
        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().UpdateDeckAndDiscardPileText();
    }

    public void Discard()
    {
        // called when the card needs to go to the discard pile

        transform.SetParent(GameObject.FindGameObjectWithTag("DiscardPile").GetComponent<RectTransform>());

        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().UpdateDeckAndDiscardPileText();
    }

    public void Target()
    {
        GameObject.FindGameObjectWithTag("EnemiesLayer").GetComponent<EnemyManager>().current_card = this;
        GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().inputtable = GameObject.FindGameObjectWithTag("EnemiesLayer").GetComponent<EnemyManager>();
        GameObject.FindGameObjectWithTag("EnemiesLayer").GetComponent<EnemyManager>().ResetEnemySelection();
    }

    private void Awake()
    {
        initialSize = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (selected)
        {
            transform.localScale = initialSize * enlarge_factor + Vector2.one * Mathf.Abs(Mathf.Sin(Time.time * 2)) * 0.1f;
        }
    }

    public void RunCost()
    {
        play_delay = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>().addTotalTime(time_cost);
    }

    public void FinishPlaying()
    {
        Deselect();
        Discard();
        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().FinishResolution();
    }
}
