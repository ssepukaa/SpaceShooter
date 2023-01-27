using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private GameObject prefEnemySpawner;
        private int score = 0;
        private int numberWaves = 0;
        private int recordScore;


        private void  Awake()
        {
            SetUpSingleton();
            recordScore = PlayerPrefs.GetInt("RecordScore");
        }

        private void SetUpSingleton()
        {
            int numberGameSession = FindObjectsOfType<GameSession>().Length;
            if (numberGameSession > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public int GetScore()
        {
            return score;
        }

        public int GetRecordScore()
        {
            return recordScore;
        }

        public void AddToScore(int valueScore)
        {
            score += valueScore;
            if (score > recordScore)
            {
                recordScore = score;
                PlayerPrefs.SetInt("RecordScore", recordScore);
                PlayerPrefs.Save();
            }
       
        }

        public void Reset()
        {
            Destroy(gameObject);
        }

        public void AddNumberWaves()
        {
            numberWaves++;
            if (numberWaves % 1 == 0)
            {
                CreateNewEnemySpawner();
            }
        
        }

        private void CreateNewEnemySpawner()
        {
            
            Instantiate(prefEnemySpawner);

            var directMovingObjects = FindObjectsOfType<DirectMoving>();
            foreach (DirectMoving directMovingObject in directMovingObjects)
            {
                directMovingObject.speed -= 0.1f;
            }

            FindObjectOfType<Player>().SubProjectileFiringPeriod();
        }
    }
}
