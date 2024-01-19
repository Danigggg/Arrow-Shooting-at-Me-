using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class Cooldown
{
    #region Variables

    [SerializeField] private float cooldownTime;
    private float _nextFireTime;

    #endregion

    public bool IsCoolingDown => Time.unscaledTime < _nextFireTime;
    public void StartCooldown() => _nextFireTime = Time.unscaledTime + cooldownTime;

    public float CooldownCurrentTime()
    {
        return _nextFireTime - Time.unscaledTime;
    }
}
