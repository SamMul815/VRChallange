using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBoxToggle : MonoBehaviour {

    public GameObject button1;
    public GameObject button1click;
    public GameObject button2;
    public GameObject button2click;


    public bool isTeleport;

    private void Awake()
    {
        button1.SetActive(true);
        button1click.SetActive(false);
        button2.SetActive(false);
        button2click.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isTeleport)
        {
            button1.SetActive(false);
            button1click.SetActive(true);
            button2.SetActive(false);
            button2click.SetActive(false);
        }
        else
        {
            button1.SetActive(false);
            button1click.SetActive(false);
            button2.SetActive(false);
            button2click.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<UISelect>().IsSelect)
            {
                other.GetComponent<UISelect>().OKSelect();
                Click();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isTeleport)
        {
            button1.SetActive(true);
            button1click.SetActive(false);
            button2.SetActive(false);
            button2click.SetActive(false);
        }
        else
        {
            button1.SetActive(false);
            button1click.SetActive(false);
            button2.SetActive(true);
            button2click.SetActive(false);
        }
    }

    public void Click()
    {
        if(isTeleport)
        {
            isTeleport = false;

            button1.SetActive(false);
            button1click.SetActive(false);
            button2.SetActive(false);
            button2click.SetActive(true);

        }
        else
        {
            button1.SetActive(false);
            button1click.SetActive(true);
            button2.SetActive(false);
            button2click.SetActive(false);
            isTeleport = true;
        }
    }

}
