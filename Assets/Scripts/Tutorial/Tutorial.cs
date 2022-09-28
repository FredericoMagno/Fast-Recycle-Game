using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{


    private Rigidbody rb;
    private GameObject Coletado, Point, Ref;
    private bool muro = false;

    [SerializeField]
    GameObject panelTutorial;

    private Animator anim;

    [SerializeField]
    private FixedJoystick moveJoystick;


    [SerializeField]
    private float speedPlayer;
    private float moveH, moveV, moveHJ, moveVJ;

    private float time;

    [SerializeField]
    GameObject[] points;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Point = GameObject.Find("Point");
        Ref = GameObject.Find("Ref");
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        MovePlayerJoystick();
        SoltarLixo();
        DrawTrajectory();
    }
   
    private void MovePlayerJoystick()
    {
        moveHJ = moveJoystick.Horizontal;
        moveVJ = moveJoystick.Vertical;

        rb.velocity = new Vector3(moveHJ * speedPlayer, rb.velocity.y, moveVJ * speedPlayer);

        Vector3 dir = new Vector3(moveHJ, 0, moveVJ).normalized;
        transform.LookAt(transform.position + dir);
        if (moveHJ != 0 || moveVJ != 0)
        {
            anim.SetTrigger("Move");
        }
        else
        {
            anim.SetTrigger("NoMove");
        }
    }



    private void SoltarLixo()
    {

        if (Input.GetButtonDown("Jump") && Coletado != null)
        {
            if ((transform.rotation.y > -0.6f && transform.rotation.y <= 0.6f) && muro == true)
            {
                Coletado.transform.parent = Ref.transform;
                Coletado.AddComponent<Rigidbody>();
                Coletado.GetComponent<Rigidbody>().AddForce(Point.transform.forward * 250/*new Vector3(0, 200, 100)*/);
                Coletado = null;
            }
        }
    }
    public void SoltarLixoButton()
    {
        if (Coletado != null && muro == true)
        {

            if (transform.rotation.y > -0.6f && transform.rotation.y <= 0.6f)
            {
                Coletado.transform.parent = Ref.transform;
                Coletado.AddComponent<Rigidbody>();
                Coletado.GetComponent<Rigidbody>().AddForce(Point.transform.forward * 250/*new Vector3(0, 200, 100)*/);
                Coletado = null;
            }



        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && Coletado == null)
        {
            Coletado = collision.gameObject;
            Coletado.transform.parent = Point.transform;
            Coletado.transform.position = Point.transform.position;
            panelTutorial.active = true;
            GameObject.Find("textTutorial").GetComponent<Text>().text = "Você coletou uma garrafa de vidro. Arremese-a na lixeira destinada a vidros. Para arremeçar basta encostar com o personagem no muro, mirar e pressionar o botão arremeçar no canto direiro da tela.";
        }
        if (collision.gameObject.layer == 8)
        {
            muro = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Coletado.transform.parent = Ref.transform;
            Coletado = null;
        }
        if (collision.gameObject.layer == 8)
        {
            muro = false;
        }
    }

    private void DrawTrajectory()
    {
        if (muro && (transform.rotation.y > -0.6f && transform.rotation.y <= 0.6f))
        {
            for (int i = 0; i < 4; i++)
            {
                points[i].active = true;
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                points[i].active = false;
            }
        }
    }

    public void buttonOk()
    {
        panelTutorial.active = false;
    }

    
}
