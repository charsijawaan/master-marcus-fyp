using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public TMP_Text continueText;

    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        continueText.text = "<sprite=2> Continue";
        animator.SetTrigger("pop");
    }

    public void ClosePopUp()
    {
        continueText.text = "";
        animator.SetTrigger("close");
    }
}
