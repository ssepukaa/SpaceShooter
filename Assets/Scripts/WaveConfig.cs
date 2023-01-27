using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Enemy Wave Config")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private List<GameObject> pathPrefabs;
        //[SerializeField] private GameObject pathPrefab;
        [SerializeField] private float timeBetweenSpawns = 0.5f;
        [SerializeField] private float spawnRandomFactor = 0.3f;
        [SerializeField] private int numberOfEnemy = 5;
        [SerializeField] private float moveSpeed = 2f;

        public GameObject GetEnemyPrefab() => enemyPrefab;

        public List<Transform> GetWaypoints()
        {
            var waypointsList = new List<Transform>();
            foreach (Transform child in pathPrefabs[Random.Range(0,pathPrefabs.Count)].transform)
            {
                waypointsList.Add(child);
            }

            return waypointsList;
        }

        public float GetTimeBetweenSpawns() => timeBetweenSpawns;
        public float GetSpawnRandomFactor() => spawnRandomFactor;
        public int GetNumberOfEnemy() => numberOfEnemy;
        public float GetMoveSpeed() => moveSpeed;
    }
}