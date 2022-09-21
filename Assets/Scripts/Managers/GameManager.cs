using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameEnded = false;
    [SerializeField] private float maxGameTime;
    [SerializeField] private float gameTime;
    [SerializeField] private GameObject jumpPrefab;
    [SerializeField] private Transform spawner;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameTime < maxGameTime)
        {
            gameTime += 1 * Time.deltaTime;
        }
        else if (gameTime >= maxGameTime && !gameEnded)
        {
            GameObject temp = Instantiate(jumpPrefab, spawner);
            temp.transform.parent = null;
            gameEnded = true;
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}