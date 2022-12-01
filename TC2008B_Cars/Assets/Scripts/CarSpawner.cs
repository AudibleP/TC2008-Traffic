using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    public List<GameObject> ai;
    public GameObject[] spawn;

    int indexAI;
    int indexSpawn;
    float Timer = 0.01f;

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            for (int x = 0; x < 1; x++)
            {
                indexAI = UnityEngine.Random.Range(0, ai.Count);
                indexSpawn = UnityEngine.Random.Range(0, spawn.Length);

                //I am currently using this kind of format since this is what I know for now.
                Instantiate(ai[indexAI], spawn[indexSpawn].transform.position, spawn[indexSpawn].transform.rotation);
            }
            Timer = 1f;
        }
        
    }
}
