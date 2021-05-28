using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    GameManager gameManagerScript;
    private AudioSource defendAudioClip;
    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        defendAudioClip = GameObject.Find("Defend Audio").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            gameManagerScript.BreakCombo();
            gameManagerScript.EmmitParticles(2);
            Destroy(other.gameObject);

            if (defendAudioClip != null)
                defendAudioClip.Play(); 
        }
        else if (other.tag == "EnemyAttack")
        {
            gameManagerScript.BreakCombo();
            gameManagerScript.EmmitParticles(2);
            Destroy(other.gameObject);

            if (defendAudioClip != null)
                defendAudioClip.Play();
        }
        else if (other.tag == "EnemyDefend")
        {
            gameManagerScript.ManageScore(1);
            gameManagerScript.EmmitParticles(1);
            gameManagerScript.EmmitParticles(3, other.transform);
            Destroy(other.gameObject);

            if (defendAudioClip != null)
                defendAudioClip.Play();
        }
    }
}
