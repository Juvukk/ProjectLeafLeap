using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] meshes;

    // I have this bool because for some reason just calling randomise inside of OnEnable wasn't working correctly
    private bool allowRand;

    private void OnEnable()
    {
        Randomise();
    }

    public void Randomise()
    {
        int randomInt = Random.Range(0, meshes.Length);
        Debug.Log("rand" + randomInt);

        for (int i = 0; i < meshes.Length; i++)
        {
            if (i != randomInt)
            {
                meshes[i].SetActive(false);
            }
            else
            {
                meshes[i].SetActive(true);
            }
        }

        allowRand = false;
    }
}