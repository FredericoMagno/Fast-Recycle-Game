using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Residuo : MonoBehaviour
{
    private string name;
    [SerializeField]
    private Text objtxt;
    [SerializeField]
    private GameObject textoObj;

    void Start()
    {
        name = gameObject.name;
    }

    
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("plastico"))
        {
            if(name == "plastico")
            {
                //print("Certo");
                Manager.score = Manager.score + 2;
            }
            else
            {
                if(Manager.score>0)
                {
                    Manager.score = Manager.score - 1;
                }
            }
        }
        if (other.CompareTag("metal"))
        {
            if (name == "metal")
            {
                //print("Certo");
                Manager.score = Manager.score + 2;
            }
            else
            {
                if (Manager.score > 0)
                {
                    Manager.score = Manager.score - 1;
                }
                //print("Errado");
            }
        }
        if (other.CompareTag("papel"))
        {
            if (name == "papel")
            {
                //print("Certo");
                Manager.score = Manager.score + 2;
            }
            else
            {
                if (Manager.score > 0)
                {
                    Manager.score = Manager.score - 1;
                }
            }
        }
        if (other.CompareTag("vidro"))
        {
            if (name == "vidro")
            {
                //print("Certo");
                Manager.score = Manager.score + 2;
            }
            else
            {
                if (Manager.score > 0)
                {
                    Manager.score = Manager.score - 1;
                }

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer==9)
        {
            if(gameObject.transform.parent == GameObject.Find("Ref").transform)
            {
                if(Manager.score>3)
                {
                    Manager.score = Manager.score - 4;
                }
                else
                {
                    Manager.score = 0;
                }
            }
        }
    }
}
