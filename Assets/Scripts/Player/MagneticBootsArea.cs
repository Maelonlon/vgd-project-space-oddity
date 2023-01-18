using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBootsArea : MonoBehaviour
{
    public List<Collider2D> magneticObjectsInArea = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Magnetic")
        {
            magneticObjectsInArea.Add(other);
            print(other.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Magnetic")
        {
            magneticObjectsInArea.Remove(other);
            print(other.gameObject.name);
        }
    }
}
