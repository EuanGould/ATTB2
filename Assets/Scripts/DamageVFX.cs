using UnityEngine;

public class DamageVFX : MonoBehaviour
{
    [SerializeField] private Vector2 goal = new Vector2();

    private void Start()
    {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Foreground";
    }
}