using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject prefDeathVFX;
    [SerializeField] private AudioClip prefDeathSFX;

    public void InstantiateDeathVFXandSFX()
    {
        Instantiate(prefDeathVFX, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(prefDeathSFX, transform.position);
    }

}
