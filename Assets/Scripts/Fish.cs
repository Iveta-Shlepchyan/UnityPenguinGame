using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] AudioClip eatSound;
    [SerializeField, Range(0f, 1f)] float volume = 0.5f;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(eatSound, Camera.main.transform.position, volume);
            Destroy(gameObject);
        }
    }
    
}
