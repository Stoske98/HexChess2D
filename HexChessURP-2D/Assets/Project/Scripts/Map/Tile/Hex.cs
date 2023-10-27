using UnityEngine;

public class Hex : Tile
{
    public int S { set; get; }
    public Hex(Vector2Int _coordinate, Vector3 _position, GameObject _prefab, bool _is_walkable = true) : base(_coordinate, _position, _prefab, _is_walkable)
    {
        S = -coordinate.x - coordinate.y;
    }
}
