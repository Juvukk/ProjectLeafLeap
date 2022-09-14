using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public float score { get; private set; }
    public int obstaclesHit;
    public string rating;

    [SerializeField] private int scoreIncrease;
    [SerializeField] private int decreaseScore;

    [SerializeField] private int dThreshold;
    [SerializeField] private int cThreshold;
    [SerializeField] private int bThreshold;
    [SerializeField] private int aThreshold;

    [SerializeField] private bool allowIncrease = false;

    private void OnEnable()
    {
        EventManager.hitEvent += LowerScore;
        EventManager.endGame += GameEnded;
    }

    private void OnDisable()
    {
        EventManager.hitEvent -= LowerScore;
        EventManager.endGame -= GameEnded;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (allowIncrease)
        {
            score += scoreIncrease * Time.deltaTime;
        }
        else
        {
            DetermineRating();
        }
    }

    private void DetermineRating()
    {
        if (score < dThreshold)
        {
            rating = "D";
        }
        else if (score > dThreshold && score < cThreshold)
        {
            rating = "C";
        }
        else if (score > cThreshold && score < bThreshold)
        {
            rating = "B";
        }
        else if (score > bThreshold && score < aThreshold)
        {
            rating = "A";
        }
        else if (score > aThreshold)
        {
            rating = "S";
        }
        Debug.Log("rating =" + rating);
    }

    private void LowerScore()
    {
        allowIncrease = false;
        score -= decreaseScore;
        obstaclesHit = obstaclesHit + 1;
        allowIncrease = true;
    }

    private void GameEnded()
    {
        allowIncrease = false;
    }
}