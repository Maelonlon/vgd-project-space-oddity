using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KillOnTouch : MonoBehaviour
{
    public bool destroyOnWallHit = false;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")   //a contatto con il player
        {
            Debug.Log("Hit");
            Locator.GetInstance().GetSpawnpointManager().ReloadScene();
        }
        if (other.CompareTag("Wall") || other.CompareTag("Magnetic"))
        {
            if (destroyOnWallHit)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")   //a contatto con il player
        {
            PlayerMovement player = Locator.GetInstance().GetPlayer().GetComponent<PlayerMovement>();
            if (player.isInvulnerable)
            {
                player.SetInvulnerabilityOff();
            }
            else
            {
                Debug.Log("Hit");
                Locator.GetInstance().GetSpawnpointManager().ReloadScene();
            }
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Magnetic"))
        {
            if (destroyOnWallHit)
                Destroy(gameObject);
        }
    }
}
