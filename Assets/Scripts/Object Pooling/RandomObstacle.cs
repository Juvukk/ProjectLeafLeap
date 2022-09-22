using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] meshes;

    private void OnEnable()
    {
        Randomise();
    }

    public void Randomise()
    {
        int randomInt = Random.Range(0, meshes.Length);

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
    }
}