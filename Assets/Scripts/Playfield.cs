using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playfield : MonoBehaviour
{

    
    public int size = 3;
    public Tile [,] garden;

    Dictionary<string, GameObject> plants = new Dictionary<string, GameObject>();

    [SerializeField] ObjectManager manager;



    // Start is called before the first frame update
    void Start()
    {
        garden = new Tile[size,size];

        for (int x = 0; x < size; x++){
            for (int z = 0; z < size; z++){
                garden[x,z] = new Tile(x,z);
                manager.createObject("ground",x,0,z);
               
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
