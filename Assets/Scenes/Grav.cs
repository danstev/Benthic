using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grav : MonoBehaviour
{
    public Vector3 g = new Vector3(0,-9,0);
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = g;
    }
}
