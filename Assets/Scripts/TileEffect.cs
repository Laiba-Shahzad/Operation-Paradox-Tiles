using UnityEngine;

public class TileEffect : MonoBehaviour
{
    public enum EffectType
    {
        Teleport,
        GravityInvert,
        TimeWarp,
        SlowField,
        PhantomExit
    }

    public EffectType effectType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EffectManager.Instance.TriggerEffect(effectType, transform);
        }
    }
}