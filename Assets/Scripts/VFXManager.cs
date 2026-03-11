using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject damage_vfx_prefab;
    public GameObject damage_text_prefab;

    public void CreateDamagePlayer(RectTransform origin)
    {
        GameObject new_fx = Instantiate(damage_vfx_prefab);
        new_fx.transform.SetParent(transform);
        new_fx.GetComponent<RectTransform>().position = origin.position;
        new_fx.GetComponent<DamageVFX>().SetGoal(new Vector2(-1150, -400));
    }

    public void CreateDamageEnemy(RectTransform origin)
    {
        GameObject new_fx = Instantiate(damage_vfx_prefab);
        new_fx.transform.SetParent(transform);
        new_fx.GetComponent<RectTransform>().position = new Vector2(0, -1100);
        new_fx.GetComponent<DamageVFX>().SetGoal(origin.position);
    }

    public void CreateDamageNumber(RectTransform origin, int amount)
    {
        GameObject new_fx = Instantiate(damage_text_prefab);
        new_fx.transform.SetParent(origin);
        new_fx.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(0, 100), 0);
        new_fx.GetComponent<DamageNumberBehaviour>().SetValue(amount);
    }
}
