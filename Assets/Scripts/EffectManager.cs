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
    public UIEffects uiEffects;
    public CameraShake cameraShake;
    public GlitchEffect glitchEffect;

    public Transform exitDoor;
    public Transform[] exitPositions;

    private bool exitRelocated = false;

    private bool gravityFlipped = false;


    private void Awake()
    {
        Instance = this;
        ResetState();
    }

    void ResetState()
    {
        exitRelocated = false;
        gravityFlipped = false;
        Physics.gravity = new Vector3(0, -9.81f, 0);

        if (cameraHolder != null)
            cameraHolder.localRotation = Quaternion.Euler(0, 0, 0);

        if (gameTimer != null)
            gameTimer.timeMultiplier = 1f;

        if (playerController != null)
            playerController.currentSpeed = playerController.speed; // ← use 'speed' not 'moveSpeed'
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

        cameraShake.Shake(2f, 0.3f); // duration, intensity
        uiEffects.ShowEffect("DISPLACED!", Color.white);
        glitchEffect.TriggerGlitch(0.2f);

        AudioManager.Instance.PlaySound(AudioManager.Instance.teleportSound);
    }

    IEnumerator GravityInvert()
    {
        if (gravityFlipped) yield break;

        gravityFlipped = true;

        // Flip gravity
        Physics.gravity = new Vector3(0, 9.81f, 0);

        // Flip camera
        cameraHolder.localRotation = Quaternion.Euler(0, 0, 180);

        uiEffects.ShowEffect("GRAVITY INVERTED!", Color.red);
        glitchEffect.TriggerGlitch(0.3f);
        AudioManager.Instance.PlaySound(AudioManager.Instance.gravitySound);

        yield return new WaitForSeconds(gravityfliptimer);

        // Restore gravity
        Physics.gravity = new Vector3(0, -9.81f, 0);

        // Reset camera
        cameraHolder.localRotation = Quaternion.Euler(0, 0, 0);

        gravityFlipped = false;
    }

    IEnumerator TimeWarp()
    {
        uiEffects.ShowEffect("TIME DISTORTED!", Color.yellow);
        AudioManager.Instance.PlaySound(AudioManager.Instance.timeWarpSound);
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
        uiEffects.ShowEffect("MOVEMENT DISRUPTED!", Color.cyan);
        AudioManager.Instance.PlaySound(AudioManager.Instance.slowSound);
        float originalSpeed = playerController.currentSpeed;

        playerController.currentSpeed *= 0.3f; // slow down

        yield return new WaitForSeconds(5f);

        playerController.currentSpeed = originalSpeed;
    }

    void TriggerPhantomExit()
    {
        if (!exitRelocated)
        {
            uiEffects.ShowEffect("SIGNAL LOST... EXIT RELOCATED", Color.magenta);
            AudioManager.Instance.PlaySound(AudioManager.Instance.glitchSound);
            glitchEffect.TriggerGlitch(0.5f);
            int index = Random.Range(0, exitPositions.Length);
            exitDoor.position = exitPositions[index].position;

            exitRelocated = true;
        }
        else
        {
            FindFirstObjectByType<UIScreenManager>().ShowWin();
            AudioManager.Instance.PlaySound(AudioManager.Instance.winSound);
            Debug.Log("Mission Complete!");
            //Time.timeScale = 0f;
        }
    }
}

