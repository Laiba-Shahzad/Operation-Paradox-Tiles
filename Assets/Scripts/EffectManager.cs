using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    public Transform cameraHolder;
    public Transform player;
    public Transform[] teleportPoints;
    public float gravityfliptimer;
    public GameTimer gameTimer;
    public float goodtimer, badtimer;
    public PlayerController playerController;

    public Transform exitDoor;
    public Transform[] exitPositions;

    private bool exitRelocated = false;

    private bool gravityFlipped = false;


    private void Awake()
    {
        Instance = this;
    }

    public void TriggerEffect(TileEffect.EffectType type, Transform tile)
    {
        if (type == TileEffect.EffectType.Teleport)
        {
            TeleportPlayer();
        }
        else if (type == TileEffect.EffectType.GravityInvert)
        {
            StartCoroutine(GravityInvert());
        }
        else if (type == TileEffect.EffectType.TimeWarp)
        {
            StartCoroutine(TimeWarp());
        }
        else if (type == TileEffect.EffectType.SlowField)
        {
            StartCoroutine(SlowField());
        }
        else if (type == TileEffect.EffectType.PhantomExit)
        {
            TriggerPhantomExit();
        }
    }

    void TeleportPlayer()
    {
        int index = Random.Range(0, teleportPoints.Length);
        player.position = teleportPoints[index].position;
    }

    IEnumerator GravityInvert()
    {
        if (gravityFlipped) yield break;

        gravityFlipped = true;

        // Flip gravity
        Physics.gravity = new Vector3(0, 9.81f, 0);

        // Flip camera
        cameraHolder.localRotation = Quaternion.Euler(0, 0, 180);

        Debug.Log("Gravity Inverted!");

        yield return new WaitForSeconds(gravityfliptimer);

        // Restore gravity
        Physics.gravity = new Vector3(0, -9.81f, 0);

        // Reset camera
        cameraHolder.localRotation = Quaternion.Euler(0, 0, 0);

        gravityFlipped = false;
    }

    IEnumerator TimeWarp()
    {
        Debug.Log("Time Warping!");

        // Random: speed up OR slow down
        float random = Random.value;

        if (random > 0.5f)
            gameTimer.timeMultiplier = badtimer;  // faster timer (bad)
        else
            gameTimer.timeMultiplier = goodtimer; // slower timer (good)

        yield return new WaitForSeconds(5f);

        gameTimer.timeMultiplier = 1f;
    }

    IEnumerator SlowField()
    {
        Debug.Log("Movement Disrupted!");

        float originalSpeed = playerController.currentSpeed;

        playerController.currentSpeed *= 0.3f; // slow down

        yield return new WaitForSeconds(5f);

        playerController.currentSpeed = originalSpeed;
    }

    void TriggerPhantomExit()
    {
        if (!exitRelocated)
        {
            Debug.Log("Signal Lost... Relocating Exit");

            int index = Random.Range(0, exitPositions.Length);
            exitDoor.position = exitPositions[index].position;

            exitRelocated = true;
        }
        else
        {
            Debug.Log("Mission Complete!");
            Time.timeScale = 0f;
        }
    }
}

