using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminho : MonoBehaviour
{

    [SerializeField]
    GameObject Point, trajectory;
    GameObject player;
    

    [SerializeField]
    GameObject[] points;
    private float tempo;
    void Start()
    {
        Player.muro = false;
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        //TrajectoryLauch();
        DrawTrajectory();
    }

    private void TrajectoryLauch()
    {
        if (Player.muro)
        {
            tempo = tempo + Time.deltaTime;
            if (tempo > 0.2f)
            {
                GameObject newTrajectory = Instantiate(trajectory,
                Point.transform.position, Quaternion.identity);
                newTrajectory.transform.parent = Point.transform;
                //newTrajectory.GetComponent<Rigidbody>().AddForce(0, 200, 100);
                newTrajectory.GetComponent<Rigidbody>().AddForce(Point.transform.forward*250);
                tempo = 0;
            }

        }
        else
        {
            tempo = 0;
        }
    }
    private void DrawTrajectory()
    {
        if(Player.muro && (player.transform.rotation.y > -0.6f && player.transform.rotation.y <= 0.6f))
        {
            for(int i = 0;i<4;i++)
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


}
