using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject damage_vfx_prefab;
    
    public void CreateDamagePlayer(RectTransform origin)
    {
        GameObject new_fx = Instantiate(damage_vfx_prefab);
        new_fx.transform.SetParent(transform);
        new_fx.GetComponent<RectTransform>().anchoredPosition = origin.anchoredPosition;
        new_fx.GetComponent<DamageVFX>().SetGoal(new Vector2(-1150, -400));
    }
}
