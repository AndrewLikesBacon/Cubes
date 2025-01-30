using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private MoneyHolder moneyHolder;
    [SerializeField] private ShootUltimate shootUltimate;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private Movement playerMovement;
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private float spawnWidth;
    [SerializeField] private int centerGapSize;
    [SerializeField] private float spawnYoffset;
    [SerializeField] private int spawnLimit;
    [SerializeField] private float minEnemySpeed;
    [SerializeField] private float maxEnemySpeed;
    private float lastSpawnTime;
    private float spawnX;
    private int enemyCount;

    void Start()
    {

        lastSpawnTime = Time.time;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }
    

    void Update()
    {

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //Difficulty ramping
        //timeBetweenSpawns = 1/(Time.time/50);

        if ( ( enemyCount < spawnLimit ) && ( Time.time - timeBetweenSpawns > lastSpawnTime ) ) {

            spawnX = UnityEngine.Random.Range(-spawnWidth/2, spawnWidth/2);

            if (spawnX > 0) {

                spawnX += centerGapSize;

            } else {

                spawnX -= centerGapSize;

            }

            spawnX += player.transform.position.x;

            transform.position = new Vector2(spawnX, transform.position.y);

            GameObject enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<Transform>().position = new Vector2(spawnX, player.position.y + spawnYoffset);
            enemy.GetComponent<Follow>().targetObj = player;
            enemy.GetComponent<AttackPlayer>().player = player;
            enemy.GetComponent<AttackPlayer>().playerRb = playerRb;
            enemy.GetComponent<Follow>().SetFollowSpeed(UnityEngine.Random.Range(minEnemySpeed, maxEnemySpeed));
            enemy.GetComponent<Health>().moneyHolder = moneyHolder;
            enemy.GetComponent<Health>().shootUltimate = shootUltimate;
            enemy.GetComponent<Health>().playerHealth = playerHealth;
            enemy.GetComponent<Health>().deathSound = deathSound;
            enemy.GetComponent<BombEnemy>().player = player;
            enemy.GetComponent<BombEnemy>().playerRb = playerRb;
            enemy.GetComponent<BombEnemy>().playerMovement = playerMovement;
            enemy.GetComponent<BombEnemy>().explosionSound = explosionSound;

            lastSpawnTime = Time.time;

        }
    }
}
