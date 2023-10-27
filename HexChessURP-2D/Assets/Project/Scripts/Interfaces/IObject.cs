using UnityEngine;

public interface IObject
{
    string id { get; set; }
    public Data data { get; set; }
    GameObject game_object { get; set; }
    Visibility visibility { get; set; }
    void Dispose();
}
