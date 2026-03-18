using UnityEngine;
using UnityEngine.UI;

public class EbbBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float ebb_magnitude = 1f;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moving_value = 1 + Mathf.Abs(Mathf.Sin(Time.time * speed) * ebb_magnitude);
        Color color = new Color(0.5f, 1f, 0.5f, 2 - moving_value);
        image.rectTransform.sizeDelta = new Vector2(moving_value * 106, 106f);
        image.color = color;
    }

    public void SetVisibility(bool value)
    {
        image.enabled = value;
    }
}
