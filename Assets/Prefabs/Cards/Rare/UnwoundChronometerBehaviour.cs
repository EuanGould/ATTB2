using UnityEngine;

public class UnwoundChronometerBehaviour : CardBehaviour
{
    public override void Play()
    {
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>().TakeDamage(2);
        GameObject.FindGameObjectWithTag("VFXCanvas").GetComponent<VFXManager>().CreateDamagePlayer(GameObject.FindGameObjectWithTag("PlayerRectPos").GetComponent<RectTransform>());

        FinishPlaying();
    }
}
