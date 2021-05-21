using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            //Debug.Log("HIT");
            Destroy(other.gameObject);
           
        }
        else if (other.tag == "EnemyAttack")
        {
            Destroy(other.gameObject);
           
        }
        else if (other.tag == "EnemyDefend")
        {
            Destroy(other.gameObject);
        }
    }
}
