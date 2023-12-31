using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant 
{
    private Vector2 position;
    private int type = 0;
    private int growthStage = 0;
    private int age = 0;
    private int saplingMax = 1;
    private int bloomMax = 3;
    private int harvestMax = 5;

    private int rotation = 0;
    private float offsetX = 0;
    private float offsetZ = 0;

    private string saplingObj = "";
    private string bloomObj = "";
    private string harvestObj = "";

    private string activeObject = "";

     public Plant(int x, int z)
    {
        type = 0;
        position = new Vector2(x,z);
    }

    public int getType(){
        return type;
    }

    public int getStage(){
        return growthStage;
    }

     public void setStage(int s){
        growthStage = s;
    }

    public int getAge(){
        return age;
    }

     public void setAge(int s){
        age = s;
        if(age >= harvestMax){
            setStage(3);
            activeObject = harvestObj;
        } else if(age >= bloomMax){
            setStage(2);
            activeObject = harvestObj;
        } else if(age >= saplingMax){
            setStage(1);
            activeObject = bloomObj;
        } else {
            setStage(0);
            activeObject = saplingObj;
        }
    }

    public Vector2 getPosition(){
        return position;
    }
    public Vector2 getOffsets(){
        return new Vector2(offsetX,offsetZ);
    }
    public int getRotation(){
        return rotation;
    }


    public string getActiveObject(){
        return activeObject;
    }
    public void setType(int t){
        type = t;
    }

    public void plantType(int t,int rot, float offx, float offz){
        rotation = rot;
        offsetX = offx;
        offsetZ = offz;
        switch (t)
        {
            case 1: 
                type = 1;
                age = 0;
                growthStage = 0;
                saplingMax = 1;
                bloomMax = 3;
                harvestMax = 4;
                saplingObj = "plant1";
                bloomObj = "plant2";
                harvestObj = "plant3";
                activeObject = "plant1";
            break;
            default:
            break;
        }
    }

    public void setPosition(Vector2 pos){
        position = pos;
    }

}
