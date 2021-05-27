using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    GameManager gameManagerScript;
    ParticleSystem particleSystem;

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
            gameManagerScript.ManageScore(1);
            particleSystem = other.GetComponentInChildren<ParticleSystem>();
            particleSystem.Emit(10);
            Destroy(other.gameObject);
        }
        else if (other.tag == "EnemyDefend")
        {
            gameManagerScript.BreakCombo();
            Destroy(other.gameObject);
        }
    }
}
