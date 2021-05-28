using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    GameManager gameManagerScript;
    public Transform Head;
    public Transform Feet;
    public AudioSource hurtAudioClip;

    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        gameObject.transform.position = new Vector3(Head.position.x, Feet.position.y, Head.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            gameManagerScript.BreakCombo();
            Destroy(other.gameObject);
            hurtAudioClip.Play();
        }
        else if (other.tag == "EnemyAttack")
        {
            gameManagerScript.BreakCombo();
            Destroy(other.gameObject);
            hurtAudioClip.Play();
        }
        else if (other.tag == "EnemyDefend")
        {
            gameManagerScript.BreakCombo(); 
            Destroy(other.gameObject);
            hurtAudioClip.Play();
        }
    }
}
