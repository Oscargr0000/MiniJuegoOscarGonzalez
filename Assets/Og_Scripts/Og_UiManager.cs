using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Og_UiManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI puntosText;


    public TextMeshProUGUI tPointsOver;
    public TextMeshProUGUI pPointsOver;
    public TextMeshProUGUI recordPpoint;
    public TextMeshProUGUI recordTpoint;
    public TextMeshProUGUI totalPoints;

    private Og_PlayerController Pc;
    private Og_DataPersistance Dp;

    public float currentTime;


    private float maxTime;

    private float coolDownDash;
    public Image backgroundCrono;
    public GameObject timeStopUI;
    public GameObject timeStopEffect;


    public GameObject uiTimPower;
    public GameObject uiDashPower;


    void Start()
    {
        Pc = FindObjectOfType<Og_PlayerController>();
        Dp = FindObjectOfType<Og_DataPersistance>();

        maxTime = Pc.dashColdDown;
        coolDownDash = 0f;
        timeStopEffect.SetActive(false);

        uiTimPower.SetActive(false);
        uiDashPower.SetActive(false);

        if (PlayerPrefs.GetInt("timeStop").Equals(1))
        {
            uiTimPower.SetActive(true);
        }

        if (PlayerPrefs.GetInt("dashBool").Equals(1))
        {
            uiDashPower.SetActive(true);
        }
    }


    void Update()
    {
        puntosText.text = Pc.puntiacionCouter.ToString();
        currentTime += Time.deltaTime;
        timeText.text = currentTime.ToString("f2");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        currentTime = 0f;
    }

    public void GameOverUI()
    {

        if (Pc.totalPuntos > Dp.maxPoints)
        {
            Dp.SaveDataPoints();
        }

        if(currentTime > Dp.maxTime)
        {
            Dp.SaveTime();
        }

        Dp.SaveCurrentPoints();

        PlayerPrefs.SetInt("puntos", Pc.totalPuntos);
        PlayerPrefs.SetFloat("tiempo", currentTime);
    }

    


     public void cronometro()
    {
        coolDownDash += Time.deltaTime;

        
        backgroundCrono.fillAmount = coolDownDash / maxTime;

        if (maxTime <= coolDownDash)
        {
            coolDownDash = 0f;
            Pc.activatecolddown = false;
        }

    }
   

    public void TimeStoped()
    {
        StartCoroutine(DesactivateTimeUI());
        timeStopUI.SetActive(false);
        timeStopEffect.SetActive(true);
    }

    IEnumerator DesactivateTimeUI()
    {
        yield return new WaitForSeconds(3);
        timeStopEffect.SetActive(false);
    }

}
