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
    MyJson dataStep;

    Dictionary<int, GameObject> dict;
    public GameObject agent;

    void Start()
    {
        Step = 0;
        tokenSource = new CancellationTokenSource();
        dict = new Dictionary<int, GameObject>();
        agent.name = "default";
    }
    async void Update()
    {
        alphaTime += Time.deltaTime;
        if (alphaTime >= 3)
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

        var result = await Task.Run(() =>
        {
            MyJson NewJson = ApiHelper.GetNewJson();
            Step = NewJson.step;
            List<Data> arr = NewJson.positionList;
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
        var elapsedTime = watch.ElapsedMilliseconds;
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
                float timeElapsed = 0;
                float timeToMove = 3;
                while (timeElapsed < timeToMove)
                {
                    dict[element.id].transform.position = Vector3.Lerp(dict[element.id].transform.position, new Vector3(element.x, 7, element.y), timeElapsed / timeToMove);
                    timeElapsed += Time.deltaTime;

                }
                //dict[element.id].transform.position = Vector3.MoveTowards(dict[element.id].transform.position, new Vector3(element.x, 7, element.y), Time.deltaTime * 2);
            }
            else
            {
                
                GameObject new_car=Instantiate(agent, new Vector3 (element.x, 7 , element.y), Quaternion.Euler (0,170,0));
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

}