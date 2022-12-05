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
    public GameObject howTo;

    private AudioManager Am;

    public bool dash;


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
        howTo.SetActive(false); 
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
                PlayerPrefs.SetInt("dobleJump", 1);

            }
            else if (price.Equals(1500)&& PlayerPrefs.GetInt("dashBool") != 1)
            {
                dash = true;
                PlayerPrefs.SetInt("dashBool", 1);
            }
            else if (price.Equals(4000) && PlayerPrefs.GetInt("timeStop") != 1)
            {
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
        howTo.SetActive(false);
        Am.PLaySound(2);
    }
    public void GoHowTo()
    {
        howTo.SetActive(true);
        Menu.SetActive(false);
    }



    IEnumerator DesactivateAlert()
    {
        yield return new WaitForSeconds(1);

        alert.SetActive(false);
        
    }
}
