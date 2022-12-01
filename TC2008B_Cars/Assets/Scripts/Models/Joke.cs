using System.Collections.Generic;
[System.Serializable]

public class Joke {
   public string[] categories;
   public string created_at;
   public string icon_url;
   public string id;
   public string updated_at;
   public string url;
   public string value;
}

[System.Serializable]
public class MyJson {

    public List<Data> positionList;
    public int step;

}

[System.Serializable]
public class Data {
    public int id;
    public float x;
    public float y;
}
