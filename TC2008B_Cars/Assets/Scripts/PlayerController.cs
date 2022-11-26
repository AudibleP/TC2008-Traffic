using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;

    //public float horizontalInput;

    private float curSpeed = 0.0f;

    //private float speed = 5f;

    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    //public string inputId;

    //public GameObject WinPanel;
    //public TextMeshProUGUI TextWin;

    private float turnSpeed = 1000f;

    private float turnAfter = 0.5f;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal" + inputId);
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.Translate(Vector3.right * curSpeed);
        //transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed);
        curSpeed += acceleration;

        if (curSpeed > maxSpeed)
        {
            curSpeed = maxSpeed;
        }

        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Car")
        {
            WinPanel.SetActive(true);
            TextWin.text = "Player " + inputId + " Wins";
            Time.timeScale = 0f;
            curSpeed = 0;
            maxSpeed = 0;
            acceleration = 0;
        }*/

        /*if (other.gameObject.tag == "Rock")
        {
            transform.Rotate(0,20,0);
            Invoke("TurnAgain", 0.35f);

        }*/
        if(other.gameObject.tag == "Kill")
        {
            Destroy(gameObject);
        }
    }

    void TurnAgain()
    {
        transform.Rotate(0,-20,0);
    }

    /*public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }*/
}
