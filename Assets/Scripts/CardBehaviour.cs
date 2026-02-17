using Unity.VisualScripting;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    // adjustable
    [SerializeField] float enlarge_factor = 1.2f;
    [SerializeField] float vertical_offset_on_selection = 200f;

    // internal
    private bool selected = false;
    private Vector2 initialSize;
    private float y_offset = 0f;

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
        Discard();
    }

    public void DrawNewCard()
    {
        GameObject.FindGameObjectWithTag("DeckPile").GetComponent<DeckPile>().DrawCard();
    }

    public void Discard()
    {
        // called when the card needs to go to the discard pile
        transform.SetParent(GameObject.FindGameObjectWithTag("DiscardPile").GetComponent<RectTransform>());
        Deselect();
    }

    private void Awake()
    {
        initialSize = transform.localScale;
    }

    private void Update()
    {
        if (selected)
        {
            transform.localScale = initialSize * enlarge_factor + Vector2.one * Mathf.Abs(Mathf.Sin(Time.time * 2)) * 0.1f;
        }
    }
}
