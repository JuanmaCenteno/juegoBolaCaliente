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

    private bool isGrounded;
    private int n_grounded;

    private Random aleatorio;
    private float tiempo;
    private float ventanaTiempo;
    private float tSiguienteSuelo;
    public GameObject[] prefabsSuelos;
    private int nSuelos;
    private GameObject[] suelos;
    private int indiceSuelos;
    private float orientacion;
    private int nMon;
    private int ul_mon;


    public GameObject sombrero;
    public Text texto;

    public AudioSource musica;
    public AudioSource pickup;
    public AudioSource salto;


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
        nMon = 0;
        ul_mon = 0;
        isGrounded = false;
        n_grounded = 0;
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
        if(numAleatorio == prefabsSuelos.Length - 1 || numAleatorio == 0 || ul_mon >=5){
            if(orientacion == 90.0f) orientacion = 0.0f;
            else orientacion = 90.0f;
            if(ul_mon >=5){
                numAleatorio = numAleatorio%2;
                if(numAleatorio != 0){
                    numAleatorio = 15;
                }
            }
            ul_mon = 0;
        }
        else{ul_mon++;}

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
        rb.AddForce(ballmove * velocidad);

        //SALTO
        if(isGrounded && Input.GetKey(KeyCode.Space)){
            salto.Play();
            Vector3 balljump = new Vector3(0.0f, 6.0f, 0.0f);
            rb.AddForce(balljump * jumpspeed);
        }

        //GENERACION DE SUELOS POR TIEMPO
        tiempo += Time.deltaTime;
        if(tiempo > tSiguienteSuelo){
            tSiguienteSuelo += ventanaTiempo;
            Destroy(suelos[indiceSuelos]);
            crearSuelo();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Premio"){
            nMon++;
            Destroy(other.gameObject);

            AudioSource sonido = Instantiate<AudioSource>(pickup, this.transform.position, pickup.transform.rotation);
            sonido.Play();

            texto.text = "Monedas: " +  nMon +" / 3";

            if(nMon == 3){
                SceneManager.LoadScene("Win", LoadSceneMode.Single);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Suelo"){
            isGrounded = true;
            n_grounded++;
        }
        if(other.gameObject.tag == "Vacio"){
            SceneManager.LoadScene("Derrota_agua", LoadSceneMode.Single);
        }

        if(other.gameObject.tag == "Muerte"){
            SceneManager.LoadScene("Derrota_pincho", LoadSceneMode.Single);
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Suelo"){
            n_grounded--;
            if(n_grounded == 0) isGrounded = false;
        }
    }
}