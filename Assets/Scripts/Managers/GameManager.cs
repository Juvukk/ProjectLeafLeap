using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameEnded = false;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameEnded)
        {
            EventManager.endGame?.Invoke();
            gameEnded = false;
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}