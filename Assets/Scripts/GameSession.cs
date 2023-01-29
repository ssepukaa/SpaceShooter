using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private GameObject prefEnemySpawner;
        private int score = 0;
        
        private int scoreTemp = 2;
        private int numberWaves = 0;
        private int recordScore;
        private int amountHealth = 100;
        private bool isMute;
        private int levelCount = 1;


        private void  Awake()
        {
            SetUpSingleton();
            recordScore = PlayerPrefs.GetInt("RecordScore");
            Application.targetFrameRate = 50;
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

            if (scoreTemp - (score / 1000) == 1)
            {
                scoreTemp += 1;
                Player player = FindObjectOfType<Player>();
                if (player != null)
                {
                    player.AddHealth(amountHealth);
                }
            }
       
        }

        public void Reset()
        {
            Destroy(gameObject);
        }

        public void AddNumberWaves()
        {
            numberWaves++;
            if (numberWaves % 10 == 0)
            {
                CreateNewEnemySpawner();
            }
        
        }

        private void CreateNewEnemySpawner()
        {
            
            Instantiate(prefEnemySpawner);
            SetLevelCount();

            var directMovingObjects = FindObjectsOfType<DirectMoving>();
            foreach (DirectMoving directMovingObject in directMovingObjects)
            {
                directMovingObject.speed -= 0.1f;
            }

            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.SubProjectileFiringPeriod();
            }
               
        }

        public int GetLevelCount()
        {
            return levelCount;
        }

        private void SetLevelCount()
        {
            levelCount++;
        }
    }
}
