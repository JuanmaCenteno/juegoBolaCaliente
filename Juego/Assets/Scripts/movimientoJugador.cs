using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimientoJugador : MonoBehaviour
{

    public Camera camara;
    public GameObject prefabSuelo;
    public int velocidad;
    public Text texto;
    private Vector3 offset;
    private float valX, valZ;
    private Rigidbody rb;
    private int cont;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        offset = camara.transform.position;
        valX = 0.0f;
        valZ = 0.0f;
        cont = 0;
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
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movHorizontal, 0.0f, movVertical);
        rb.AddForce(movimiento * velocidad);
    }

    void OnTriggerEnter(Collider Other){
        if(Other.CompareTag("Premio")){
            Debug.Log("Premio!");
            cont++;
            texto.text = "Puntuacion: " + cont;
            Destroy(Other.gameObject);
        }
    }
}
