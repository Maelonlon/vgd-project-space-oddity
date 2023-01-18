using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : PoolObject
{

    public float speed;
    public float lifetime = 10f;
    private Rigidbody2D rb;
    private float timer = 0f;


    //private Vector2 screenBounds;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    public void Initialize(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
        timer = 0f;
    }

    private void Update()
    {
        if (timer >= 0.0f)
        {
            timer += Time.deltaTime;
            if (timer >= lifetime)
            {
                SetInactive();
                timer = -1f;
            }
        }

    }
    void SetInactive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")   //a contatto con il player
        {
            SetInactive();

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
        if (collision.tag == "Wall")
        {
            SetInactive();
        }


    }
}


