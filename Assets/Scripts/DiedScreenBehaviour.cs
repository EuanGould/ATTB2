using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiedScreenBehaviour : InputtableBehaviour
{
    [SerializeField] private GameObject arrows_left;
    [SerializeField] private GameObject arrows_right;
    
    private bool left_button_selected = true;
    
    public override void OnSingleButtonTapped()
    {
        left_button_selected = !left_button_selected;
        update_arrows();
    }

    public override void OnSingleButtonHeld()
    {
        if (left_button_selected)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Application.Quit();
        }
    }

    private void update_arrows()
    {
        if (left_button_selected)
        {
            arrows_left.SetActive(true);
            arrows_right.SetActive(false);
        }
        else
        {
            arrows_right.SetActive(true);
            arrows_left.SetActive(false);
        }
    }
}
