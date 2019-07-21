using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemGenerator : MonoBehaviour
{

    public GameObject SunObject;
    public GameObject PlanetObject;
    public GameObject Moon;
    public GameObject Asteroid;

    public int planet = 20;
    private int bounds = 2000;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(LevelManager.Instance.seed);
        

        //spawn sun
        int SunSize = Random.Range(15,20);
        GameObject s = Instantiate(SunObject, new Vector3(bounds/2,-130,bounds/2), Quaternion.identity);
        
        s.transform.localScale = new Vector3(SunSize,SunSize,SunSize);
        s.name = "Sun";

        for(int i = 0; i < planet; i++)
        { 
            GeneratePlanet(s);
        }

        
    }

    void GeneratePlanet(GameObject Sun)
    {
        Vector3 position = new Vector3();
        GameObject p = Instantiate(PlanetObject, new Vector3(0,0,0), Quaternion.identity);
        //p.transform.parent = Sun.transform;
        p.transform.position = new Vector3(Random.Range(0,bounds), Random.Range(-5,5),Random.Range(0,bounds));
        for(int m = 0; m < Random.Range(0,7); m++)
            GenerateMoon(p);

        p.transform.parent = Sun.transform;
    }

    void GenerateMoon(GameObject Planet)
    {
        Vector3 position = Planet.transform.position;
        GameObject p = Instantiate(Moon, position, Quaternion.identity);
        //p.transform.parent = Sun.transform;
        p.transform.position += new Vector3(Random.Range(-50,50), Random.Range(-50,100),Random.Range(-50,50));
        p.transform.parent = Planet.transform;
    }



}
