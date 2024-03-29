using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyPathing : MonoBehaviour
    {
        [SerializeField] WaveConfig waveConfig;
        private List<Transform> waypoints;

        private int waypointIndex = 0;

        void Start()
        {

            waypoints = waveConfig.GetWaypoints();

            transform.position = waypoints[waypointIndex].transform.position;

        }


        void Update()
        {
            MoveEnemy();

        }

        public void SetWaveConfig(WaveConfig waveConfig)
        {
            this.waveConfig = waveConfig;
        }

        private void MoveEnemy()
        {
            if (waypointIndex <= waypoints.Count - 1)
            {
                var targetPosition = waypoints[waypointIndex].transform.position;
                var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector2.MoveTowards(
                    transform.position, targetPosition, movementThisFrame);
                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
