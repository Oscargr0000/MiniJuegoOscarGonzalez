using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is attach to SpawnManager and contains the logic for the spawn of items
public class Og_SpawnManager : MonoBehaviour
{
    public GameObject[] fruits;

    public bool spawnON;

    void Start()
    {
        spawnON = true;
        StartCoroutine(Spawn(PlayerPrefs.GetFloat("spawnRate")));
    }


    //Logic for the spawning of the items
    public IEnumerator Spawn(float waitTime)
    {

        while (spawnON.Equals(true))
        {
            int randomNum = Random.Range(0, fruits.Length); //Select the item

            Vector2 position = new Vector2(Random.Range(-8f, 8f), 4.5f); //Select the position for the spawn

            PlayerPrefs.SetFloat("spawnRate", PlayerPrefs.GetFloat("spawnRate") - 0.03f); //Set the new velocity

            Instantiate(fruits[randomNum], position, transform.rotation);

            yield return new WaitForSeconds(PlayerPrefs.GetFloat("spawnRate"));
        }
       
    }

}
