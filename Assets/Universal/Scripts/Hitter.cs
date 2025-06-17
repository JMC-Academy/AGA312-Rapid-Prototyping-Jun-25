using System.Runtime.Serialization;
using UnityEngine;

public class Hitter : MonoBehaviour
{
    private void OnPlayerHit()
    {
        GetComponent<Renderer>().material.color = ColorX.GetRandomColor();
    }

    private void OnPlayerDied(GameObject obj)
    {
        obj.transform.localScale += Vector3.one * 2;
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerHit += OnPlayerHit;
        GameEvents.OnPlayerDied += OnPlayerDied;
    }

    

    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= OnPlayerHit;
        GameEvents.OnPlayerDied -= OnPlayerDied;
    }
}
