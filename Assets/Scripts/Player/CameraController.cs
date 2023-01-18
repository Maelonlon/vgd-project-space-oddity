using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;       //riferimento per linkare l'oggetto corrente (camera) all'oggetto sfera

    public bool shake = false;
    // Start is called before the first frame update
    public Vector3 delta = new Vector3(5f, 0f, -10f);          //Creo una variabile vettore per contenere la differenza tra le coordinate della camera e le coordinate dell'oggetto

    public float shakeIntensity = 0.1f;
    void Start()
    {
        player = Locator.GetInstance().GetPlayer();
        if (!player)
        {
            Debug.Log("Player reference null (CameraController.cs)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.position = player.transform.position + delta;
        }
        //Faccio in modo che la posizione della camera sia uguale alla posizione della sfera + la differenza di posizione tra i due oggetti

        if (shake)
        {
            StartCoroutine(Shaking());

        }
    }

    IEnumerator Shaking()
    {

        while (shake)
        {
            transform.position = (player.transform.position + delta) + Random.insideUnitSphere * shakeIntensity;

            yield return null;
        }
        transform.position = player.transform.position + delta;
    }
}
