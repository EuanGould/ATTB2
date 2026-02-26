using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private RectTransform health_bar_fill;
    [SerializeField] private TextMeshProUGUI health_text;
    [SerializeField] private Image selection_arrow;

    [SerializeField] private int max_health;

    private float health;

    private void Awake()
    {
        health = max_health;
        updateHealth();
    }

    public void updateHealth()
    {
        health_text.text = health + "/" + max_health;

        health_bar_fill.localScale = new Vector2(health / max_health, 1);
    }

    public virtual void damage(int amount)
    {
        // called when taking damage
        health -= amount;
        updateHealth();
    }

    public void Select()
    {
        selection_arrow.enabled = true;
    }

    public void Deselect()
    {
        selection_arrow.enabled = false;
    }
}
