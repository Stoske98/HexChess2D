using System;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMap : Map
{
    List<Vector2Int> neighbors_vectors = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(1, -1), new Vector2Int(-1, 0),
                                                                                new Vector2Int(1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -1) };

    List<Vector2Int> diagonals_neighbors_vectors = new List<Vector2Int>() { new Vector2Int(2, -1), new Vector2Int(1, -2), new Vector2Int(-1, -1),
                                                                                        new Vector2Int(-2, 1), new Vector2Int(-1, 2), new Vector2Int(1, 1) };
    public HexagonMap(int _column, int _row, float _offset, float _height, Pallete _map_pallete) : base(_column, _row, _offset, _height, _map_pallete)
    {
    }

    public override void CreateMap()
    {
        Vector3 pos = Vector3.zero;

        for (int c = -column; c <= column; c++)
        {
            int r1 = Mathf.Max(-column, -c - column);
            int r2 = Mathf.Min(column, -c + column);


            int counter = 0;
            int index = 0;

            for (int i = 0; i < Mathf.Abs(c); i++)
            {
                counter++;
                if (counter == 3 || counter == 0)
                {
                    counter = index = 0;
                    continue;
                }
                index = counter == 1 ? 2 : 1;
            }

            for (int r = r1; r <= r2; r++)
            {

                if (index == 3)
                    index = 0;

                pos.x = height * 3.0f / 2.0f * c * offset;
                pos.y = height * Mathf.Sqrt(3.0f) * (r + c / 2.0f) * offset;

                Hex hex = new Hex(new Vector2Int(c, r), pos, MapController.Instance.hexagon_prefab);
                hex.game_object.name = "Hex[" + c + "][" + r + "]";
                //setup background collor
                hex.game_object.transform.GetComponent<SpriteRenderer>().color = MapController.Instance.GetPallete(pallete).pallete[index];
                //setup base color
                hex.SetBaseColor(new Color(255f, 255f, 255f, 0.0f));
                tiles.Add(hex);

                index++;
            }
        }

        foreach (Tile tile in tiles)
            tile.SetNeighbors(this);
    }

    public override Direction CoordinatesToDirection(Vector2Int _coordinates)
    {
        if (_coordinates == new Vector2Int(0, 1))
        {
            return Direction.UP;
        }
        else if (_coordinates == new Vector2Int(0, -1))
        {
            return Direction.DOWN;
        }
        else if (_coordinates == new Vector2Int(1, -1))
        {
            return Direction.UPPER_RIGHT;
        }
        else if (_coordinates == new Vector2Int(-1, 0))
        {
            return Direction.UPPER_LEFT;
        }
        else if (_coordinates == new Vector2Int(1, 0))
        {
            return Direction.LOWER_RIGHT;
        }
        else if (_coordinates == new Vector2Int(-1, 1))
        {
            return Direction.LOWER_LEFT;
        }
        else
        {
            throw new ArgumentException("Invalid coordinates");
        }
    }
    public override Vector2Int DirectionToCoordinates(Direction _direction)
    {
        switch (_direction)
        {
            case Direction.UP:
                return new Vector2Int(0, 1);
            case Direction.DOWN:
                return new Vector2Int(0, -1);
            case Direction.UPPER_RIGHT:
                return new Vector2Int(1, -1);
            case Direction.UPPER_LEFT:
                return new Vector2Int(-1, 0);
            case Direction.LOWER_RIGHT:
                return new Vector2Int(1, 0);
            case Direction.LOWER_LEFT:
                return new Vector2Int(-1, 1);
            default:
                return new Vector2Int();
        }
    }

    public override List<Vector2Int> GetDiagonalsNeighborsVectors()
    {
        return diagonals_neighbors_vectors;
    }

    public override List<Vector2Int> GetNeighborsVectors()
    {
        return neighbors_vectors;
    }

    public override Tile OnHoverMapGetTile(Vector2 mouse_position)
    {
        Vector2Int rounded_vector = GetRoundedVector(mouse_position.x, mouse_position.y);

        Hex closest_hex = null;
        Hex neighbor = null;

        List<Vector2Int> neighbor_vectors = new List<Vector2Int>()
        {
            rounded_vector,
            rounded_vector + neighbors_vectors[0],
            rounded_vector + neighbors_vectors[1],
            rounded_vector + neighbors_vectors[2],
            rounded_vector + neighbors_vectors[3],
            rounded_vector + neighbors_vectors[4],
            rounded_vector + neighbors_vectors[5],
        };

        foreach (Vector2Int nv2 in neighbor_vectors)
        {
            foreach (Hex hex in tiles)
            {
                if (hex.coordinate.x == nv2.x && hex.coordinate.y == nv2.y)
                {
                    neighbor = hex;
                    if (closest_hex == null)
                        closest_hex = neighbor;
                    else
                    if (Vector2.Distance(new Vector2(neighbor.game_object.transform.position.x, neighbor.game_object.transform.position.y), new Vector2(mouse_position.x, mouse_position.y)) <
                                    Vector2.Distance(new Vector2(closest_hex.game_object.transform.position.x, closest_hex.game_object.transform.position.y), new Vector2(mouse_position.x, mouse_position.y)))
                        closest_hex = neighbor;
                    break;
                }
            }
        }
        return closest_hex;
    }

    public override List<Tile> TilesInRange(Tile _tile, int _range)
    {
        List<Tile> list_of_tiles = new List<Tile>();

        for (int q = -_range; q <= _range; q++)
        {
            for (int r = Mathf.Max(-_range, -q - _range); r <= Mathf.Min(_range, -q + _range); r++)
            {
                Vector2Int neighbor_offset = new Vector2Int(q, r);
                Tile neighbor_in_range = GetTile(_tile.coordinate.x + neighbor_offset.x, _tile.coordinate.y + neighbor_offset.y);
                if (neighbor_in_range != null)
                    list_of_tiles.Add(neighbor_in_range);
            }
        }

        return list_of_tiles;
    }

    private Vector2Int GetRoundedVector(float x, float y)
    {
        float _x = (2.0f / 3 * x) / (height * offset);
        float _y = (-1.0f / 3 * x + Mathf.Sqrt(3) / 3 * y) / (height * offset);

        return Round(_x, _y);
    }

    private Vector2Int Round(float x, float y)
    {
        float s = -x - y;

        int _x = Mathf.RoundToInt(x);
        int _y = Mathf.RoundToInt(y);
        int _s = Mathf.RoundToInt(s);

        float _xDiff = Mathf.Abs(_x - x);
        float _yDiff = Mathf.Abs(_y - y);
        float _sDiff = Mathf.Abs(_s - s);

        if (_xDiff > _yDiff && _xDiff > _sDiff)
            _x = -_y - _s;
        else if (_yDiff > _sDiff)
            _y = -_x - _s;
        else
            _s = -_x - _y;
        return new Vector2Int(_x, _y);
    }
}
