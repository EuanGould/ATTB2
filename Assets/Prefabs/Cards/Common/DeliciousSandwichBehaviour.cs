using UnityEngine;

public class DeliciousSandwichBehaviour : CardBehaviour
{
    public override void Play()
    {
        DrawNewCard();
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>().Heal(5);
        FinishPlaying();
    }
}
