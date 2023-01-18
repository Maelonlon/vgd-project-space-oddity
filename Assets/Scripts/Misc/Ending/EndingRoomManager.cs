using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingRoomManager : MonoBehaviour
{
    //Punto dove spawna il giocatore




    public GameObject prefabBlackHole, prefabEndingRoom;

    public List<Transform> endingRooms;
    public List<Transform> endingDoors;

    public Transform GetEndingRoom()
    {

        return endingRooms[SaveLoadUtils.GetCurrentWinRoomIndex()];

    }


    private void Start()
    {
        SetWinRoom();


    }

    public void SetWinRoom()
    {
        int index = SaveLoadUtils.GetCurrentWinRoomIndex();
        SetWinRoom(index);

    }


    public void SetWinRoom(int index)
    {
        for (int i = 0; i < endingRooms.Count; i++)
        {
            if (i == index)
            {
                GameObject endingRoom = Instantiate(prefabEndingRoom);
                if (SaveLoadUtils.GetCurrentLevel() == 1) // load level 2 with door closed and without button
                {
                    endingRoom.GetComponent<EnemySpawner>().ClosePortal();
                    endingRoom.GetComponent<EndingRoom>().DestroyButton();
                }
                else
                {
                    endingDoors[i].gameObject.SetActive(true);
                }
                endingRoom.transform.position = endingRooms[i].transform.position;
                endingRoom.transform.localScale = endingRooms[i].transform.localScale;
            }
            else
            {
                GameObject blackHole = Instantiate(prefabBlackHole);
                blackHole.transform.position = endingRooms[i].transform.position;
                blackHole.transform.localScale = endingRooms[i].transform.localScale;
            }
        }
    }





}
