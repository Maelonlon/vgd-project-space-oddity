using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{

    //public int debrisActivationType;
    public float debrisSpeed;
    public float triggerDistance;
    GameObject player;
    Rigidbody2D rb;

    public Rotate rotator;

    bool isMoving = false;
    public bool rightAndLeft = false;

    [Range(0f, 360f)]
    public float angle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = Locator.GetInstance().GetPlayer();
        if (!player)
        {
            Debug.Log("Player reference null (Debris.cs)");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            float distance = (player.transform.position - transform.position).magnitude;
            if (distance < triggerDistance)
            {
                rotator.rotationSpeed = 0f;
                Invoke("Aggro", 0.2f);

            }
        }
    }

    void Aggro()
    {
        if (isMoving) return;

        if (rightAndLeft)
        {
            if (player.transform.position.x < transform.position.x)
            {
                rb.velocity = (Vector2FromAngle(angle) * debrisSpeed);
            }
            else
            {
                rb.velocity = (-Vector2FromAngle(angle) * debrisSpeed);
            }

            isMoving = true;

        }
        else
        {
            rb.velocity = (Vector2FromAngle(angle) * debrisSpeed);

            isMoving = true;
        }
    }

    public Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    private void OnDrawGizmos()
    {
        Vector2 ray = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
        Debug.DrawRay(transform.position, ray, Color.magenta);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject, 3f);
        }
    }


}


