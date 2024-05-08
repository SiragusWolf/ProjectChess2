using System;
using System.Collections;
using System.Collections.Generic;
using Facade;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Collider2D _collider;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip winSound;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision");
        if (other.CompareTag("EnemyMovement"))
        {
            SoundEffect(loseSound);
            GameManager.Instance.LoseGame();
            //Destroy(this.gameObject);
        }
        else if (other.CompareTag("goal"))
        {
            SoundEffect(winSound);
            GameManager.Instance.WinGame();
            //Destroy(this);
        }
    }

    public void SoundEffect(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }
}
