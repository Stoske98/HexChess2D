using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tile
{
    public Vector2Int coordinate;
    protected Color base_color;
    protected bool is_walkable;
    protected List<Tile> neighbors;
    public TilePathData path_data;
    public List<IObject> objects;
    public GameObject game_object { get; set; }

    public Tile(Vector2Int _coordinate, Vector3 _position, GameObject _prefab, bool _is_walkable = true)
    {
        coordinate = _coordinate;
        is_walkable = _is_walkable;
        path_data = new TilePathData();
        neighbors = new List<Tile>();
        objects = new List<IObject>();
        game_object = GameObject.Instantiate(_prefab, _position, Quaternion.identity);
        game_object.transform.SetParent(MapController.Instance.main_tiles_containter);
    }

    public void SetBaseColor(Color _color)
    {
        base_color = _color;
        game_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = base_color;
    }
    public T GetObjectOfType<T>() where T : IObject
    {
        foreach (var obj in objects)
            if (obj is T _type)
                return _type;

        return default;
    }
    public void AddObject(IObject _obj)
    {
        if (_obj is Unit)
        {
            is_walkable = false;
            objects.Remove(objects.FirstOrDefault(obj => obj is Unit));
        }

        objects.Add(_obj);
    }
    public void RemoveObject(IObject _obj)
    {
        if (_obj is Unit)
            is_walkable = true;

        objects.Remove(_obj);
    }
    public void SetNeighbors(Map _map)
    {
        foreach (Vector2Int _neigbor_vector in _map.GetNeighborsVectors())
        {
            Tile _neigbor_tile = _map.GetTile(coordinate.x + _neigbor_vector.x, coordinate.y + _neigbor_vector.y);
            if (_neigbor_tile != null)
                neighbors.Add(_neigbor_tile);
        }
    }
    public List<Tile> GetNeighbors()
    {
        return neighbors;
    }

    public bool IsWalkable()
    {
        return is_walkable;
    }

    public void SetColor(Color color)
    {
        game_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
    }

    public void ResetColor()
    {
        game_object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = base_color;
        game_object.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
}
