using UnityEngine;

public class HealthController
{
    #region PRIVATE_PROPERTIES
    private float _maxLife;
    private float _currentLife;
    #endregion

    #region PUBLIC_PROPERTIES
    public float MaxLife => _maxLife;
    public float CurrentLife => _currentLife;
    public bool IsAlive() { return CurrentLife > 0; }
    #endregion

    #region FUNCTIONS
    public void Initialize(float maxLife)
    { 
        _maxLife = maxLife;
        _currentLife = MaxLife;
    }
    public void Kill()
    {
        _currentLife = 0;
    }
    #endregion
}
