using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private GameObject LeafPrefab;
    [SerializeField] private AudioClip[] LeafEffect;
    [SerializeField] private AudioSource source;

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("end Game");
            LeafPrefab.SetActive(true);
            int rand = Random.Range(0, LeafEffect.Length);
            source.PlayOneShot(LeafEffect[rand]);
            EventManager.endGame?.Invoke();
        }
    }
}