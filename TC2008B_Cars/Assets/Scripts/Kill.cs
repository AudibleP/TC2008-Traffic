using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Kill : MonoBehaviour
{
    private int numCars = 0;
    public TextMeshProUGUI flujo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            Destroy(gameObject);
        }
    }
}
