using TMPro;
using UnityEngine;

public class DamageNumberBehaviour : MonoBehaviour
{
    float starting_time = Mathf.Infinity;
    
    private void Awake()
    {
        starting_time = Time.time;
    }

    public void SetValue(int value)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = value.ToString();
        if (value > 0)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition += Vector2.down * Time.fixedDeltaTime * 17;

        if (Time.time > starting_time + 1)
        {
            Destroy(gameObject);
        }
    }
}
