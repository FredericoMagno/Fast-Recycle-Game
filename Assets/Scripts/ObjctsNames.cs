using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjctsNames : MonoBehaviour
{

    [SerializeField]
    GameObject objtxt,point;
    [SerializeField]
    Text objname;

    private string name,showname;
    private Transform[] points;
    private int qnt;

    void Start()
    {
        
    }

    
    void Update()
    {
        points = point.GetComponentsInChildren<Transform>();
        qnt = points.Length;
        
        if(qnt == 3)
        {
            name = points[1].name;
            objtxt.active = true;
        }
        else if( qnt == 7)
        {
            name = points[5].name;
            objtxt.active = true;
        }
        else
        {
            objtxt.active = false;
        }

        if (name == "vidro")
        {
            showname = "Garrafa de Vidro";
        }
        else if (name == "metal") 
        {
            showname = "latinha de refrigerante";
        }
        else if (name == "papel") 
        {
            showname = "papel";
        }
        else if (name == "plastico")
        {
            showname = "copo descartavel";
        }

        objname.text = showname;
    }
}
