using UnityEngine;
using UnityEngine.UIElements;

public class DamageVFX : MonoBehaviour
{
    [SerializeField] private Vector3 goal = new Vector3();

    private void FixedUpdate()
    {
        GetComponent<RectTransform>().position += (goal - GetComponent<RectTransform>().position) * Time.fixedDeltaTime * 7;

        if ((goal - GetComponent<RectTransform>().position).magnitude < 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetGoal(Vector2 new_goal)
    {
        goal = new_goal;
    }
}