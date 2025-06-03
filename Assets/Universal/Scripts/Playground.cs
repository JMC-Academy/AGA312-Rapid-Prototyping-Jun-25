using UnityEngine;

public class Playground : GameBehaviour
{
    public GameObject player;

    void Start()
    {
        ObjectX.ScaleObjectToZero(player);
        ExecuteAfterSeconds(1, () =>
        {
            SetupPlayer();
        });

        ExecuteAfterFrames(3, () => print("3 frames later..."));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            player.GetComponent<Renderer>().material.color = ColorX.GetRandomColour();
    }

    private void SetupPlayer()
    {
        player.GetComponent<Renderer>().material.color = ColorX.GetRandomColour();
        ObjectX.ScaleObjectToValue(player);
    }
}
