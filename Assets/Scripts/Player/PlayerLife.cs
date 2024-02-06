using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;

    private Vector2 respawnPoint;
    private bool isDead;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnPoint = transform.position;

        isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint") && respawnPoint != Checkpoint.checkPointPos)
        {
            respawnPoint = Checkpoint.checkPointPos;
        }

        if (collision.gameObject.CompareTag("Trap") && !isDead)
        {
            Die();
        }
    }
    
    //Totally a lame way to bypass respawn bug, can't think of anything else for the moment
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint") && respawnPoint != Checkpoint.checkPointPos)
        {
            respawnPoint = Checkpoint.checkPointPos;
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        player.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        isDead = true;
    }

    private void Respawn()
    {
        anim.SetTrigger("respawn");
        player.bodyType = RigidbodyType2D.Dynamic;
        transform.position = respawnPoint;
        isDead = false;
    }
}
