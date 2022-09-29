using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
    }
}
