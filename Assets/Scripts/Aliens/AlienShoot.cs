using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShoot : MonoBehaviour
{

    private ObjectPool<PoolObject> bulletPool;
    public float firingRate = 2f;     //1f = 1 secondo
    public float bulletSpeed = 3f;
    public float range = 10f;
    private float timer = 0f;

    public Sprite[] frames;

    public GameObject bulletPrefab;


    private GameObject player;
    private SpriteRenderer sprite;




    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new ObjectPool<PoolObject>(bulletPrefab, 10);
        sprite = GetComponent<SpriteRenderer>();
        player = Locator.GetInstance().GetPlayer();
        if (!player)
        {
            Debug.Log("Player reference null (AlienShoot.cs)");
        }
    }
    private void Update()
    {
        if (player)
        {
            Vector2 distanceVector = player.transform.position - transform.position;
            UpdateSpriteOnShootingDirection(distanceVector);
            timer += Time.deltaTime;
            if (distanceVector.magnitude < range && timer >= firingRate)
            {
                GameObject bulletObject = SpawnBullet();
                if (bulletObject)
                {
                    AlienBullet bullet = bulletObject.GetComponent<AlienBullet>();
                    bullet.Initialize(distanceVector);
                }
                timer = 0f;
            }
        }

    }



    public Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    void UpdateSpriteOnShootingDirection(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(-transform.right, direction.normalized);
        if (angle < 30f)
        {
            sprite.sprite = frames[0];// horizontal sprite
            if (sprite.flipX)
                sprite.flipX = false;
        }
        else if (angle < 70f)
        {
            sprite.sprite = frames[1];// diagonal sprite
            if (sprite.flipX)
                sprite.flipX = false;
        }
        else if (angle < 110f)
        {
            sprite.sprite = frames[2];// vertical sprite
        }
        else if (angle < 150f)
        {
            sprite.sprite = frames[1];// diagonal sprite
            if (!sprite.flipX)
                sprite.flipX = true;
        }
        else
        {
            sprite.sprite = frames[0];// horizontal sprite
            if (!sprite.flipX)
                sprite.flipX = true;
        }
    }

    GameObject SpawnBullet()
    {
        GameObject bullet = bulletPool.PullGameObject(transform.position, transform.rotation);

        bullet.GetComponent<AlienBullet>().speed = bulletSpeed;
        return bullet;

    }
}
