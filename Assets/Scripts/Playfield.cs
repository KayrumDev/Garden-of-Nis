using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class Playfield : MonoBehaviour
{

    
    public int size = 3;
    public int [,] garden  ;

    [SerializeField] GameObject groundtile;

    // Start is called before the first frame update
    void Start()
    {
        garden = new int[size,size];

        for (int x = 0; x < size; x++){
            for (int y = 0; y < size; y++){
                Instantiate(groundtile, new Vector3(x,0,y),Quaternion.Euler(0, 0, 0));
            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
