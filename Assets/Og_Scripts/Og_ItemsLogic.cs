using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Og_ItemsLogic : MonoBehaviour
{
    

    public int recompensa;

    private Og_SpawnManager Sm;
    private Og_PlayerController Pc;
    private Og_UiManager UM;
    private Og_AudioManager Am;

    public int spawnedCouter;

    public bool isActivated;

    private void Start()
    {
        isActivated = true;
        spawnedCouter++;

        Pc = FindObjectOfType<Og_PlayerController>();
        PlayerPrefs.SetFloat("itemsSpeed", PlayerPrefs.GetFloat("itemsSpeed") + spawnedCouter * 0.03f);
    }

    private void Update()
    {
        if (Pc.desactivateTime.Equals(false))
        {
            transform.Translate(Vector2.down * PlayerPrefs.GetFloat("itemsSpeed") * Time.deltaTime);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameOver();
        }
    }


    void GameOver()
    {
        Sm = FindObjectOfType<Og_SpawnManager>();
        
        UM = FindObjectOfType<Og_UiManager>();
        Am = FindObjectOfType<Og_AudioManager>();

        UM.GameOverUI();
        SceneManager.LoadScene(2);

        Am._musicSource.Stop();
        Am.PLayMusic(1);

        
    }

}
