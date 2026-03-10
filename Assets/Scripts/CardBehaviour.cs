using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    // adjustable
    [SerializeField] float enlarge_factor = 1.2f;
    [SerializeField] float vertical_offset_on_selection = 200f;
    [SerializeField] int starting_time_cost = 0;
    [SerializeField] TextMeshProUGUI cost_text;
    [SerializeField] Image clockface;

    // internal
    private bool selected = false;
    private Vector2 initialSize;
    private float y_offset = 0f;

    private int time_cost = 0;

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
        time_cost = starting_time_cost;
        cost_text.text = time_cost.ToString();

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
        GameObject.FindGameObjectWithTag("EnemiesLayer").GetComponent<EnemyManager>().DeathCheckAll();
    }

    public void ResetValues()
    {
        time_cost = starting_time_cost;
        UpdateCostText();
    }

    public void AddCost(int value)
    {
        time_cost += value;
        UpdateCostText();
    }

    public void SetCost(int value)
    {
        time_cost = value;
        UpdateCostText();
    }

    private void UpdateCostText()
    {
        Color clockFaceDefault = new Color(1, 1, 1);
        Color textDefault = new Color(0, 0, 0);

        Color clockFaceNegative = new Color(0.76f, 1, 0.82f);
        Color textNegative = new Color(0, 0.33f, 0);

        cost_text.text = time_cost.ToString();

        if (time_cost > 0)
        {
            cost_text.color = textDefault;
            clockface.color = clockFaceDefault;
        }
        else
        {
            cost_text.color = textNegative;
            clockface.color = clockFaceNegative;
        }
    }
}
