using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > spawnTime)
        {
            int n = Random.Range(1, 4);

            switch(n)
            {
                case 1:
                    Instantiate(enemyDef, new Vector3(leftSpawner.position.x, leftSpawner.position.y, leftSpawner.position.z),Quaternion.identity);
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

        timer += Time.deltaTime;
        MoveEnemies();
    }

    public void MoveEnemies()
    {
        objs = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemies in objs)
        {
            float step = speed * Time.deltaTime;

            enemies.transform.position = Vector3.MoveTowards(enemies.transform.position, new Vector3(1000.0f, enemies.transform.position.y, enemies.transform.position.z), step);
        }
    }
}
