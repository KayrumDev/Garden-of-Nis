using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] GameObject [] objects;
    [SerializeField] Dictionary<string, int> table = new Dictionary<string, int>();

    void Start()
    {
        table.Add("ground",0);
    }
    
    public GameObject createObject (string name, int x, int y, int z){
        GameObject obj = Instantiate(objects[table[name]], new Vector3(x,y,z),Quaternion.Euler(0, 0, 0),transform);
        obj.name = name + "_" + x + "_" + z;
        return obj;
    }
}
