using UnityEngine;

[System.Serializable]
public class BasicInfo
{

    public string id;
    public string name;

    [Multiline]
    public string description;

    //TODO ren
    //public Sprite logo;

    public BasicInfo(string id, string name, string description)
    { 
        this.id = id;
        this.name = name;
        this.description = description;
    }

}
