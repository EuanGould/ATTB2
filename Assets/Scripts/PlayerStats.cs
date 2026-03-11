using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health_text;

    [SerializeField] private int starting_health = 100;

    private int health;

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

        if (health <= 0)
        {
            Die();
        } 
    }

    public void Die()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ResetPlayerStats()
    {
        health = starting_health;
        health_text.text = health.ToString();
    }

    private void FixedUpdate()
    {
        health_text.text = health.ToString();
    }

    public void Heal(int amount)
    {
        health += amount;
        health_text.text = health.ToString();
    }
}
