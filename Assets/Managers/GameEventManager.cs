using UnityEngine;
using System.Collections;

public class GameEventManager : MonoBehaviour {

    public delegate void GameEvent();

    public static event GameEvent GameStart, GameOver, DayStart, NightStart;

    public static void TriggerGameStart()
    {
        if (GameStart != null)
        {
            GameStart();
        }
    }

    public static void TriggerGameOver()
    {
        if (GameOver != null)
        {
            GameOver();
        }
    }
}
