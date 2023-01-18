using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : PoolObject      //movimento destra-sinistra su-gi� (in a box)
{

    Vector2 directionVector;

    public float speed = 3;
    public Vector2 distance = new Vector2(10f, 5f);    //distanza da percorrere nei vettori x e y

    private Vector2 startingPoint;  //punto da cui parte l'alieno

    public bool dieOnWallHit = false;
    public bool bounceOnWallHit = false;

    public float delay;     //variabile per aggiungere un delay alla partenza dell'alieno

    SpriteRenderer sprite;

    [Range(0f, 360f)]
    [SerializeField]
    public float angle = 0f;

    public void SetAngle(float angle)
    {
        this.angle = angle;
        directionVector = Vector2FromAngle(angle);
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startingPoint = transform.position;

        SetAngle(angle);


        if (directionVector.x <= 0f)
            sprite.flipX = false;
        else
            sprite.flipX = true;

        directionVector = directionVector.normalized;



    }

    public void DeactivateIn(float seconds)
    {
        Invoke("Deactivate", seconds);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // These 3 methods are used to show the effect of the aliens going out of the frame in the ending room
    public void MoveLayerAboveIn(float seconds)
    {
        Invoke("MoveLayerAbove", seconds);
    }

    void MoveLayerAbove()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    public void ResetLayer()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 0;
    }

    public Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    // Update is called once per frame
    void Update()
    {

        if (delay > 0)
        {
            //sottrae il tempo passato al delay
            delay -= Time.deltaTime;
        }
        else
        {
            transform.Translate(directionVector * speed * Time.deltaTime);

            sprite.flipX = directionVector.x > 0;

            if (transform.position.x < startingPoint.x - (distance.x / 2f)
                || transform.position.x > startingPoint.x + (distance.x / 2f))
            {
                directionVector.x *= -1f;
            }

            if (transform.position.y < startingPoint.y - (distance.y / 2f)
                || transform.position.y > startingPoint.y + (distance.y / 2f))
            {
                directionVector.y *= -1f;
            }



        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Magnetic"))
        {
            if (dieOnWallHit)
                Destroy(gameObject);

            if (bounceOnWallHit)
            {
                Vector2 normal = collision.contacts[0].normal;

                Vector2 targetDirectionVector = Vector2.Reflect(directionVector, normal);
                float targetAngle = Mathf.Atan2(targetDirectionVector.y, targetDirectionVector.x) * Mathf.Rad2Deg;

                Debug.DrawRay(collision.contacts[0].point, targetDirectionVector, Color.cyan, 1f);

                SetAngle(FindClosestAngle(targetAngle));

            }

        }

    }



    float FindClosestAngle(float targetAngle)
    {
        float closestAngle = angle;
        float minDelta = Mathf.Abs(Mathf.DeltaAngle(targetAngle, angle));
        for (int i = 1; i < 4; i++)
        {
            float currentAngle = angle + i * 90f;
            float deltaAngle = Mathf.Abs(Mathf.DeltaAngle(targetAngle, currentAngle));
            if (minDelta > deltaAngle)
            {
                closestAngle = currentAngle;
                minDelta = deltaAngle;


            }
        }

        return closestAngle % 360;
    }

    private void OnDrawGizmos()
    {
        Vector2 quadOrigin;
        if (Application.isPlaying)
        {
            Debug.DrawRay(transform.position, directionVector, Color.magenta);
            quadOrigin = startingPoint;
        }
        else
        {
            Vector2 ray = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            Debug.DrawRay(transform.position, ray, Color.magenta);
            quadOrigin = transform.position;
        }



        Vector2 bottomLeft = new Vector2(quadOrigin.x - (distance.x / 2f), quadOrigin.y - (distance.y / 2f));
        Vector2 bottomRight = new Vector2(quadOrigin.x + (distance.x / 2f), quadOrigin.y - (distance.y / 2f));
        Vector2 topRight = new Vector2(quadOrigin.x + (distance.x / 2f), quadOrigin.y + (distance.y / 2f));
        Vector2 topLeft = new Vector2(quadOrigin.x - (distance.x / 2f), quadOrigin.y + (distance.y / 2f));

        Debug.DrawLine(bottomLeft, bottomRight, Color.red);
        Debug.DrawLine(bottomLeft, topLeft, Color.red);
        Debug.DrawLine(bottomRight, topRight, Color.red);
        Debug.DrawLine(topLeft, topRight, Color.red);
    }








}
