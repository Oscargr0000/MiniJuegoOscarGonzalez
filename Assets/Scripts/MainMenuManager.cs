using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public TextMeshProUGUI totalPoints;

    public GameObject Shop;
    public GameObject Menu;
    public TextMeshProUGUI alert;
    public Animator _animator;

    private AudioManager Am;

    private bool dash;
    private bool dobleJump;
    private bool TimeStop;


    private void Awake()
    {
        Am = FindObjectOfType<AudioManager>();
    }


    private void Start()
    {
        totalPoints.text = PlayerPrefs.GetInt("currentPoints").ToString();
        Shop.SetActive(false);
        Menu.SetActive(true);
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

            if (price.Equals(500) && dobleJump.Equals(false))
            {
                dobleJump = true;

            }else if (price.Equals(1500)&& dash.Equals(false))
            {
                dash = true;
            }else if (price.Equals(4000) && TimeStop.Equals(false))
            {
                TimeStop = true;
            }
            else
            {
                Debug.Log("funciona");
                StartCoroutine(Fade());
            }
        }

        totalPoints.text = PlayerPrefs.GetInt("currentPoints").ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);

        Am.PLaySound(0);
    }

    public void GoShop()
    {
        Shop.SetActive(true);
        Menu.SetActive(false);
        Am.PLaySound(1);
    }

    public void GoMenu()
    {
        Shop.SetActive(false);
        Menu.SetActive(true);
        Am.PLaySound(2);
    }

    IEnumerator Fade()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            Color c = alert.color;
            c.a = ft;
            alert.color = c;
            yield return null;
        }
    }
}
