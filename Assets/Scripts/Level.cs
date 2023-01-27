using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Level : MonoBehaviour
    {

        public void LoadStartMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
            FindObjectOfType<GameSession>().Reset();
        }

        public void LoadGameOver()
        {
            StartCoroutine(DelayAfterDeath());

        }
        IEnumerator DelayAfterDeath()
        {

            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Game Over");
        }
        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
