using UnityEngine; //Para la clase JsonUtility
using System.Net;
using System.IO;

public class ApiHelper : MonoBehaviour
{
    public static MyJson GetNewJson()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://localhost:5000/position");

        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());

        string json = reader.ReadToEnd();
        Debug.Log(json);
        //return json;
        return JsonUtility.FromJson<MyJson>(json);
    }

}
