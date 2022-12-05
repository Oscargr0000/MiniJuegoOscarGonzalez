using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI puntosText;

    public GameObject gameOverCanvas;
    public GameObject uiCanvas;

    public TextMeshProUGUI tPointsOver;
    public TextMeshProUGUI pPointsOver;
    public TextMeshProUGUI recordPpoint;
    public TextMeshProUGUI recordTpoint;
    public TextMeshProUGUI totalPoints;

    private PlayerController Pc;
    private DataPersistance Dp;

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
        Pc = FindObjectOfType<PlayerController>();
        Dp = FindObjectOfType<DataPersistance>();
        uiCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
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
        Dp.LoadData();

        tPointsOver.text = timeText.text;
        pPointsOver.text = puntosText.text;

        gameOverCanvas.SetActive(true);
        uiCanvas.SetActive(false);
    }

    public void GoTo(int escena)
    {
        SceneManager.LoadScene(escena);
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

    public void ExitGame()
    {
        Application.Quit();
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
