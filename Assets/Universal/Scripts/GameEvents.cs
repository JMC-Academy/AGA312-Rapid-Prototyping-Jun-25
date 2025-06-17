using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnPlayerHit = null;
    public static event Action<GameObject> OnPlayerDied = null;

    public static void ReportOnPlayerHit() => OnPlayerHit?.Invoke();
    public static void ReportOnPlayerDied(GameObject _go) => OnPlayerDied?.Invoke(_go);
}
