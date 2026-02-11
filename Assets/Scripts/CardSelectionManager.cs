using UnityEngine;
using UnityEngine.InputSystem;

public class CardSelectionManager : InputtableBehaviour
{

    // internal variables

    private enum State
    {
        Selecting,
        Watching
    }

    private SelectionIndicatorBehaviour selection_indicator;
    
    void Awake()
    {
        selection_indicator = GameObject.FindGameObjectWithTag("SelectionIndicator").GetComponent<SelectionIndicatorBehaviour>();
    }

    public override void OnSingleButtonHeld()
    {
        print("held");
    }

    public override void OnSingleButtonTapped()
    {
        print("tapped");
    }
}
