using UnityEngine;

[CreateAssetMenu(fileName ="CharacterStats", menuName ="Stats/Character", order =0)]
public class CharacterStats : ScriptableObject
{
    [SerializeField] private CharacterStatValues _stats;

    public int MaxLife => _stats.MaxLife;
    public float MovementSpeed => _stats.MovementSpeed;
}

[System.Serializable]
public struct CharacterStatValues
{
    public int MaxLife;
    public float MovementSpeed;
}
