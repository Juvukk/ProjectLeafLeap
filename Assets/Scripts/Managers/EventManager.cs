using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void LoadScene(int index);

    public static LoadScene sceneLoad;

    public delegate void PlayerHitObstacle();

    public static PlayerHitObstacle hitEvent;

    public delegate void EndGameEvent();

    public static EndGameEvent endGame;

    public delegate void BeginEnd();

    public static BeginEnd endBegin;
}