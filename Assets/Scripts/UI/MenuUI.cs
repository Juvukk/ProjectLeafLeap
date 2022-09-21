using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void setPause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }

        if (!isPaused)
        {
            Time.timeScale = 1;
        }
    }

    public void ShowCredits(bool show)
    {
        creditsPanel.SetActive(show);
    }
}