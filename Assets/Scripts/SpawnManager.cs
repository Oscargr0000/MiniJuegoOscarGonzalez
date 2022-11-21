using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] fruits;

    private bool spawnON;

    // Start is called before the first frame update
    void Start()
    {
        spawnON = true;
        StartCoroutine(Spawn(2));
    }

    IEnumerator Spawn(float waitTime)
    {

        while (spawnON.Equals(true))
        {
            int randomNum = Random.Range(0, fruits.Length);

            Vector2 position = new Vector2(Random.Range(-8f, 8f), 4);


            Instantiate(fruits[randomNum], position, transform.rotation);

            yield return new WaitForSeconds(waitTime);
        }
       
    }

}
