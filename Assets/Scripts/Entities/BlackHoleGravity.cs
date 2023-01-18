using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleGravity : MonoBehaviour

{

    public float blackHoleMass = 10000f;
    public float playerMass = 1f;
    public float gravitationalConstant = 10f;


    private PlayerMovement player;
    private bool attract = false;

    private void Start()
    {
        player = Locator.GetInstance().GetPlayer().GetComponent<PlayerMovement>();
        if (!player)
        {
            Debug.Log("Player can't be located (BlackHoleGravity.cs)");
        }
    }
    private void FixedUpdate()
    {
        if (attract)
        {
            Vector2 pullDirection = transform.position - player.gameObject.transform.position;
            player.AddForce(ComputeGravitationalForce(pullDirection.magnitude) * pullDirection.normalized, player.transform.position, true);

        }
    }

    float ComputeGravitationalForce(float distance)
    {
        return gravitationalConstant * playerMass * blackHoleMass / (distance * distance);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            attract = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            attract = false;
            player.ResetScale();
        }

    }

}
