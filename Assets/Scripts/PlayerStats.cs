using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health_text;

    [SerializeField] private TextMeshProUGUI stats_text;

    [SerializeField] private int starting_health = 100;

    private int health;
    private int exhaustion = 15;
    private int attack_add = 0;
    private int attack_mult = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = starting_health;
        health_text.text = health.ToString();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        health_text.text = health.ToString();

        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamageNumber(health_text.gameObject.GetComponent<RectTransform>(), -amount);

        if (health <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        SceneManager.LoadScene("DeathScreen");
    }

    public void ResetPlayerStats()
    {
        health = starting_health;
        health_text.text = health.ToString();
        exhaustion = 15;
        attack_add = 0;
        attack_mult = 1;
    }

    public void IncrementExhaustion()
    {
        exhaustion += exhaustion;
    }

    public int GetExhaustion()
    {
        return exhaustion;
    }

    private void FixedUpdate()
    {
        health_text.text = health.ToString();
        string stats_text_contents = "";

        if (attack_add != 0)
        {
            stats_text_contents += attack_add.ToString() + " bonus damage dealt\n";
        }
        if (attack_mult != 1)
        {
            stats_text_contents += attack_mult.ToString() + " times damage dealt\n";
        }
        if (exhaustion != 15)
        {
            stats_text_contents += exhaustion.ToString() + " exhaustion cost\n";
        }

        stats_text.text = stats_text_contents;
    }

    public void Heal(int amount)
    {
        health += amount;
        health_text.text = health.ToString();
        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamageNumber(health_text.gameObject.GetComponent<RectTransform>(), amount);
    }

    public int CalcDamage(int amount)
    {
        return (attack_add + amount) * attack_mult;
    }

    public void ExpendAttackMult()
    {
        attack_mult = 1;
    }

    public void MultiplyAttackMult(int amount)
    {
        attack_mult *= amount;
    }

    public void AddAttackAdd(int amount)
    {
        attack_add += amount;
    }
}
