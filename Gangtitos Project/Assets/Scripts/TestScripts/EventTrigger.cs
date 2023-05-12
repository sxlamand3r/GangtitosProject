using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [Header("Probability")]
    public float chanceOfEvent = 0.5f; // Probabilidad de que ocurra el evento, puedes ajustarla según tus necesidades

    [Space(20)]
    [Header("Types of Events")]
    public bool event1 = false; // Variable para controlar si el evento ya se activó
    public GameObject objetoADestruir;
    [SerializeField] private AudioClip Foco;
    [Space(10)]
    public bool event2 = false;
    public GameObject objetoActivar;
    public bool activarGravedad = true;
    [SerializeField] private AudioClip Cae;



    private void OnTriggerEnter(Collider other)
    {
        //Evento 1 Foco Explota
        if (other.CompareTag("Player") && event1)
        {
            float randomValue = Random.value;
            if (randomValue <= chanceOfEvent)
            {
                SoundController.Instance.runSound(Foco); 
                Destroy(objetoADestruir); // Destruye el objeto especificado
            }
            Destroy(gameObject); // Destruye el collider con el que colisionó
        }

        //Evento 2 Objeto Cae
        if (other.CompareTag("Player") && event2)
        {
            float randomValue = Random.value;
            Rigidbody rb = objetoActivar.GetComponent<Rigidbody>();
            if (rb != null && randomValue <= chanceOfEvent)
            {
                rb.useGravity = activarGravedad;
                /*if (other.CompareTag("Ground"))
                {
                    SoundController.Instance.runSound(Cae);
                }*/
            }
            Destroy(gameObject);
        }
    }
}