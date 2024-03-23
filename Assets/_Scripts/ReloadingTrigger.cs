using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadingTrigger : MonoBehaviour
{
    Vector3 eulerRotation;
    void Update()
    {
        eulerRotation = new Vector3(0, transform.rotation.eulerAngles.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Gun gun = other.GetComponent<Gun>();
        if (gun)
        {
            Debug.Log("Reloading...");
            gun.Reload();
        }
    }
}
