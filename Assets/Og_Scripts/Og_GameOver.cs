using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Og_GameOver : MonoBehaviour
{
    public TextMeshProUGUI tPointsOver;
    public TextMeshProUGUI pPointsOver;
    public TextMeshProUGUI recordPpoint;
    public TextMeshProUGUI recordTpoint;
    public TextMeshProUGUI totalPoints;

    private Og_AudioManager Am;

    void Start()
    {

        Am = FindObjectOfType<Og_AudioManager>();
        tPointsOver.text = PlayerPrefs.GetInt("puntos").ToString();
        pPointsOver.text = PlayerPrefs.GetFloat("tiempo").ToString("f2");

        recordTpoint.text = PlayerPrefs.GetFloat("tiempoFloat").ToString("f2");
        totalPoints.text = PlayerPrefs.GetInt("currentPoints").ToString();
        recordPpoint.text = PlayerPrefs.GetInt("puntuacionInt").ToString();

        Am.PLayMusic(0);
    }

    public void GoTo(int escena)
    {
        SceneManager.LoadScene(escena);
        Am.PLaySound(0);
    }
}
