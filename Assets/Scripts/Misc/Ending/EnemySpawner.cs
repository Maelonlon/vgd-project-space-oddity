using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;

    public List<ObjectPool<PoolObject>> enemiesPools;

    GameObject player;

    public float triggerDistance;
    bool isTriggered = false;

    public float spawnRate = 5f;
    public float minSpeed = 2f;
    public float maxSpeed = 10f;

    public Animator doorAnimator;




    float timer = 0f;
    private void Start()
    {
        enemiesPools = new List<ObjectPool<PoolObject>>();
        foreach (GameObject prefab in enemyPrefabs)
        {
            enemiesPools.Add(new ObjectPool<PoolObject>(prefab, 5));
        }
        player = Locator.GetInstance().GetPlayer();
    }

    private void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < triggerDistance && !isTriggered)
        {
            isTriggered = true;
        }
        if (isTriggered && !stopSpawning)
        {
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }

    }

    bool stopSpawning = false;

    public void ClosePortal()
    {
        stopSpawning = true;
        doorAnimator.SetTrigger("closeDoor");
    }



    void SpawnEnemy()
    {
        int prefabIndex = Random.Range(0, enemiesPools.Count);

        float randomX;
        float randomY;
        if (Random.Range(0, 2) == 0) //gets a random position in the door frame
        {
            randomX = Random.Range(-5f, 5f);
            randomY = (Random.Range(0, 2) == 0) ? Random.Range(-5f, -4f) : Random.Range(5f, 4f);
        }
        else
        {
            randomX = (Random.Range(0, 2) == 0) ? Random.Range(-5f, -4f) : Random.Range(5f, 4f);
            randomY = Random.Range(-5f, 5f);
        }

        Vector2 randomPosition = (Vector2)transform.position + new Vector2(randomX, randomY);

        Vector2 direction = ((Vector2)transform.position - randomPosition).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject enemy = enemiesPools[prefabIndex].PullGameObject(randomPosition, Quaternion.identity);

        AlienMovement movement = enemy.GetComponent<AlienMovement>();
        movement.angle = angle;
        movement.bounceOnWallHit = false;
        movement.dieOnWallHit = false;
        movement.speed = Random.Range(minSpeed, maxSpeed);
        movement.distance = new Vector2(100, 100);

        movement.SetAngle(angle);

        movement.DeactivateIn(5f);

        movement.ResetLayer();
        movement.MoveLayerAboveIn(0.5f);

    }





}
