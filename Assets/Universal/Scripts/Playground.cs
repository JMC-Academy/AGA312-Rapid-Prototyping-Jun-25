using UnityEngine;

public class Playground : GameBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    [ReadOnly] public int speed = 5;
    public GameObject tester;

    void Start()
    {
        ObjectX.ScaleObjectToZero(player);
        ExecuteAfterSeconds(1, () =>
        {
            SetupPlayer();
        });

        ExecuteAfterFrames(3, () => print("3 frames later..."));
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
