using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    [SerializeField]
    private GameObject tutorialPanel;

    public static int score = 0;
    private int click = 0;
    [SerializeField]
    private Text tutorialText;
    private int bestScore;

    [SerializeField]
    Image timebar;

    [SerializeField]
    private Text scoretxt, residuostxt,besttxt;

    [SerializeField]
    private Transform[] Residuos,Ref;

    [SerializeField]
    private GameObject residuosObj,refObj,camera,truck,finalPosTruck,player;
    public static int qntResiduos = 0;

    [SerializeField]
    private float tempo = 0, tempoDefinido,speedCamera,speedTruck;

    [SerializeField]
    private Text bestfinish, pointsfinish;

    void Awake()
    {
        
        Residuos = residuosObj.GetComponentsInChildren<Transform>();
        qntResiduos = Residuos.Length - 1;
        tutorialText.text = "Colete o maximo de residuos e arremece em suas lixeiras correspondentes antes do caminhão da reciclagem vir busca o lixo.";
        if (PlayerPrefs.HasKey("first"))
        {
            tutorialPanel.transform.position = new Vector3(-1000, -1000, -1000);
        }
        else
        {
            tutorialPanel.transform.position = GameObject.Find("PanelPos").transform.position;
            PlayerPrefs.SetInt("first", 1);
            PlayerPrefs.Save();
        }

        bestScore = PlayerPrefs.GetInt("best");
        score = 0;
        timebar.fillAmount = 0;
    }

    
    void Update()
    {
        
        if(PlayerPrefs.GetInt("click")>1)
        {
            tempo = tempo + Time.deltaTime;
        }
        
        
        
        timebar.fillAmount = tempo / 30;
        ScoreAndQnt();
        CameraMove();
        MoveTruck();
        besttxt.text = bestScore.ToString();
        
        if(tempo >= tempoDefinido)
        {
            if (score >= bestScore)
            {
                PlayerPrefs.SetInt("best", score);
                PlayerPrefs.Save();
            }
            
        }
        Finish();
        
    }

    private void CameraMove()
    {
        if (tempo > tempoDefinido)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position,
                new Vector3(0, 10, 6), speedCamera * Time.deltaTime);
        }
        else
        {
            if(player.transform.position.z >= -8 && player.transform.position.z<=0)
            {
                camera.transform.position = Vector3.MoveTowards(camera.transform.position,
                new Vector3(0, 10, player.transform.position.z), speedCamera * Time.deltaTime);
            }
            
        }
    }
    private void ScoreAndQnt()
    {
        Ref = refObj.GetComponentsInChildren<Transform>();
        qntResiduos = (Residuos.Length - Ref.Length)/2;

        scoretxt.text = score.ToString();
        
        residuostxt.text = qntResiduos.ToString();
    }
    private void MoveTruck()
    {
        if(camera.transform.position.z >= 5)
        {
            truck.transform.position = Vector3.MoveTowards(truck.transform.position,
            finalPosTruck.transform.position, speedTruck * Time.deltaTime);
        }
        
    }
    private void Finish()
    {
        if(truck.transform.position.x >= finalPosTruck.transform.position.x)
        {
            pointsfinish.text = score.ToString();
            bestfinish.text = PlayerPrefs.GetInt("best").ToString();
            GameObject.Find("PanelFinish").transform.position = GameObject.Find("PanelPos").transform.position;
        }
        else
        {
            GameObject.Find("PanelFinish").transform.position = new Vector3(-1000,- 1000, -1000);
        }
    }

    public void okButton()
    {
        click = click + 1;
        PlayerPrefs.SetInt("click", click);
        PlayerPrefs.Save();
        if (click == 1)
        {
            tutorialText.text = "Para coletar os residuos basta encostar neles. Para arremeçar os residuos" +
                " encoste com o personagem no muro, mire e pressione o botão arremeçar no canto direito. Tente fazer a maior pontuação possivel.";
        }
        else
        {
            tutorialPanel.transform.position = new Vector3(-1000, -1000, -1000);
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
