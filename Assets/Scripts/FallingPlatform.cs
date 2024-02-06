using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private BoxCollider2D coll;

    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float colorHueTimeChange = 0.1f;
    [SerializeField] private float returnDelay = 2f;

    private bool activated;
    private float timeSinceChange = 0f;
    private float colorHue = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();    
        sprite = GetComponent<SpriteRenderer>();

        anim.SetBool("active", true);
        activated = false;
    }

    private void Update()
    {
        if (activated)
        {
            timeSinceChange += Time.deltaTime;

            if (timeSinceChange >= colorHueTimeChange)
            {
                colorHue += colorHueTimeChange;
                Debug.Log(sprite.color = new Color(1f, 1f - colorHue / fallDelay, 1f - colorHue / fallDelay, 1f));

                timeSinceChange = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Disable());
        }    
    }

    private IEnumerator Disable()
    {
        anim.SetBool("active", false);
        activated = true;

        yield return new WaitForSeconds(fallDelay);
        activated = false;
        sprite.color = new Color(1f, 1f, 1f, 0.3f);
        coll.enabled = false;

        yield return new WaitForSeconds(returnDelay);
        sprite.color = new Color(1f, 1f, 1f, 1f);
        coll.enabled = true;
        colorHue = 0f;

        anim.SetBool("active", true);
    }
}
