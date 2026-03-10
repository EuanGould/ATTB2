using UnityEngine;

public class EvolvingStrainBehaviour : CardBehaviour
{
    public override void Play()
    {
        Target();
    }

    public override void targetPayoff(EnemyBehaviour enemy)
    {
        int damage_output = GameObject.FindGameObjectWithTag("DiscardPile").GetComponentsInChildren<CardBehaviour>().Length;
        
        enemy.damage(damage_output);

        GameObject.FindGameObjectWithTag("DiscardPile").GetComponent<DiscardPile>().ShuffleIntoDeck();

        FinishPlaying();
    }
}
