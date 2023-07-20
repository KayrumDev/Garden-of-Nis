using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playfield : MonoBehaviour
{

    
    public int size = 3;
    public Plant [,] garden;

    Dictionary<string, GameObject> plantObjects = new Dictionary<string, GameObject>();

    [SerializeField] ObjectManager manager;
    [SerializeField] GameObject selector;

    public void simulationStart(){
        print("Start");

        for (int x = 0; x < size; x++){
            for (int z = 0; z < size; z++){
                if (garden[x,z].getType()!=0){
                   garden[x,z].setAge(garden[x,z].getAge()+1);   
                }
            }
        }
        wither();
        
    }

    private void wither(){
        for (int x = 0; x < size; x++){
            for (int z = 0; z < size; z++){
                if (garden[x,z].getType()!=0 && garden[x,z].getStage()==2){
                    destroy(x,z);
                }
            }
        }
    }

    private void destroy(int x, int z){
        GameObject.Destroy(plantObjects["plant_"+x+"_"+z]);
        plantObjects.Remove("plant_"+x+"_"+z);
        garden[x,z].setType(0);
        garden[x,z].setStage(0);
    }

    private void plant(int type,int x, int z){
        if (garden[x,z].getType()== 0){
            int rotation = Random.Range(0,360);
            float offsetX = Random.Range(-5,5)/70f;
            float offsetZ = Random.Range(-5,5)/70f;
            garden[x,z].plantType(type,rotation,offsetX,offsetZ);
            plantObjects.Add("plant_"+x+"_"+z,manager.createObject("plant1",x+0.5f,0,z+0.5f,offsetX,offsetZ,rotation));
           
        }
    }

    public List<Plant> getNeighbours(int x, int z){
        List<Plant> res = new List<Plant>();
        if((x-1)>=0){
            res.Add(garden[x-1,z]);
        }
        if((z-1)>=0){
            res.Add(garden[x,z-1]);
        }
        if((x+1)<size){
            res.Add(garden[x+1,z]);
        }
        if((z+1)<size){
            res.Add(garden[x,z+1]);
        }

        return res;
    }

    private Plane plane = new Plane(Vector3.down,0);
    // Start is called before the first frame update
    void Start()
    {
        garden = new Plant[size,size];
        manager.initialize();
        for (int x = 0; x < size; x++){
            for (int z = 0; z < size; z++){
                garden[x,z] = new Plant(x,z);
                manager.createObject("ground",x,0,z);
               
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = new Vector3(0,0,0);
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
        if (plane.Raycast(ray, out float distance)) 
        {
            worldPosition = ray.GetPoint(distance);
        }
        worldPosition = new Vector3(Mathf.Floor(worldPosition.x),Mathf.Floor(worldPosition.y),Mathf.Floor(worldPosition.z));
        int posX = (int)worldPosition.x;
        int posZ = (int)worldPosition.z;
        if (posX >= 0 && posX < size && posZ >= 0 && posZ < size){
            selector.SetActive(true);
            selector.transform.position = new Vector3(posX,selector.transform.position.y,posZ);
            Plant selectedPlant = garden[posX,posZ];
            if ( Input.GetMouseButtonDown(0)){
                int type = selectedPlant.getType();
                if (type == 0){
                    plant(1,posX,posZ);
                } else {
                    destroy(posX,posZ);
                }
            }
        } else {
            selector.SetActive(false) ;
        }
    }
}
