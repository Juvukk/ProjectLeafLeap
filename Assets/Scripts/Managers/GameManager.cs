using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameEnded = false;
    public bool isEnding;
    [SerializeField] private float maxGameTime;
    [SerializeField] private float gameTime;
    [SerializeField] private float stopSpawninginterval;
    [SerializeField] private GameObject stopSpawnPrefab;
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
        stopSpawninginterval = maxGameTime - stopSpawninginterval;
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameTime < maxGameTime)
        {
            gameTime += 1 * Time.deltaTime;
        }

        if (gameTime >= stopSpawninginterval && !isEnding)
        {
            GameObject temp = Instantiate(stopSpawnPrefab, spawner);
            temp.transform.parent = null;
            isEnding = true;
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