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
    public GameObject alert;

    private AudioManager Am;

    public bool dash;
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
        alert.SetActive(false);
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

            if (price.Equals(500) && PlayerPrefs.GetInt("dobleJump") != 1)
            {
                dobleJump = true;
                PlayerPrefs.SetInt("dobleJump", 1);

            }
            else if (price.Equals(1500)&& PlayerPrefs.GetInt("dashBool") != 1)
            {
                dash = true;
                PlayerPrefs.SetInt("dashBool", 1);
            }
            else if (price.Equals(4000) && PlayerPrefs.GetInt("timeStop") != 1)
            {
                TimeStop = true;
                PlayerPrefs.SetInt("timeStop", 1);
            }
            else
            {
                alert.SetActive(true);
                PlayerPrefs.SetInt("currentPoints", PlayerPrefs.GetInt("currentPoints") + price);
                StartCoroutine(DesactivateAlert());
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

    IEnumerator DesactivateAlert()
    {
        yield return new WaitForSeconds(1);

        alert.SetActive(false);
        
    }
}
