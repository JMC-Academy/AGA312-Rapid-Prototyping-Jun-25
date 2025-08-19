using System;
using UnityEngine;

public class Playground : GameBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    [ReadOnly] public int speed = 5;
    public GameObject tester;

    public float fogIntensity;

    void Start()
    {
        ObjectX.ScaleObjectToZero(player);
        ExecuteAfterSeconds(1, () =>
        {
            SetupPlayer();
        });

        ExecuteAfterFrames(3, () => print("3 frames later..."));

        ///InitializeLevel();
    }


    //private Action InitializeLevel(Action _onComplete = null)
    //{
    //    print("starting the function");

    //ExecuteAfterSeconds(1, () =>
    //{
    //    _onComplete?.Invoke();
    //});
    //return _onComplete?.Invoke();

    //    void OnComplete(float _time, Action _onComplete = null)
    //{
    //    bool saveLoaded = false;
    //    ExecuteWhenTrue(() => saveLoaded, () => _onComplete?.Invoke());
    //}
    //}


    public float timeLimit = 120;
    private void Update()
    {
        timeLimit -= Time.deltaTime;
        fogIntensity = MathX.Map(timeLimit, 0, 120, 20, 600);
    }

    private void SetupPlayer()
    {
        player.GetComponent<Renderer>().material.color = ColorX.GetRandomColor();
        ObjectX.ScaleObjectToValue(player);
    }

    private void OnMove(Vector2 _moveVector)
    {
        player.transform.position += new Vector3(_moveVector.x, 0, _moveVector.y) * Time.deltaTime;
    }

    public void OnJump()
    {
        player.GetComponent<Renderer>().material.color = ColorX.GetRandomColor();
        GameEvents.ReportOnPlayerHit();
        GameEvents.ReportOnPlayerDied(player);
    }

    private void OnEnable()
    {
        InputManager.OnMove += OnMove;
        InputManager.OnJump += OnJump;
    }

    private void OnDisable()
    {
        InputManager.OnMove -= OnMove;
        InputManager.OnJump -= OnJump;
    }
}
