using UnityEngine;

namespace Flyweight
{
    [CreateAssetMenu(fileName = "NewEnemySpawnerData", menuName = "Data/EnemySpawnerData")]
    public class EnemySpawnerData : ScriptableObject
    {
        public int turnsBetweenSpawn;
        public GameObject[] enemyPrefabs;
        public float activationRadius;
    }
}
