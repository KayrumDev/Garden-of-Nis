using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(new Vector3(0,Random.Range(0,360),0),Space.Self);
        this.transform.position = new Vector3 (transform.position.x + Random.Range(-5,5)/70f,transform.position.y,transform.position.z + Random.Range(-5,5)/70f);
    }

}
