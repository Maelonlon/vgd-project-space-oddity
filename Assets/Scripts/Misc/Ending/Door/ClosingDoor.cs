using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingDoor : MonoBehaviour
{

    Vector2 openPosition;
    public bool isOpen = true;
    Vector2 closedPosition;
    public float distance;

    public float speed = 1;

    private void Start()
    {
        openPosition = transform.position;
        closedPosition = transform.position + transform.right * distance;


    }

    private void Update()
    {
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        float distance = (transform.position - targetPosition).magnitude;
        if (distance > 0.1f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
    public void Open() => isOpen = true;
    public void Close()
    {
        isOpen = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distance);
    }

}
