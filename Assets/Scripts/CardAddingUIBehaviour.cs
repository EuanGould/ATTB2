using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class CardAddingUIBehaviour : InputtableBehaviour
{
    [SerializeField] private GameObject[] card_pool;


    [SerializeField] private RectTransform choice_a_transform;
    [SerializeField] private RectTransform choice_b_transform;
    [SerializeField] private RectTransform choice_c_transform;

    [SerializeField] private Image background;

    GameObject choice_a;
    GameObject choice_b;
    GameObject choice_c;
    
    private int selection_index = 1;

    private bool active = false;

    private void Awake()
    {
        
    }

    public void InvokeOfferChoice()
    {
        Invoke("OfferChoice", 0.5f);
    }

    private void OfferChoice()
    {
        GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<CardSelectionManager>().ResetDeck();

        active = true;
        
        background.enabled = true;
        
        int choice_a_id = Random.Range(0, card_pool.Length);

        int choice_b_id = Random.Range(0, card_pool.Length);

        while (choice_a_id == choice_b_id)
        {
            choice_b_id = Random.Range(0, card_pool.Length);
        }

        int choice_c_id = Random.Range(0, card_pool.Length);

        while (choice_a_id == choice_c_id || choice_b_id == choice_c_id)
        {
            choice_c_id = Random.Range(0, card_pool.Length);
        }

        choice_a = Instantiate(card_pool[choice_a_id]);
        choice_b = Instantiate(card_pool[choice_b_id]);
        choice_c = Instantiate(card_pool[choice_c_id]);

        choice_a.transform.SetParent(choice_a_transform, false);
        choice_b.transform.SetParent(choice_b_transform, false);
        choice_c.transform.SetParent(choice_c_transform, false);

        choice_a.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        choice_b.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        choice_c.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        ProcessSelection();
        GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().inputtable = this;
    }

    public void ProcessSelection()
    {
        
        if (selection_index == 0)
        {
            choice_a.GetComponent<CardBehaviour>().Select();
            choice_b.GetComponent<CardBehaviour>().Deselect();
            choice_c.GetComponent<CardBehaviour>().Deselect();
        }
        else if (selection_index == 1)
        {
            choice_a.GetComponent<CardBehaviour>().Deselect();
            choice_b.GetComponent<CardBehaviour>().Select();
            choice_c.GetComponent<CardBehaviour>().Deselect();
        }
        else
        {
            choice_a.GetComponent<CardBehaviour>().Deselect();
            choice_b.GetComponent<CardBehaviour>().Deselect();
            choice_c.GetComponent<CardBehaviour>().Select();
        }
    }

    public override void OnSingleButtonHeld()
    {
        if (active)
        {

            if (selection_index == 0)
            {
                choice_a.transform.SetParent(GameObject.FindGameObjectWithTag("DeckPile").transform);
                choice_a.GetComponent<CardBehaviour>().Deselect();
            }
            else if (selection_index == 1)
            {
                choice_b.transform.SetParent(GameObject.FindGameObjectWithTag("DeckPile").transform);
                choice_b.GetComponent<CardBehaviour>().Deselect();
            }
            else
            {
                choice_c.transform.SetParent(GameObject.FindGameObjectWithTag("DeckPile").transform);
                choice_c.GetComponent<CardBehaviour>().Deselect();
            }

            FinishUp();
        }

    }

    public override void OnSingleButtonTapped()
    {
        if (active)
        {
            selection_index++;
            selection_index %= 3;

            ProcessSelection();
        }
    }

    public void FinishUp()
    {
        active = false;
        
        foreach (CardBehaviour card in transform.GetComponentsInChildren<CardBehaviour>())
        {
            Destroy(card.gameObject);
        }

        background.enabled = false;

        GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().inputtable = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<InputtableBehaviour>();

        GameObject.FindGameObjectWithTag("EnemiesLayer").GetComponent<EnemyManager>().GoNext();
    }
}
