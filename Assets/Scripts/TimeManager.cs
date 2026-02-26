using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int total_time = 0;

    public int getTotalTime()
    {
        return total_time;
    }

    public void addTotalTime(int amount)
    {
        total_time += amount;
    }
}
