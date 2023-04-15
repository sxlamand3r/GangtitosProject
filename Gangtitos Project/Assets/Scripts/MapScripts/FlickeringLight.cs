using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light lightSource;
    public bool isFlashing = false;
    public bool randomFlash = false;
    public float minFlashSpeed = 0.1f;
    public float maxFlashSpeed = 1.0f;
    public bool initialState = true;

    private float timePassed = 0.0f;
    private bool isOn = true;
    private float flashSpeed = 1.0f;

    void Start()
    {
        // Configuramos el estado inicial de la luz
        SetInitialState(initialState);
    }

    void Update()
    {
        if (isFlashing)
        {
            timePassed += Time.deltaTime;
            if (timePassed > flashSpeed)
            {
                isOn = !isOn;
                lightSource.enabled = isOn;
                timePassed = 0.0f;

                if (randomFlash)
                {
                    flashSpeed = Random.Range(minFlashSpeed, maxFlashSpeed);
                }
            }
        }
    }

    public void SetFlashing(bool flashing)
    {
        isFlashing = flashing;
        lightSource.enabled = !flashing;
    }

    public void SetFlashSpeed(float speed)
    {
        flashSpeed = speed;
    }

    public void SetOn(bool on)
    {
        isOn = on;
        lightSource.enabled = on;
        isFlashing = false;
    }

    public void SetRandomFlash(bool random)
    {
        randomFlash = random;
    }

    public void SetMinMaxFlashSpeed(float minSpeed, float maxSpeed)
    {
        minFlashSpeed = minSpeed;
        maxFlashSpeed = maxSpeed;
    }

    public void SetInitialState(bool state)
    {
        initialState = state;

        // Actualizamos el estado actual de la luz
        if (lightSource.enabled && !state)
        {
            lightSource.enabled = false;
            isOn = false;
            isFlashing = false;
        }
        else if (!lightSource.enabled && state)
        {
            lightSource.enabled = true;
            isOn = true;
            isFlashing = false;
        }
    }
}


