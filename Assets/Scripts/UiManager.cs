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

    private bool gameOver;


    private float maxTime;

    private float coolDownDash;
    public Image backgroundCrono;


    void Start()
    {
        Pc = FindObjectOfType<PlayerController>();
        Dp = FindObjectOfType<DataPersistance>();
        uiCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        gameOver = false;
        maxTime = Pc.dashColdDown;
        coolDownDash = maxTime;
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

        gameOver = true;
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
        coolDownDash -= Time.deltaTime;

        if (coolDownDash <= 0f)
        {
            coolDownDash = 0f;
           Pc.activatecolddown = false;
        }

        backgroundCrono.fillAmount = coolDownDash / maxTime;


    }
}
