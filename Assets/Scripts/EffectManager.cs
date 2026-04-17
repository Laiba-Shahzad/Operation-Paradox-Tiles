using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public Transform player;
    public Transform[] teleportPoints;

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
    }

    void TeleportPlayer()
    {
        int index = Random.Range(0, teleportPoints.Length);
        player.position = teleportPoints[index].position;
    }
}