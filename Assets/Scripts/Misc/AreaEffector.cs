using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffector : MonoBehaviour
{

    private PlayerMovement player;
    private bool attract = false;
    private Vector2 gravityVector;
    public float angle = 0f;
    public float gravityForce;


    // Start is called before the first frame update
    void Start()
    {
        player = Locator.GetInstance().GetPlayer().GetComponent<PlayerMovement>();
        if (!player)
        {
            Debug.Log("Player can't be located (AreaEffector.cs)");
        }
        gravityVector = Vector2FromAngle(angle);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (attract)
        {
            Vector2 pullDirection = transform.position - player.gameObject.transform.position;
            player.AddForce(gravityVector * gravityForce, player.transform.position, true);

        }
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

    public Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    private void OnDrawGizmos()
    {
        DrawArrow();
    }

    void DrawArrow()
    {
        Vector3 ray = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);
        Vector3 right = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle + 15f)), Mathf.Sin(Mathf.Deg2Rad * (angle + 15f)), 0f);
        Vector3 left = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (angle - 15f)), Mathf.Sin(Mathf.Deg2Rad * (angle - 15f)), 0f);
        Debug.DrawRay(transform.position, ray * 5f, Color.cyan);
        Vector3 point = transform.position + (Vector3)ray * 5f;
        Debug.DrawLine(point, transform.position + right * 4f, Color.cyan);
        Debug.DrawLine(point, transform.position + left * 4f, Color.cyan);
    }

}
