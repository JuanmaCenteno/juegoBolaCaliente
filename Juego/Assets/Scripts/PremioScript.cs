using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremioScript : MonoBehaviour
{
    public ParticleSystem particulas;
    public AudioSource sonido;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime * 180);
    }

    void OnDestroy(){
        Instantiate<ParticleSystem>(particulas, this.transform.position, particulas.transform.rotation);
        AudioSource mysound = Instantiate<AudioSource>(sonido, this.transform.position, sonido.transform.rotation);
        mysound.Play();
    }
}
