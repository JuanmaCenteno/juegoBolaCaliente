using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimientoJugador : MonoBehaviour
{

    public Camera camara;
    public GameObject prefabSuelo;
    public int velocidad;
    private Vector3 offset;
    private float valX, valZ;

    public GameObject sombrero;

    // Start is called before the first frame update
    void Start()
    {
        offset = camara.transform.position;
        offset = sombrero.transform.position;
        valX = 0.0f;
        valZ = 0.0f;
        SueloInicial();
    }

    void SueloInicial(){
        for(int i = 0; i < 3; i++){
            valZ += 6.0f;
            GameObject nuevoSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sombrero.transform.position = this.transform.position + offset;
    }
}
