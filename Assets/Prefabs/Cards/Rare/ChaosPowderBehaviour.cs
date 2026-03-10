using UnityEngine;

public class ChaosPowderBehaviour : CardBehaviour
{
    public override void Play()
    {
        GameObject[] card_pool = GameObject.FindGameObjectWithTag("CardAddingUI").GetComponent<CardAddingUIBehaviour>().GetCardPool();

        int times_run = Random.Range(1,5);

        int choice_id = 0;

        for (int i = 0; i < times_run + 1; i++)
        {
            choice_id = Random.Range(0, card_pool.Length);

            GameObject choice = Instantiate(card_pool[choice_id]);
            choice.transform.SetParent(GameObject.FindGameObjectWithTag("PlayerHand").transform);
            choice.GetComponent<CardBehaviour>().SetIsTemporaryCard(true);
            choice.GetComponent<CardBehaviour>().SetCost(0);
            choice.GetComponent<RectTransform>().localScale = Vector3.one;
            
        }

        FinishPlaying();
    }
}
