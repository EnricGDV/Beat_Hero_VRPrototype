using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    GameManager gameManagerScript;
    private AudioSource sliceAudioClip;
    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sliceAudioClip = GameObject.Find("Slice Audio").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            gameManagerScript.BreakCombo();
            gameManagerScript.EmmitParticles(2);
            Destroy(other.gameObject);

            if (sliceAudioClip != null)
                sliceAudioClip.Play();
        }
        else if (other.tag == "EnemyAttack")
        {
            gameManagerScript.ManageScore(1);
            gameManagerScript.EmmitParticles(1);
            gameManagerScript.EmmitParticles(3, other.transform);
            Destroy(other.gameObject);

            if (sliceAudioClip != null)
                sliceAudioClip.Play();
        }
        else if (other.tag == "EnemyDefend")
        {
            gameManagerScript.BreakCombo();
            gameManagerScript.EmmitParticles(2);
            Destroy(other.gameObject);

            if (sliceAudioClip != null)
                sliceAudioClip.Play();
        }
    }
}
