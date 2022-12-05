using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] fruits;

    public bool spawnON;

    void Start()
    {
        spawnON = true;
        StartCoroutine(Spawn(PlayerPrefs.GetFloat("spawnRate")));
    }

    public IEnumerator Spawn(float waitTime)
    {

        while (spawnON.Equals(true))
        {
            int randomNum = Random.Range(0, fruits.Length);

            Vector2 position = new Vector2(Random.Range(-8f, 8f), 4);

            PlayerPrefs.SetFloat("spawnRate", PlayerPrefs.GetFloat("spawnRate") - 0.03f);

            Instantiate(fruits[randomNum], position, transform.rotation);

            yield return new WaitForSeconds(PlayerPrefs.GetFloat("spawnRate"));
        }
       
    }

}
