using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimientoJugador : MonoBehaviour
{
    Rigidbody rb;
    public Camera camara;
    public GameObject prefabSuelo;
    public int velocidad;
    public int jumpspeed;
    private Vector3 offset;
    private float valX, valZ;

    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = camara.transform.position;
        valX = 0.0f;
        valZ = 0.0f;
        SueloInicial();
    }

    void SueloInicial(){
        for(int i = 0; i < 30; i++){
            valZ += 6.0f;
            GameObject nuevoSuelo = Instantiate(prefabSuelo, new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0.0f, 0.0f,0.025f);
        
        float Hmove = Input.GetAxis("Horizontal");
        float Vmove = Input.GetAxis("Vertical");

        camara.transform.position = this.transform.position + offset;

        Vector3 ballmove = new Vector3 (Hmove, 0.0f, Vmove);
        rb.AddForce(ballmove * velocidad);

        if(isGrounded && Input.GetKey(KeyCode.Space)){
            Vector3 balljump = new Vector3(0.0f, 6.0f, 0.0f);
            rb.AddForce(balljump * jumpspeed);
        }
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag ==  "Suelo"){
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag ==  "Suelo"){
            isGrounded = false;
        }
    }
}
