using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator fadeAnim;

    public bool startWithFadeOut = false;

    private void Start() {
        if (startWithFadeOut)
            FadeOut();
    }

    public void FadeIn(){
        fadeAnim.SetTrigger("fadeIn");
    }
    public void FadeOut(){
        fadeAnim.SetTrigger("fadeOut");
    }

    
}
