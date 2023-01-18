using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{

    [HideInInspector]
    public bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !activated)
        {
            Locator.GetInstance().GetSpawnpointManager().SetNewSpawnPoint(transform);
            GetComponent<Renderer>().material.color = Color.cyan;

        }
    }
}
