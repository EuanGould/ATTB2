using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private RectTransform health_bar_fill;
    [SerializeField] private TextMeshProUGUI health_text;
    [SerializeField] private Image selection_arrow;
    [SerializeField] private TextMeshProUGUI countdown_text;

    // adjustables
    [SerializeField] private int max_health;
    [SerializeField] private int attack_cooldown;
    [SerializeField] private int attack_damage;

    private TimeManager time_manager;
    private int health;

    private int cooldown_start_time;

    private void Awake()
    {
        health = max_health;
        updateHealth();
        time_manager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        cooldown_start_time = time_manager.getTotalTime();
        countdown_text.text = attack_cooldown.ToString();
    }

    public void updateHealth()
    {
        health_text.text = health + "/" + max_health;

        health_bar_fill.localScale = new Vector2((float)health / (float)max_health, 1);
    }

    public virtual void damage(int amount)
    {
        // called when taking damage
        health -= amount;
        updateHealth();
    }

    public virtual void onTimeProgressed()
    {
        int current_time = time_manager.getTotalTime();

        while (current_time >= cooldown_start_time + attack_cooldown)
        {
            Attack();
            cooldown_start_time += attack_cooldown;
        }

        countdown_text.text = (attack_cooldown - current_time + cooldown_start_time).ToString();
    }

    public virtual void Attack()
    {
        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamagePlayer(GetComponent<RectTransform>());
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>().TakeDamage(attack_damage);
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
