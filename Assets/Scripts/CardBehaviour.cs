using Unity.VisualScripting;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    private bool selected = false;
    private Vector2 initialSize;
    
    public void Select()
    {
        selected = true;
    }

    public void Deselect()
    {
        selected = false;
        transform.localScale = initialSize;
    }

    public bool GetSelected()
    {
        return selected;
    }

    public virtual void Play()
    {
        print("Hello World");
        Discard();
    }

    public void Discard()
    {
        // called when the card needs to go to the discard pile
        Destroy(gameObject);
    }

    private void Awake()
    {
        initialSize = transform.localScale;
    }

    private void Update()
    {
        if (selected)
        {
            transform.localScale = initialSize * 1.1f + Vector2.one * 0.5f * Mathf.Abs(Mathf.Sin(Time.time));
        }
    }
}
