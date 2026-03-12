using UnityEngine;

public class BurningMunitionsBehaviour : CardBehaviour
{
    private int payoff_damage = 0;
    
    public override void Play()
    {
        foreach(CardBehaviour card in GameObject.FindGameObjectWithTag("DeckPile").GetComponentsInChildren<CardBehaviour>())
        {
            card.transform.SetParent(GameObject.FindGameObjectWithTag("DiscardPile").transform);
            payoff_damage++;
        }
        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().UpdateDeckAndDiscardPileText();
        Target();
    }

    public override void targetPayoff(EnemyBehaviour enemy)
    {
        enemy.damage(payoff_damage);
        enemy.DelayAttack(payoff_damage);
        FinishPlaying();
    }
}
