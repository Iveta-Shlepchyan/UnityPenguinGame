using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    [SerializeField, Range(0f, 1f)] float volume = 0.5f;
    [SerializeField] float speedToBreake = 20f;


    bool hasEnoughSpeedToBreak = false;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponentInChildren<Rigidbody2D>();
        if (rb != null)
        {
            float playerHorizontalVelocity = Mathf.Abs(rb.velocity.x);
            Debug.Log(playerHorizontalVelocity);
            if(playerHorizontalVelocity >= speedToBreake)
            {
                hasEnoughSpeedToBreak = true;
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        hasEnoughSpeedToBreak = false;
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(hasEnoughSpeedToBreak)
        {
            crashEffect.Play();
            AudioSource.PlayClipAtPoint(crashSFX, Camera.main.transform.position, volume);
            Destroy(gameObject);
        }
    }


}
