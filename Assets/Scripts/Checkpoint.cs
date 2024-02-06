using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private AudioSource checkpointSound;

    private Animator anim;
    private bool checker = false;

    public static Vector2 checkPointPos;

    private void Start()
    {
        checkpointSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !checker)
        {
            CheckPointActivate();
        }
    }

    private void CheckPointActivate()
    {
        checkPointPos = transform.position;
        checkpointSound.Play();
        anim.SetTrigger("activate");
        //magic number, duration of animation_activated
        float playTime = 1f / 24f * 25f;
        Invoke("CheckpointLoop", playTime);
        checker = !checker;
    }
    
    private void CheckpointLoop()
    {
        anim.SetBool("is_activated", true);
    }
}
