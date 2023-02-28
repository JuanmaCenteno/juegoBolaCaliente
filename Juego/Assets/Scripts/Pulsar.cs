using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pulsar : MonoBehaviour
{
    private Button boton;
    public Image imagen;
    public Text texto;
    public Sprite[] spNumeros;

    private bool contar;
    private int numero;

    // Start is called before the first frame update
    void Start()
    {
        boton = GameObject.FindAnyObjectByType<Button>();
        boton.onClick.AddListener(Pulsado);
        contar = false;
        numero = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(contar){
            switch(numero){
                case 0: 
                    Debug.Log("Terminado - Salto a otra escena.");
                    SceneManager.LoadScene("Juego", LoadSceneMode.Single);
                    break;

                case 1:
                    imagen.sprite = spNumeros[2];
                    texto.text = "Uno";
                    break;

                case 2:
                    imagen.sprite = spNumeros[1];
                    texto.text = "Dos";
                    break;

                case 3:
                    imagen.sprite = spNumeros[0];
                    texto.text = "Tres";
                    break;
            }
            StartCoroutine(Esperar());
            contar = false;
            numero--;
        }
    }

    IEnumerator Esperar(){
        yield return new WaitForSeconds(1);
        contar = true;        
    }

    void Pulsado(){
        imagen.gameObject.SetActive(true);
        contar = true;
    }
}
