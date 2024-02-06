using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private int timeScoreBase = 2000;
    [SerializeField] private int timeScoreMultiplier = 10;
    [SerializeField] private int minTimeScore = 1000;
    [SerializeField] private bool goToNextLevel = true;
    [SerializeField] private float sceneDelay = 2f;
    private bool endLevel = false;

    [SerializeField] private AudioSource victorySound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        victorySound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !endLevel)
        {
            endLevel = true;
            PlayerMovement.canMove = false;
            ScoreCalc();
            Animate();
            Invoke("NextLevel", sceneDelay);
        }
    }

    private void ScoreCalc()
    {
        float time = Timer.currentTime;

        if(time * timeScoreMultiplier <= timeScoreBase)
        {
            ItemCollector.score += (int)(timeScoreBase - time * timeScoreMultiplier);
        }

        ItemCollector.score += minTimeScore;
    }

    private void Animate()
    {
        victorySound.Play();
        anim.SetTrigger("end_level");
    }

    private void NextLevel()
    {
        Timer.ResetTime();
        if (goToNextLevel)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 != SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
