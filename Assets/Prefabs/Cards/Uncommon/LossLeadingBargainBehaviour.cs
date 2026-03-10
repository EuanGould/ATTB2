using UnityEngine;

public class LossLeadingBargainBehaviour : CardBehaviour
{
    public override void Play()
    {
        CardBehaviour winning_card = this;
        
        foreach (CardBehaviour card in GameObject.FindGameObjectWithTag("PlayerHand").GetComponentsInChildren<CardBehaviour>())
        {
            if (winning_card.GetCost() == card.GetCost() && Random.Range(0,10) % 2 == 0)
            {
                winning_card = card;
            }
            else if (winning_card.GetCost() < card.GetCost())
            {
                winning_card = card;
            }
        }

        winning_card.AddCost(-winning_card.GetCost() / 2);

        DrawNewCard();

        FinishPlaying();
    }
}
