using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    private GameObject Coletado, Point, Ref;
    public static bool muro = false;

    private Animator anim;

    [SerializeField]
    private FixedJoystick moveJoystick;
   

    [SerializeField]
    private float speedPlayer;
    private float moveH, moveV,moveHJ,moveVJ;

    private float time;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Point = GameObject.Find("Point");
        Ref = GameObject.Find("Ref");
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        MovePlayer();
        MovePlayerJoystick();
        SoltarLixo();
        
    }

    private void MovePlayer()
    {
        moveH = Input.GetAxis("Horizontal"); 
        moveV = Input.GetAxis("Vertical");
       
        rb.velocity = new Vector3(moveH*speedPlayer, rb.velocity.y, moveV*speedPlayer);

        Vector3 dir = new Vector3(moveH, 0, moveV).normalized;
        transform.LookAt(transform.position + dir);
    }
    private void MovePlayerJoystick()
    {
        moveHJ = moveJoystick.Horizontal;
        moveVJ = moveJoystick.Vertical;

        rb.velocity = new Vector3(moveHJ * speedPlayer, rb.velocity.y, moveVJ * speedPlayer);

        Vector3 dir = new Vector3(moveHJ, 0, moveVJ).normalized;
        transform.LookAt(transform.position + dir);
        if(moveHJ != 0 || moveVJ != 0)
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
            if((transform.rotation.y > -0.6f && transform.rotation.y <= 0.6f) && muro == true)
            {
                Coletado.transform.parent = Ref.transform;
                Coletado.AddComponent<Rigidbody>();
                Coletado.GetComponent<Rigidbody>().AddForce(Point.transform.forward*250/*new Vector3(0, 200, 100)*/);
                Coletado = null;
            }
        }
    }
    public void SoltarLixoButton()
    {
        if(Coletado != null && muro == true)
        {
            
            if(transform.rotation.y>-0.6f && transform.rotation.y <= 0.6f)
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
        if(collision.gameObject.layer == 6 && Coletado == null)
        {
            Coletado = collision.gameObject;
            Coletado.transform.parent = Point.transform;
            Coletado.transform.position = Point.transform.position;
        }
        if(collision.gameObject.layer == 8)
        {
            muro = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Coletado.transform.parent = Ref.transform;
            Coletado = null;
        }
        if (collision.gameObject.layer == 8)
        {
            muro = false;
        }
    }


}
