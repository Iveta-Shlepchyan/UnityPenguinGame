using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delayDuration = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    CircleCollider2D headCollider;
    bool hasCrashed = false;

    void Start() {
        headCollider = GetComponent<CircleCollider2D>();
    }

    void Update() {
        detectCrash();
    }


    void detectCrash()
    {
        if(headCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", delayDuration);
        }
            
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
