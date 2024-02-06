using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public static int score = 0;
    public static int restartScore;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private int cherryScore = 50;

    private void Start()
    {
        ScoreToText();
        restartScore = score;
    }

    private void Update()
    {
        ScoreToText();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            score += cherryScore;
        }
    }

    private void ScoreToText()
    {
        scoreText.text = "Score:" + "\n" + score.ToString();
    }

    public static void ResetScore()
    {
        score = 0;
    }
}
