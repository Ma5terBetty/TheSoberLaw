using UnityEngine;

[CreateAssetMenu(fileName = "LevelEnemies",menuName = "Stats/EnemiesOnLevel",order = 2)]
public class EnemiesOnLevelData : ScriptableObject
{
    [SerializeField]
    public GameObject[] Enemies;
    [SerializeField]
    public GameObject[] Bosses;
}