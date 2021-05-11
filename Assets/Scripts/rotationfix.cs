using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationfix : MonoBehaviour
{
    public GameObject model;
    public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        model.transform.Rotate(rotation.x,rotation.y,rotation.z,Space.Self);   
    }
}
