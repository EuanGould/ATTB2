using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected RectTransform health_bar_fill;
    [SerializeField] protected TextMeshProUGUI health_text;
    [SerializeField] protected Image selection_arrow;
    [SerializeField] protected TextMeshProUGUI countdown_text;
    [SerializeField] protected TextMeshProUGUI damage_text;

    // adjustables
    [SerializeField] public int max_health;
    [SerializeField] public int attack_cooldown;
    [SerializeField] public int attack_damage;

    protected TimeManager time_manager;
    protected int health;

    protected int cooldown_start_time;

    private void Awake()
    {
        health = max_health;
        updateHealth();
        time_manager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        cooldown_start_time = time_manager.getTotalTime();
        countdown_text.text = attack_cooldown.ToString();
        UpdateAttackDamage();
    }

    public virtual void OnSpawning()
    {
        cooldown_start_time = time_manager.getTotalTime();
        gameObject.GetComponent<RectTransform>().localScale = Vector3.one * 0.7f;
        UpdateAttackDamage();
    }

    public void updateHealth()
    {
        health_text.text = health + "/" + max_health;

        health_bar_fill.localScale = new Vector2(Mathf.Max(0, (float)health / (float)max_health), 1);
    }

    public virtual void damage(int amount)
    {
        // called when taking damage
        int post_calc_amount = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>().CalcDamage(amount);

        health -= post_calc_amount;
        updateHealth();
        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamageEnemy(GetComponent<RectTransform>());
        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamageNumber(GetComponent<RectTransform>(), -post_calc_amount);
    }

    public void DeathCheck()
    {
        if (health <= 0)
        {
            transform.parent.GetComponent<EnemyManager>().EnemyDeath(this);
        }
    }

    public virtual float onTimeProgressed()
    {
        float delay_to_add = 0.0f;
        int current_time = time_manager.getTotalTime();

        while (current_time >= cooldown_start_time + attack_cooldown)
        {
            InvokeAttack(delay_to_add);
            cooldown_start_time += attack_cooldown;
            delay_to_add += 0.2f;
        }

        countdown_text.text = (attack_cooldown - current_time + cooldown_start_time).ToString();

        return delay_to_add;
    }

    protected void InvokeAttack(float delay)
    {
        Invoke("Attack", delay);
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

    public void DelayAttack(int value)
    {
        attack_cooldown += value;
        countdown_text.text = (attack_cooldown - time_manager.getTotalTime() + cooldown_start_time).ToString();
    }

    public void InvokeDeath(float time)
    {
        Invoke("InvokableDeath", time);
    }

    private void InvokableDeath()
    {
        Deselect();
        Destroy(this.gameObject);

    }

    protected void UpdateAttackDamage()
    {
        damage_text.text = attack_damage.ToString();
    }

    public void SetAttackDamage(int amount)
    {
        attack_damage = amount;
        UpdateAttackDamage();
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(max_health, health + amount);
        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamageNumber(GetComponent<RectTransform>(), amount);
        updateHealth();
    }
}
