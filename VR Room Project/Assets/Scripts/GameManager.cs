using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private bool firstAttack = true;
    private bool firstDefend = true;
    private bool firstObstacle = true;

    public int maxHealth;
    private int currentHealth;

    private GameState gameState;

    public AudioSource song;

    public ParticleSystem particleSystem_1;
    public ParticleSystem particleSystem_2;
    public ParticleSystem particleSystem_3;

    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI hpText;


    void Start()
    {
        currentHealth = maxHealth;
        gameState = GameState.GAME_STARTING;
        particleSystem_1.emissionRate = 0;
        particleSystem_2.emissionRate = 0;
        particleSystem_3.emissionRate = 0;
        song.volume = 0.5f;
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
                            case 1: //Left Row
                                int pref = Random.Range(1, 3);
                                if(pref == 1)
                                {
                                    Instantiate(enemyDef, new Vector3(leftSpawner.position.x, leftSpawner.position.y, leftSpawner.position.z), Quaternion.identity);
                                    if (firstDefend)
                                    {
                                        firstDefend = !firstDefend;
                                    }
                                    if (!firstDefend)
                                    {
                                        enemyDef.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                }
                                else if(pref == 2)
                                {
                                    Instantiate(obstacle, new Vector3(leftSpawner.position.x, leftSpawner.position.y, leftSpawner.position.z), Quaternion.identity);
                                    if (firstObstacle)
                                    {
                                        firstObstacle = !firstObstacle;
                                    }
                                    if (!firstObstacle)
                                    {
                                        obstacle.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                }
                                
                                break;
                            case 2: //Mid Row
                                int pref2 = Random.Range(1, 4);
                                if (pref2 == 1)
                                {
                                    Instantiate(enemyDef, new Vector3(MidSpawner.position.x, MidSpawner.position.y, MidSpawner.position.z), Quaternion.identity);
                                    if (firstDefend)
                                    {
                                        firstDefend = !firstDefend;
                                    }
                                    if (!firstDefend)
                                    {
                                        enemyDef.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                }
                                else if (pref2 == 2)
                                {
                                    Instantiate(obstacle, new Vector3(MidSpawner.position.x, MidSpawner.position.y, MidSpawner.position.z), Quaternion.identity);
                                    if (firstObstacle)
                                    {
                                        firstObstacle = !firstObstacle;
                                    }
                                    if (!firstObstacle)
                                    {
                                        obstacle.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                }
                                else if (pref2 == 3)
                                {
                                    Instantiate(enemyAttack, new Vector3(MidSpawner.position.x, MidSpawner.position.y, MidSpawner.position.z), Quaternion.identity);
                                    if (firstAttack)
                                    {
                                        firstAttack = !firstAttack;
                                    }
                                    if (!firstAttack)
                                    {
                                        enemyAttack.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                }
                                break;
                            case 3: //Right Row
                                int pref3 = Random.Range(1, 3);
                                if (pref3 == 1)
                                {
                                    Instantiate(obstacle, new Vector3(RightSpawner.position.x, RightSpawner.position.y, RightSpawner.position.z), Quaternion.identity);
                                    if (firstObstacle)
                                    {
                                        firstObstacle = !firstObstacle;
                                    }
                                    if (!firstObstacle)
                                    {
                                        obstacle.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                }
                                else if (pref3 == 2)
                                {
                                    Instantiate(enemyAttack, new Vector3(RightSpawner.position.x, RightSpawner.position.y, RightSpawner.position.z), Quaternion.identity);
                                    if (firstAttack)
                                    {
                                        firstAttack = !firstAttack;
                                    }
                                    if (!firstAttack)
                                    {
                                        enemyAttack.GetComponentInChildren<Canvas>().enabled = false;
                                    }
                                }
                                break;
                        }
                        hpText.text = currentHealth.ToString() + " HP";
                        timer = 0.0f;
                    }

                    if (currentHealth <= 0 || currentHealth > 20)
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
            enemies.GetComponentInChildren<Transform>().Rotate(Time.deltaTime*50, Time.deltaTime*150, 0);
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
        pointsText.text = score.ToString() + " pts";
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
            EmmitParticles(1);
        }
        else if(other.tag == "EnemyAttack")
        {
            Destroy(other.gameObject);
            BreakCombo();
            EmmitParticles(2);
        }
        else if (other.tag == "EnemyDefend")
        {
            Destroy(other.gameObject);
            BreakCombo();
            EmmitParticles(2);
        }
    }

    public void EmmitParticles(int type, Transform transform = null)
    {
        switch (type)
        {
            case 0:
                {
                    return;
                }
            case 1:
                {
                    // Right
                    particleSystem_1.Emit(10);
                    break;
                }
            case 2:
                {
                    // Wrong
                    particleSystem_2.Emit(10);
                    break;
                }
            case 3:
                {
                    // Death
                    if (transform != null) 
                    particleSystem_3.gameObject.transform.position = transform.position;
                    particleSystem_3.Emit(100);
                    break;
                }
        }
    }

    public void CheckCanvas(GameObject pref)
    {

    }
}
