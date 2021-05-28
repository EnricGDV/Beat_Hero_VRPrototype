using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    GameManager gameManagerScript;
    public AudioSource sliceAudioClip;
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
            sliceAudioClip.Play();
        }
        else if (other.tag == "EnemyAttack")
        {
            gameManagerScript.ManageScore(1);
            gameManagerScript.EmmitParticles(3, other.transform);
            Destroy(other.gameObject);
            sliceAudioClip.Play();
        }
        else if (other.tag == "EnemyDefend")
        {
            gameManagerScript.BreakCombo();
            Destroy(other.gameObject);
            sliceAudioClip.Play();
        }
    }
}
