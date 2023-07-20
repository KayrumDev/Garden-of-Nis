using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] GameObject [] objects;
    [SerializeField] Dictionary<string, int> table = new Dictionary<string, int>();


    public void initialize()
    {
        table.Add("ground",0);
        table.Add("plant1",1);
    }


    public GameObject createObject (string name, float x, float y, float z){
        return createObject(name,x,y,z,0,0,0);
    }

    public GameObject createObject (string name, float x, float y, float z, float offsetX,float offsetZ,int rotation){
        GameObject obj = Instantiate(objects[table[name]], new Vector3(x+offsetX,y,z+offsetZ),Quaternion.Euler(0, rotation, 0),transform);
        obj.name = name + "_" + x + "_" + z;
        return obj;
    }
}
