using UnityEngine;

public class DamageVFX : MonoBehaviour
{
    [SerializeField] private Vector2 goal = new Vector2();

    private void FixedUpdate()
    {
        GetComponent<RectTransform>().anchoredPosition += (goal - GetComponent<RectTransform>().anchoredPosition) * Time.fixedDeltaTime * 7;

        if ((goal - GetComponent<RectTransform>().anchoredPosition).magnitude < 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetGoal(Vector2 new_goal)
    {
        goal = new_goal;
    }
}