using TMPro;
using UnityEngine;

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
    }
}
