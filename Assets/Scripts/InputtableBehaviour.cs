using UnityEngine;

public class InputtableBehaviour : MonoBehaviour
{
    // this is a base class for anything that requires the player to use the tapping and holding convention to do things
    
    public virtual void OnSingleButtonHeld()
    {
        return;
    }

    public virtual void OnSingleButtonTapped()
    {
        return;
    }
}
