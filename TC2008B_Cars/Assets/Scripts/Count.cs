using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count : MonoBehaviour
{
    private float numCars = 0;
    public TextMeshProUGUI flujo;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Counter")
        {
            numCars += 1;
            float a  =numCars / timer;
           
            flujo.text = "Flujo de carros: " + a;
        }
    }
}
