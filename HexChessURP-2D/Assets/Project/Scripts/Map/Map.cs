using System.Collections.Generic;
using UnityEngine;
public abstract class Map
{
    protected int column;
    protected int row;
    public float offset;
    public float height;
    public List<Tile> tiles;
    protected Pallete pallete;
    public Map(int _column, int _row, float _offset, float _height, Pallete _map_pallete)
    {
        column = _column;
        row = _row;
        offset = _offset;
        height = _height;
        pallete = _map_pallete;
        tiles = new List<Tile>();
        CreateMap();
    }
    public Tile GetTile(int column, int row)
    {

        foreach (Tile tile in tiles)
            if (tile.coordinate.x == column && tile.coordinate.y == row)
                return tile;

        return null;
    }
    public Tile GetTile(IObject obj)
    {
        foreach (Tile tile in tiles)
            foreach (IObject _ogj in tile.objects)
                if (_ogj == obj)
                    return tile;

        return null;
    }
    public void PlaceObject(IObject _obj, Tile _tile)
    {
        _tile.AddObject(_obj);
        _obj.game_object.transform.position = _tile.game_object.transform.position;
    }
    public void PlaceObject(IObject _obj, int _column, int _row)
    {
        foreach (Tile _tile in tiles)
        {
            if (_tile.coordinate.x == _column && _tile.coordinate.y == _row)
            {
                _tile.AddObject(_obj);
                _obj.game_object.transform.position = _tile.game_object.transform.position;
                break;
            }
        }
    }
    public abstract void CreateMap();
    public abstract Tile OnHoverMapGetTile(Vector2 position);
    public abstract List<Vector2Int> GetNeighborsVectors();
    public abstract List<Vector2Int> GetDiagonalsNeighborsVectors();
    public abstract List<Tile> TilesInRange(Tile _tile, int _range);
    public List<Tile> GetTilesInDirection(Direction direction, Tile center_tile, int range, bool count_unwalkable_tiles = true)
    {
        List<Tile> direction_tiles = new List<Tile>();
        Vector2Int direction_coordinates = DirectionToCoordinates(direction);

        for (int i = 1; i < range + 1; i++)
        {
            Tile tile = GetTile(i * direction_coordinates.x + center_tile.coordinate.x, i * direction_coordinates.y + center_tile.coordinate.y);
            if (tile != null)
            {
                if (count_unwalkable_tiles)
                    direction_tiles.Add(tile);
                else
                {
                    if (tile.IsWalkable())
                        direction_tiles.Add(tile);
                    else
                        break;
                }
            }
        }
        return direction_tiles;
    }
    public abstract Vector2Int DirectionToCoordinates(Direction _direction);
    public abstract Direction CoordinatesToDirection(Vector2Int _coordinates);
}
