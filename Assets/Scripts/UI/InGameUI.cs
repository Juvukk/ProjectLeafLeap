using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private ScoreSystem scoreRef;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private TextMeshProUGUI ratingText;
    [SerializeField] private TextMeshProUGUI obstaclesHitText;
    [SerializeField] private GameObject endPanel;

    private void OnEnable()
    {
        EventManager.endGame += StartRoutine;
    }

    private void OnDisable()
    {
        EventManager.endGame -= StartRoutine;
    }

    // Start is called before the first frame update
    private void Start()
    {
        scoreRef = GetComponent<ScoreSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void StartRoutine()
    {
        StopCoroutine(ShowScore());
        StartCoroutine(ShowScore());
    }

    private IEnumerator ShowScore()
    {
        yield return new WaitForSeconds(.5f);
        endScoreText.text = "Score:" + scoreRef.score.ToString("f0");
        obstaclesHitText.text = "Obstacles Hit:" + scoreRef.obstaclesHit.ToString();
        ratingText.text = scoreRef.rating;
        endPanel.SetActive(true);
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0;
        yield return null;
    }
}