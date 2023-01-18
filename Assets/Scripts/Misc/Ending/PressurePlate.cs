using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public UnityEvent onButtonPressed;

    public float distanceForPressing = 0.5f;
    Vector3 initialPosition;

    bool isPressed = false;
    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        float distance = (transform.localPosition - initialPosition).magnitude;
        if (isPressed && distance < distanceForPressing)
        {
            transform.Translate(Vector3.down * Time.deltaTime, Space.Self);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isPressed && other.gameObject.CompareTag("Player"))
        {
            isPressed = true;
            onButtonPressed.Invoke();
        }
    }


}
