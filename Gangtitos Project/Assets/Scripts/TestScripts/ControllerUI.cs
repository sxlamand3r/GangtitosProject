using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoItem;
    
    public void ActivateAndSetTextObject(string text)
    {
        try
        {
            infoItem.text = text;

            infoItem.gameObject.SetActive(true);
        }
        catch
        {
            Debug.LogError("Falta TextMeshPro");
        }
    }

    public void DeactivateTextObject()
    {
        try
        {
            infoItem.gameObject.SetActive(false);
        }
        catch
        {
            Debug.LogError("Falta TextMeshPro");
        }
        
    }

}
