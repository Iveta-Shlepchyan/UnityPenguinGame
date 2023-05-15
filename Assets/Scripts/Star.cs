using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] AudioClip starSound;
    [SerializeField, Range(0f, 1f)] float volume = 0.5f;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(starSound, Camera.main.transform.position, volume);
            Destroy(gameObject);
        }
    }
}
