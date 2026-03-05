using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int total_time = 0;

    public int getTotalTime()
    {
        return total_time;
    }

    public float addTotalTime(int amount)
    {
        total_time += amount;
        float contending_delay = 0.0f;
        float delay_to_add = 0.0f;

        foreach (EnemyBehaviour enemy in GameObject.FindGameObjectWithTag("EnemiesLayer").transform.GetComponentsInChildren<EnemyBehaviour>())
        {
            contending_delay = enemy.onTimeProgressed();
            if (contending_delay > delay_to_add )
            {
                delay_to_add = contending_delay;
            }
        }

        return delay_to_add;
    }

}
