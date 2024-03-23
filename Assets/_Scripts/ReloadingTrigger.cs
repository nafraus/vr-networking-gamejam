using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadingTrigger : MonoBehaviour
{
    Vector3 eulerRotation;
    [SerializeField] private Transform followTransform;
    void Update()
    {
        eulerRotation = new Vector3(0, followTransform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(eulerRotation);
        transform.position = followTransform.position;
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
