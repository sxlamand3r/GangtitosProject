using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonidoContacto : MonoBehaviour
{
    public AudioClip sonido;
    public float volumen = 1f;

    private AudioSource audioSource;

    private void Start()
    {
        // Obtener o agregar un componente AudioSource al objeto
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Asignar el clip de sonido al AudioSource
        audioSource.clip = sonido;
        audioSource.volume = volumen;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reproducir el sonido
            audioSource.Play();
        }
    }
}
