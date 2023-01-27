
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner: MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> waveConfigs;

        [SerializeField] private int startingWave=0;
        [SerializeField] private bool loopingWaves = false;

        private GameSession gameSession;

        IEnumerator Start()
        {
            gameSession = FindObjectOfType<GameSession>();
            do
            {
                yield return StartCoroutine(SpawnAllWaves());
            }
            while (loopingWaves);
        }

        private IEnumerator SpawnAllWaves()
        {
            for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
            {
                var currentWay = waveConfigs[waveIndex];
                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWay));
                gameSession.AddNumberWaves();
            }

            
        }
        private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
        {
            for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemy(); enemyCount++)
            {

                var newEnemy = Instantiate(
                    waveConfig.GetEnemyPrefab(),
                    waveConfig.GetWaypoints()[0].transform.position,
                    Quaternion.identity);

                newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
            }
        }
    }
}
