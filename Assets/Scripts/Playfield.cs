using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playfield : MonoBehaviour
{

    
    public int size = 5;
    public Plant [,] garden;

    private int numberOfTypes = 3;
    Dictionary<string, GameObject> plantObjects = new Dictionary<string, GameObject>();

    [SerializeField] ObjectManager manager;
    [SerializeField] GameObject selector;

    public void simulationStart(){
        print("Start");
        List<Plant> [] typeLists = new List<Plant>[numberOfTypes+1];
        for (int t = 0; t <= numberOfTypes; t++){
            typeLists[t] = new List<Plant>() ;
        }
        //aging
        for (int x = 0; x < size; x++){
            for (int z = 0; z < size; z++){
                if (garden[x,z].getType()!=0){
                    typeLists[garden[x,z].getType()].Add(garden[x,z]);

                    string oldObj = garden[x,z].getActiveObject();
                    garden[x,z].setAge(garden[x,z].getAge()+1);   
                    //visual change of aging
                    if (oldObj != garden[x,z].getActiveObject()){
                        GameObject.Destroy(plantObjects["plant_"+x+"_"+z]);
                        plantObjects.Remove("plant_"+x+"_"+z);
                        Vector2 offset = garden[x,z].getOffsets();
                        plantObjects.Add("plant_"+x+"_"+z,manager.createObject(garden[x,z].getActiveObject(),x+0.5f,0,z+0.5f,offset.x,offset.y,garden[x,z].getRotation()));
                    }
                   
                }
            }
        }
        print("wither");
        wither();
        for (int t = 0; t <= numberOfTypes; t++){
            typeLists[t].Sort(compareAge);
            typeLists[t].ForEach((x)=> print(x.getAge()));
            typeLists[t].ForEach((x)=> {
                Vector2 pos = x.getPosition();
                List<Plant> neigbours = getNeighbours((int)pos.x,(int)pos.y).FindAll((n)=> n.getType()!= 0);
                if (x.getStage()>0 && neigbours.Count>2){
                    x.setAge(3);
                }
            });
        }
         //aging
        for (int x = 0; x < size; x++){
            for (int z = 0; z < size; z++){
                if (garden[x,z].getType()!=0){
                    //visual change of aging
                    GameObject.Destroy(plantObjects["plant_"+x+"_"+z]);
                    plantObjects.Remove("plant_"+x+"_"+z);
                    Vector2 offset = garden[x,z].getOffsets();
                    plantObjects.Add("plant_"+x+"_"+z,manager.createObject(garden[x,z].getActiveObject(),x+0.5f,0,z+0.5f,offset.x,offset.y,garden[x,z].getRotation()));
                }
            }
        }
        
    }

    private void wither(){
        for (int x = 0; x < size; x++){
            for (int z = 0; z < size; z++){
                if (garden[x,z].getType()!=0 && garden[x,z].getStage()==3){
                    destroy(x,z);
                }
            }
        }
    }

    private int compareAge(Plant p1, Plant p2){
        return (p2.getAge() - p1.getAge());
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
            plantObjects.Add("plant_"+x+"_"+z,manager.createObject(garden[x,z].getActiveObject(),x+0.5f,0,z+0.5f,offsetX,offsetZ,rotation));
           
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
