using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Debug = UnityEngine.Debug;

public class StepManager : MonoBehaviour
{
    private float alphaTime = 0;
    public static int Step { get; private set; }
    public static Action OnStepChanged;
    CancellationTokenSource tokenSource;

    public int size;
    bool calculationOver = true;
    //MyJson[] dataStep;
    MyJson dataStep;
    int indexStep ;

    public Camera mainCamera;
    public Camera breakCamera;

    Dictionary<int, GameObject> dict;
    public GameObject agent;

    float elapsedTime;
    public static int carCount;
    /*private int getIndex()
    {
        indexStep = (indexStep + 1) % 4;
        Debug.Log("IndexStep: " + indexStep);
        return indexStep;
    }*/

    /*private void Awake()
    {
        dataStep = new MyJson[4];
        MyJson NewJson = ApiHelper.GetNewJson();
        dataStep[0] = NewJson;
        NewJson = ApiHelper.GetNewJson();
        dataStep[1] = NewJson;
        NewJson = ApiHelper.GetNewJson();
        dataStep[2] = NewJson;
        Debug.Log("Array 0: "+dataStep[0].positionList.Count);
        Debug.Log("Array 1: " + dataStep[1].positionList.Count);
        Debug.Log("Array 2: " + dataStep[2].positionList.Count);
    }*/

    void Start()
    {
        Step = 0;
        tokenSource = new CancellationTokenSource();
        dict = new Dictionary<int, GameObject>();
        agent.name = "default";
        indexStep = 3;
        carCount = 0;


    }
    
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mainCamera.enabled = !mainCamera.enabled;
            breakCamera.enabled = !breakCamera.enabled;
        }
        alphaTime += Time.deltaTime;
        if (alphaTime >=0.1f)
        {
            if (calculationOver)
            {
                await PerformCalculations();
                Debug.Log("Positions: " + dataStep);
                Debug.Log("Step: " + dataStep.step);

                handlePositionList(dataStep);
                ConsoleOutput();
            }
            
        }
        
        
    }

    private async Task<MyJson> PerformCalculations()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();

        calculationOver = false;
        //await Task.Delay(1000);
        var result = await Task.Run(() =>
        {

            MyJson NewJson = ApiHelper.GetNewJson();
            Step = NewJson.step;
            alphaTime = 0f; 
            dataStep = NewJson;

            if (tokenSource.IsCancellationRequested)
            {
                return NewJson;
            }

            return NewJson;
        },tokenSource.Token);
        if (tokenSource.IsCancellationRequested)
        {
            Debug.Log("Cancelled");
            return null;
        }

        watch.Stop();
        elapsedTime = watch.ElapsedMilliseconds;
        Debug.Log("Operation took: " + elapsedTime + " ms");
        calculationOver = true;
        return null;
    }
    void handlePositionList(MyJson data)
    {
        List<Data> Positions = data.positionList;
        for (int i = 0; i < Positions.Count; i++)
        {
            Data element = Positions[i];
            //Debug.Log("AQUI EL VIEJO HACE QUE EL CARRITO " + Positions[i].id.ToString() + " SE MUEVA A LA POSICION (" + Positions[i].x.ToString() + ", " + Positions[i].y.ToString() + ")");
            if (dict.ContainsKey(element.id))
            {
                Debug.Log("Moueve Carro: " + element.id);
                //dict[element.id].transform.position = new Vector3(0,7,0);
                StartCoroutine(Movement(element));
                //dict[element.id].transform.position = Vector3.MoveTowards(dict[element.id].transform.position, new Vector3(element.x, 7, element.y), Time.deltaTime * 2);
            }
            else
            {
                
                GameObject new_car=Instantiate(agent, new Vector3 (element.x, 6.2f , element.y), Quaternion.Euler (0,170,0));
                carCount += 1;
                new_car.gameObject.name = "sedan" + element.id;
                Debug.Log("Crea Carro: " + new_car);
                dict.Add(element.id, new_car);

            }


        }
    }
    void OnDisable()
    {
        tokenSource.Cancel();
    }

    void ConsoleOutput()
    {
        foreach (KeyValuePair<int, GameObject> kvp in dict)
        {
            Debug.Log("ID: "+ kvp.Key+" Prefab: " +kvp.Value.name);
        }
            
    }
    IEnumerator Movement(Data element)
    {
        float timeElapsed = 0;
        float timeToMove = 90f;
        while (timeElapsed < timeToMove)
        {
            dict[element.id].transform.position = Vector3.Lerp(dict[element.id].transform.position, new Vector3(element.x, 6.2f, element.y), timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        dict[element.id].transform.position = new Vector3(element.x, 6.2f, element.y);
    }

}