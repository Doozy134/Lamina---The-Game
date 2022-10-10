using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    //setting variables
    [SerializeField] private int maxScore = 1000;
    [SerializeField] private int minScore = 50;
    [SerializeField] private int rate = 30;
    private int deductAmount = 100;

    private int score;
    private TextMeshProUGUI scoreShowing;
    

    private void Start()
    {
        //getting references and setting inital score
        scoreShowing = gameObject.transform.Find("concurrentScore").GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    private void Update()
    {
        //update score each frame
        scoreShowing.text = score.ToString();
    }
    public void deductScore()
    {
        //checking for exception and removing score
        if (score - deductAmount > 0)
        {
            score -= deductAmount;
        }
        else
            score = 0;
    }

    public void addScore(int scoreToAdd)
    {
        //adding new score from parameter
        score += scoreToAdd;
    }

    public int calcScore(double tt)
    {
        //calculating score from amount of time
        double scoreForQuestion = maxScore * ((rate - tt) / rate);
        //converting to int
        int scoreRewarded = Mathf.RoundToInt((float)scoreForQuestion);

        //checking for exceptions
        if (scoreRewarded > maxScore)
            scoreRewarded = minScore;
        else if (scoreRewarded < minScore)
            scoreRewarded = minScore;

        //adding new score
        score += scoreRewarded;

        return scoreRewarded;
    }
}
