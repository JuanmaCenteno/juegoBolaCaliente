using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class movimientoJugador : MonoBehaviour
{
    Rigidbody rb;
    public Camera camara;
    public GameObject Vacio;
    public int velocidad;
    public float jumpspeed;
    private Vector3 offset_camara;
    private Vector3 offset_sombrero;
    private float valX, valZ;
    private int cont;

    private bool isGrounded = true;

    private Random aleatorio;
    private float tiempo;
    private float ventanaTiempo;
    private float tSiguienteSuelo;
    public GameObject[] prefabsSuelos;
    private int nSuelos;
    private GameObject[] suelos;
    private int indiceSuelos;
    private float orientacion;

    public GameObject sombrero;

    public Text texto;

    public AudioSource musica;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset_camara = camara.transform.position;
        offset_sombrero = sombrero.transform.position;
        valZ = 0.0f;
        valX = -6.0f;
        aleatorio = new Random();
        tiempo = 0;
        ventanaTiempo = 6/velocidad;
        tSiguienteSuelo = ventanaTiempo;
        nSuelos = 4;
        suelos = new GameObject[nSuelos];
        indiceSuelos = 0;
        orientacion = 90.0f;
        musica.Play();
        SueloInicial();
    }

    void SueloInicial(){
        for(int i = 0; i<nSuelos; i++){
            suelos[i] = Instantiate(prefabsSuelos[0], new Vector3(valX, 0.0f, valZ), Quaternion.identity) as GameObject;
            valX += 6.0f;
        }
    }

    void crearSuelo(){
        int numAleatorio = aleatorio.Next(prefabsSuelos.Length);
        if(numAleatorio == 0){
            if(orientacion == 90.0f) orientacion = 0.0f;
            else orientacion = 90.0f; 
        }
        suelos[indiceSuelos] = Instantiate(prefabsSuelos[numAleatorio], new Vector3(valX, 0.0f, valZ), Quaternion.Euler(0f, orientacion+90.0f, 0f)) as GameObject;
        indiceSuelos = (indiceSuelos+1)%nSuelos;
        if(orientacion == 0.0f) valZ += 6.0f;
        else valX += 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position += new Vector3(0.0f, 0.0f,0.025f);
        
        float Hmove = Input.GetAxis("Horizontal");
        float Vmove = Input.GetAxis("Vertical");
        camara.transform.position = this.transform.position + offset_camara;
        Vacio.transform.position = new Vector3(this.transform.position.x, -3.0f, this.transform.position.z);
        //sombrero.transform.position = this.transform.position + offset_sombrero;



        Vector3 ballmove = new Vector3 (Hmove, 0.0f, Vmove);
        rb.AddForce(ballmove * velocidad/2);

        //SALTO
        if(isGrounded && Input.GetKey(KeyCode.Space)){
            Vector3 balljump = new Vector3(0.0f, 6.0f, 0.0f);
            rb.AddForce(balljump * jumpspeed);
        }

        //GENERACION DE SUELOS POR TIEMPO
        tiempo += Time.deltaTime;
        if(tiempo > tSiguienteSuelo){
            tSiguienteSuelo += ventanaTiempo;
            suelos[indiceSuelos].SetActive(false);
            crearSuelo();
        }

        //RESTART
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);     //Reiniciar si se pulsa la R, annadir reiniciar tiempo y puntuacion
        }
    }

    void OnTriggerEnter(Collider Other){
        if(Other.CompareTag("Premio")){
            Debug.Log("Premio!");
            cont++;
            texto.text = "Puntuacion: " + cont;
            Destroy(Other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag ==  "Suelo"){
            isGrounded = true;
        }
        if(other.gameObject.tag == "Vacio" || other.gameObject.tag == "Muerte"){
            Debug.Log("A tomar por culo");
            // DECIR QUE SE HA PERDIDO, MOMENTO GUITARTE
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag ==  "Suelo"){
            isGrounded = false;
        }
    }
}