using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem DustTrailEffect;
    //[SerializeField] AudioSource jump;
    //[SerializeField] AudioSource snowboard;

    [SerializeField] AudioClip snowboardSFX;

    


    /* void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            //snowboard.Play();
            DustTrailEffect.Play();
            //jump.Play();
            GetComponent<AudioSource>().Play();
        }
        
    }

    void OnCollisionExit2D(Collision2D other) 
    {    
        if(other.gameObject.tag == "Ground")
        {
            DustTrailEffect.Stop();
            //snowboard.Stop();
           // GetComponent<AudioSource>().Stop();
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
            //snowboard.Play();
            //GetComponent<AudioSource>().PlayOneShot(snowboardSFX);
        }
    } */
}
