using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    private static Locator _instance;
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private SpawnpointManager spawnPointManager = null;
    [SerializeField]
    private EndingRoomManager endRoomManager = null;

    [SerializeField]
    private UIManager UIManager = null;



    private void Awake()
    {
        Debug.Log("Locator awake");
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public static Locator GetInstance()
    {

        return _instance;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public SpawnpointManager GetSpawnpointManager()
    {
        return spawnPointManager;
    }

    public EndingRoomManager GetEndingRoomManager()
    {
        return endRoomManager;
    }

    public UIManager GetUIManager()
    {
        return UIManager;
    }



    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

}
