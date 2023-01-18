using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinTrigger : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")   //a contatto con il player
        {
            SceneManager.LoadScene("WinScreen");

        }

    }
}
