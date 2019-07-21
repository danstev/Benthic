using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    int seed;
    float rot = 0;
    public float multipler;
    // Start is called before the first frame update
    void Start()
    {
        multipler = Random.Range(-0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        rot += (1 * multipler);
        if(rot >=  360)
        {
            rot = 0;
        }
        
         transform.localRotation = Quaternion.Euler(0, rot, 0);
        
    }
}
