using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    // Variables de ajuste del head bobbing
    public float bobbingSpeed = 0.1f; // Velocidad del movimiento de la cabeza
    public float bobbingAmount = 0.1f; // Cuánto se mueve la cabeza
    public float midpoint = 2.0f; // Altura media del movimiento de la cabeza

    // Variables de movimiento de la cabeza
    private float timer = 0.0f;
    private float midpointY = 0.0f;

    // Variables de control del movimiento
    private Vector3 lastPosition;

    void Start()
    {
        // Inicializa la última posición
        lastPosition = transform.position;
        midpointY = midpoint;
    }

    void Update()
    {
        // Calcula la posición actual del personaje
        Vector3 currentPosition = transform.position;

        // Calcula el movimiento del personaje
        Vector3 moveDelta = currentPosition - lastPosition;

        // Si el personaje se está moviendo, mueve la cabeza
        if (moveDelta.magnitude > 0)
        {
            float waveSlice = Mathf.Sin(timer);
            midpointY = midpoint + (waveSlice * bobbingAmount);
            timer += bobbingSpeed;

            // Limita el tiempo para evitar desbordamiento
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        else
        {
            midpointY = midpoint;
        }

        // Actualiza la posición de la cámara
        transform.localPosition = new Vector3(transform.localPosition.x, midpointY, transform.localPosition.z);

        // Guarda la última posición para el siguiente frame
        lastPosition = currentPosition;
    }
}
