using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpiner : MonoBehaviour
{

    [SerializeField] float rotationRatio = 3f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationRatio*Time.deltaTime);
    }
}
