using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelselect : MonoBehaviour
{
    //pass it reference to text
    [SerializeField] private TextMeshProUGUI message;

    // set the text to not show when first loaded
    void Start()
    {
        message.enabled = false;
    }

    public void showMessage()
    {
        StartCoroutine("hideMessage");
        //start sequence below
    }

    IEnumerator hideMessage()
    {
        //show the message then wait 2s then hide the message
        message.enabled = true;
        yield return new WaitForSeconds(2);
        message.enabled = false;
    }
}
