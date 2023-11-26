using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    #region PRIVATE_PROPERTIES
    [SerializeField] protected CharacterStats _characterStats;
    protected bool _isPlayer;
    #endregion

    #region PUBLIC_PROPERTIES
    public int MaxLife => _characterStats.MaxLife;
    public float MovementSpeed => _characterStats.MovementSpeed;
    public bool IsPlayer => _isPlayer;
    #endregion
}
