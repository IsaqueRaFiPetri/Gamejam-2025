using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class Passcode : MonoBehaviour
{
    [SerializeField] string code;
    string Nr = null;
    int NrIndex = 0;
    string alpha;
    public TextMeshProUGUI UiText = null;
    

    [SerializeField] GameObject doorObj, painelObj;

    public void CodeFunction(string Numbers)
    {
        NrIndex++;
        Nr = Nr + Numbers;
        UiText.text = Nr;
    }

    public void Enter()
    {
        if (Nr == code)
        {
            doorObj.SetActive(false);
        }
    }
    public void Delete()
    {
        NrIndex++;
        Nr = null;
        UiText.text = Nr;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            Debug.Log("Player encostando no painel!");

        painelObj.SetActive(true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            painelObj.SetActive(false);
    }
}
//https://www.youtube.com/watch?v=VRbRxiCNR2s 