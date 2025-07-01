using UnityEngine;
using DG.Tweening;
using TMPro;

public class TweenFun : GameBehaviour
{
    public enum Direction { North, East, South, West}
    public Transform player;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveTweenTime = 1f;
    [SerializeField] private float shakeStrength = 0.4f;
    [SerializeField] private Ease moveEase;
    [SerializeField] private bool gridMovement;
    private bool isMoving = false;

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int scoreBonus = 100;
    [SerializeField] private float scoreTweenTime = 1f;
    [SerializeField] private Ease scoreEase;
    private int score;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            MovePlayer(Direction.North);
        if (Input.GetKeyDown(KeyCode.D))
            MovePlayer(Direction.East);
        if (Input.GetKeyDown(KeyCode.S))
            MovePlayer(Direction.South);
        if (Input.GetKeyDown(KeyCode.A))
            MovePlayer(Direction.West);
    }

    private void MovePlayer(Direction _direction)
    {
        if (isMoving && gridMovement)
            return;

        isMoving = true;

        switch (_direction)
        {
            case Direction.North:
                player.transform.DOLocalMoveZ(player.transform.localPosition.z + moveDistance, moveTweenTime).
                    SetEase(moveEase).
                    OnComplete(TweenComplete);
                break;
            case Direction.East:
                player.transform.DOLocalMoveX(player.transform.localPosition.x + moveDistance, moveTweenTime).
                    SetEase(moveEase).
                    OnComplete(TweenComplete);
                break;
            case Direction.South:
                player.transform.DOLocalMoveZ(player.transform.localPosition.z - moveDistance, moveTweenTime).
                    SetEase(moveEase).
                    OnComplete(TweenComplete);
                break;
            case Direction.West:
                player.transform.DOLocalMoveX(player.transform.localPosition.x - moveDistance, moveTweenTime).
                    SetEase(moveEase).
                    OnComplete(TweenComplete);
                break;

        }
        ChangeColour();
        
    }

    private void TweenComplete()
    {
        ScreenShake();
        IncreaseScore();
        isMoving = false;
    }

    private void ChangeColour() => player.GetComponent<Renderer>().material.DOColor(ColorX.GetRandomColor(), moveTweenTime);

    private void ScreenShake() => Camera.main.DOShakePosition(moveTweenTime / 2, shakeStrength);

    private void IncreaseScore()
    {
        TweenX.TweenNumbers(scoreText, score, score + scoreBonus, scoreTweenTime, scoreEase, "F2");
        score = score + scoreBonus;
    }
}