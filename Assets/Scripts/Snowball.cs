using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;
    [SerializeField, Range(0f, 1f)] float volume = 0.5f;
    [SerializeField] float snowballSpeedX = 10f;
    [SerializeField] float snowballSpeedY = 5f;

    Rigidbody2D rb;
    PlayerController player;
    float xSpeed, ySpeed;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        xSpeed = player.transform.localScale.x * snowballSpeedX;
        ySpeed = player.transform.localScale.y * snowballSpeedY;

        if(player.GetComponent<SpriteRenderer>().flipX) xSpeed = -xSpeed;

        rb.AddForce(new Vector2(xSpeed, ySpeed), ForceMode2D.Impulse);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        //crashEffect.Play();
        //ParticleSystem instance = Instantiate(crashEffect, this.transform.position, Quaternion.identity);
        //Destroy(instance.gameObject,  instance.main.duration + instance.main.startLifetime.constantMax);

        AudioSource.PlayClipAtPoint(crashSFX, Camera.main.transform.position,volume);
        //Destroy(gameObject, 0.1f);
        Destroy(gameObject);
    }
}
