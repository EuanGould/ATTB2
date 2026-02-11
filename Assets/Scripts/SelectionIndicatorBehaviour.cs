using UnityEngine;

public class SelectionIndicatorBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 offset = Vector2.zero;

    public void MoveSelectorTo(Vector2 pos)
    {
        transform.position = pos + offset;
    }
}
