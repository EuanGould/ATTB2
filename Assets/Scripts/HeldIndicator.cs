using UnityEngine;
using UnityEngine.UI;

public class HeldIndicator : MonoBehaviour
{
    private Image fill;

    [SerializeField] EbbBehaviour ebber;

    private void Awake()
    {
        fill = GetComponent<Image>();
    }

    public void changeIndication(float value)
    {
        Color color = new Color(1, 1f, 1f, value);

        if (value >= 1)
        {
            ebber.SetVisibility(true);
            color = new Color(0.5f, 1f, 0.5f, value);
            fill.color = color;
        }
        else
        {
            ebber.SetVisibility(false);

            fill.color = color;
        }


    }
}
