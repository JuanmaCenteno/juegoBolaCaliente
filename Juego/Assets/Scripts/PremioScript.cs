using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremioScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime * 180);
    }

    void OnTriggerEnter(Collider Other){
        Debug.Log("Premio!");
    }
}
