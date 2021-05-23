using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GAME_STARTING,
        GAME_RUNNING,
        GAME_PAUSED,
        GAME_STOPPING,
        GAME_STOP,
        NONE
    }

    // Start is called before the first frame update
    public Transform leftSpawner;
    public Transform MidSpawner;
    public Transform RightSpawner;

    [Header("Spawnables")]
    public GameObject enemyAttack;
    public GameObject enemyDef;
    public GameObject obstacle;

    public float speed;
    public float spawnTime;
    private float timer = 0.0f;
    private GameObject[] objs;

    private float score = 0.0f;
    private float scoreMultiplier = 1.0f;
    private int currentStreak = 0;
    public int streakInterval;

    public uint maxHealth;
    private uint currentHealth;

    private GameState gameState;

    public AudioSource song;

    
    void Start()
    {
        currentHealth = maxHealth;
        gameState = GameState.GAME_STARTING;
        //song = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.GAME_STARTING:
                {
                    song.Play();
                    gameState = GameState.GAME_RUNNING;
                    break;
                }
            case GameState.GAME_RUNNING:
                {
                    if (timer > spawnTime)
                    {
                        int n = Random.Range(1, 4);

                        switch (n)
                        {
                            case 1:
                                Instantiate(enemyDef, new Vector3(leftSpawner.position.x, leftSpawner.position.y, leftSpawner.position.z), Quaternion.identity);
                                break;
                            case 2:
                                Instantiate(obstacle, new Vector3(MidSpawner.position.x, MidSpawner.position.y, MidSpawner.position.z), Quaternion.identity);
                                break;
                            case 3:
                                Instantiate(enemyAttack, new Vector3(RightSpawner.position.x, RightSpawner.position.y, RightSpawner.position.z), Quaternion.identity);
                                break;
                        }
                        timer = 0.0f;
                    }

                    if (currentHealth <= 0)
                        gameState = GameState.GAME_STOPPING;

                    timer += Time.deltaTime;
                    MoveEnemies();

                    break;
                }
            case GameState.GAME_STOPPING:
                {
                    Reset();

                    break;
                }
            case GameState.GAME_STOP:
                {
                    break;
                }
        }
    }

    public void MoveEnemies()
    {
        objs = GameObject.FindGameObjectsWithTag("EnemyAttack");

        foreach (GameObject enemies in objs)
        {
            float step = speed * Time.deltaTime;

            enemies.transform.position = Vector3.MoveTowards(enemies.transform.position, new Vector3(1000.0f, enemies.transform.position.y, enemies.transform.position.z), step);
        }

        objs = GameObject.FindGameObjectsWithTag("EnemyDefend");

        foreach (GameObject enemies in objs)
        {
            float step = speed * Time.deltaTime;

            enemies.transform.position = Vector3.MoveTowards(enemies.transform.position, new Vector3(1000.0f, enemies.transform.position.y, enemies.transform.position.z), step);
        }

        objs = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject enemies in objs)
        {
            float step = speed * Time.deltaTime;

            enemies.transform.position = Vector3.MoveTowards(enemies.transform.position, new Vector3(1000.0f, enemies.transform.position.y, enemies.transform.position.z), step);
        }

    }

    public void ManageScore(float difference)
    {
        //Update the streak
        currentStreak++;

        //Update the score multiplier
        scoreMultiplier = currentStreak % streakInterval + 1;

        //Update the score
        score += difference * scoreMultiplier;
    }

    public void BreakCombo()
    {
        currentHealth--;
        currentStreak = 0;
    }

    public void Reset()
    {
        //Reset values
        currentHealth = maxHealth;
        currentStreak = 0;
        score = 0.0f;
        timer = 0.0f;

        //Destroy enemies that are still alive
        objs = GameObject.FindGameObjectsWithTag("EnemyAttack");
        foreach (GameObject enemies in objs)
        {
            Destroy(enemies);
        }
        objs = GameObject.FindGameObjectsWithTag("EnemyDefend");
        foreach (GameObject enemies in objs)
        {
            Destroy(enemies);
        }
        objs = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject enemies in objs)
        {
            Destroy(enemies);
        }

        song.Stop();

        gameState = GameState.GAME_STOP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            //Debug.Log("HIT");
            Destroy(other.gameObject);
            ManageScore(1);
        }
        else if(other.tag == "EnemyAttack")
        {
            Destroy(other.gameObject);
            BreakCombo();
        }
        else if (other.tag == "EnemyDefend")
        {
            Destroy(other.gameObject);
            BreakCombo();
        }
    }
}
