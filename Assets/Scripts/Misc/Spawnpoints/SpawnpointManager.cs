using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnpointManager : MonoBehaviour
{
    //Punto dove spawna il giocatore

    public List<Transform> spawnPoints;

    public GameObject prefabPlayer;

    static bool firstSceneLoad = true;
    // only true when the scene first loads, if resetCheckpointOnPlay is true,
    // it resets the checkpoint data for playing in the editor

    public bool resetCheckpointOnPlay = true;

    public bool flipPlayer = false;


    GameObject player;




    private void Awake()
    {

        Debug.Log("Spawn Manager awake");



        if (SaveLoadUtils.GetCurrentLevel() == 1 && SceneManager.GetActiveScene().name != "Level2")
        {
            SceneManager.LoadScene("Level2");
        }

        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Level1":
                SaveLoadUtils.SetCurrentLevel(0);
                break;
            case "Level2":
                SaveLoadUtils.SetCurrentLevel(1);
                break;
        }

        if (!SaveLoadUtils.HasCurrentSpawnPointIndex())
            SaveLoadUtils.SetCurrentSpawnPointIndex(0);

        if (firstSceneLoad && resetCheckpointOnPlay)
            SaveLoadUtils.SetCurrentSpawnPointIndex(0);

        switch (SaveLoadUtils.GetCurrentLevel())
        {
            case 0:
                SpawnPlayer(GetCurrentSpawnPointPosition());
                break;
            case 1:
                Transform currentEndingTransform = Locator.GetInstance().GetEndingRoomManager().GetEndingRoom();
                spawnPoints.Insert(0, currentEndingTransform);
                SpawnPlayer(GetCurrentSpawnPointPosition());
                break;
        }

        firstSceneLoad = false;

    }



    private void Start()
    {
        Debug.Log("Spawn Manager start");

        int currentSpawnPointIndex = SaveLoadUtils.GetCurrentSpawnPointIndex();
        for (int i = 1; i < spawnPoints.Count; i++)
        {
            if (i <= currentSpawnPointIndex)
            {
                spawnPoints[i].GetComponent<Spawnpoint>().activated = true;
                spawnPoints[i].GetComponent<Renderer>().material.color = Color.cyan;
            }
        }

    }

    Vector2 GetCurrentSpawnPointPosition()
    {
        int currentSpawnPointIndex = SaveLoadUtils.GetCurrentSpawnPointIndex();
        return spawnPoints[currentSpawnPointIndex].position;
    }

    Transform GetCurrentSpawnPoint()
    {
        int currentSpawnPointIndex = SaveLoadUtils.GetCurrentSpawnPointIndex();
        return spawnPoints[currentSpawnPointIndex];
    }

    public void SetNewSpawnPoint(Transform spawnPoint)
    {
        int index = spawnPoints.FindIndex(t => t == spawnPoint);
        if (index == -1)
            Debug.LogError("Spawnpoint not in spawnPoint List");
        else
            SaveLoadUtils.SetCurrentSpawnPointIndex(index);
    }

    public void ReloadScene()
    {
        //(ri)carica la scena già presente
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SpawnPlayer(Vector2 spawnPoint)
    {
        Debug.Log("Spawn");
        player = Instantiate(prefabPlayer);
        //instanzia, cambia la posizione del player allo spawnpoint salvato, 
        //e lo setta nel locator in modo che gli altri script possano trovarlo
        player.transform.position = spawnPoint;
        if (flipPlayer)
            player.GetComponent<PlayerMovement>().FlipScale();
        Locator.GetInstance().SetPlayer(player);
    }


}
