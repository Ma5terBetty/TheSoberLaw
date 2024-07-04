using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "Difficulty/DifficultyLevel", order = 0)]
public class DifficultySO : ScriptableObject
{
    [Range(0.01f, 1)]
    public float BulletSpeed;
    [Range(0.01f, 1)]
    public float EnemiesMovement;
    [Range(0.01f, 1)]
    public float EnemiesMaxLife;
    [Range(0.01f, 1)]
    public float PlayerSpeed;
    [Range(0.01f, 1)]
    public float PlayerMaxHealth;
}
