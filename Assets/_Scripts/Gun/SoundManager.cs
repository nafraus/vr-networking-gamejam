using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Transform playerLocation;
    public void SpawnSound(GameObject obj)
    {
        Instantiate(obj, playerLocation.position, Quaternion.identity);
    }
}
