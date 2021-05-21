using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            gameManagerScript.BreakCombo();
            Destroy(other.gameObject);
        }
        else if (other.tag == "EnemyAttack")
        {
            gameManagerScript.BreakCombo();
            Destroy(other.gameObject);
        }
        else if (other.tag == "EnemyDefend")
        {
            gameManagerScript.BreakCombo();
            Destroy(other.gameObject);
        }
    }
}
