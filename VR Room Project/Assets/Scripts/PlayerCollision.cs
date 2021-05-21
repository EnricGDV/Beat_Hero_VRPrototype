using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            //Debug.Log("HIT");
            Destroy(other.gameObject);
            //points++;
        }
        else if (other.tag == "EnemyAttack")
        {
            Destroy(other.gameObject);
            //points--;
            //maxHealth--;
        }
        else if (other.tag == "EnemyDefend")
        {
            Destroy(other.gameObject);
            //points--;
           // maxHealth--;
        }
    }
}
