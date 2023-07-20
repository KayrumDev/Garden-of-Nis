using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    private Vector2 position;

    private int type = 0;


     public Tile(int x, int z)
    {
        type = 0;
        position = new Vector2(x,z);
    }

    public int getType(){
        return type;
    }

    public Vector2 getPosition(){
        return position;
    }

    public void setType(int t){
        type = t;
    }

    public void setPosition(Vector2 pos){
        position = pos;
    }

}
