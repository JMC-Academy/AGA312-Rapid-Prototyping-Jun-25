using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMPro.TMP_Text score;
    private int scoreValue = 0;

    private void OnPlayerHit()
    {
        scoreValue += 100;
        score.text = scoreValue.ToString();
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerHit += OnPlayerHit;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerHit -= OnPlayerHit;
    }
}
