using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Test : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Artcoroutine());
        }

        IEnumerator Artcoroutine()
        {
            print("Hello!");
            yield return new WaitForSeconds(3f);
            print("Good bye!");
        }
    }
}