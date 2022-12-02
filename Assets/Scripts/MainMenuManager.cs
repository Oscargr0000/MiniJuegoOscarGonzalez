using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public TextMeshProUGUI totalPoints;

    private bool dash;
    private bool dobleJump;
    private bool TimeStop;


    private void Start()
    {
        totalPoints.text = PlayerPrefs.GetInt("currentPoints").ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.SetInt("currentPoints", PlayerPrefs.GetInt("currentPoints") + 500);
        }
    }

    public void Buy(int price)
    {
        if(PlayerPrefs.GetInt("currentPoints") >= price)
        {
            PlayerPrefs.SetInt("currentPoints", PlayerPrefs.GetInt("currentPoints") - price);

            if (price.Equals(500))
            {
                dobleJump = true;
                Debug.Log("tienes el salto");
            }else if (price.Equals(1500))
            {
                dash = true;
            }else if (price.Equals(4000))
            {
                TimeStop = true;
            }
        }

        totalPoints.text = PlayerPrefs.GetInt("currentPoints").ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
