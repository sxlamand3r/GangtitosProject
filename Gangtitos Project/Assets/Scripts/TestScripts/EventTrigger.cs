using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [Header("Types of Events")]
    public bool event1 = false; // Variable para controlar si el evento ya se activó
    public float chanceOfEvent1 = 0.5f;
    public GameObject objetoADestruir;
    [SerializeField] private AudioClip Foco;
    [Space(10)]
    public bool event2 = false;
    public float chanceOfEvent2 = 0.5f;
    public GameObject objetoActivar;
    public bool activarGravedad = true;
    [SerializeField] private AudioClip Cae;

    private bool seEjecuto = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!seEjecuto)
        {
            //Evento 1 Foco Explota
            if (other.CompareTag("Player") && event1)
            {
                float randomValue1 = Random.value;
                Debug.Log(randomValue1);
                if (randomValue1 <= chanceOfEvent1)
                {
                    SoundController.Instance.runSound(Foco);
                    Destroy(objetoADestruir); // Destruye el objeto especificado
                }
                Destroy(gameObject); // Destruye el collider con el que colisionó
            }

            //Evento 2 Objeto Cae
            if (other.CompareTag("Player") && event2)
            {
                float randomValue2 = Random.value;
                Rigidbody rb = objetoActivar.GetComponent<Rigidbody>();
                if (rb != null && randomValue2 <= chanceOfEvent2)
                {
                    rb.useGravity = activarGravedad;
                }
                Destroy(gameObject);
            }
            seEjecuto = true;
        }
    }
}