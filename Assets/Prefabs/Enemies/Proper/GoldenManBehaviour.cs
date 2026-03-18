using TMPro;
using UnityEngine;

public class GoldenManBehaviour : EnemyBehaviour
{
    private Vector2 start_pos;

    [SerializeField] private TextMeshProUGUI light_text;

    private int light_points = 0;

    private void Awake()
    {
        health = max_health;
        updateHealth();
        time_manager = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager>();
        cooldown_start_time = time_manager.getTotalTime();
        countdown_text.text = attack_cooldown.ToString();
        UpdateAttackDamage();

        start_pos = new Vector2(0, -320);
    }

    private void FixedUpdate()
    {
        GetComponent<RectTransform>().anchoredPosition = start_pos + Vector2.up * Mathf.Sin(Time.time) * 4;
    }

    public override void Attack()
    {
        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamagePlayer(GetComponent<RectTransform>());
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>().TakeDamage(attack_damage);
        SetAttackDamage(attack_damage * 2);

    }

    public void onPlayed()
    {
        light_points++;
        light_text.text = light_points.ToString();
        time_manager.addTotalTime(light_points);
    }
}
