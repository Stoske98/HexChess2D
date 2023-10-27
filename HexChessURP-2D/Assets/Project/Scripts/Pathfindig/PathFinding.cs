using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding
{
    public static List<Tile> FindPath_AStar(Tile start, Tile end, List<Tile> tiles)
    {
        foreach (Tile tile in tiles)
            tile.path_data.cost = int.MaxValue;

        start.path_data.cost = 0;
        Comparison<Tile> heuristicComparison = (lhs, rhs) =>
        {
            float lhsCost = lhs.path_data.cost + GetEuclideanHeuristicCost(lhs, end);
            float rhsCost = rhs.path_data.cost + GetEuclideanHeuristicCost(rhs, end);

            return lhsCost.CompareTo(rhsCost);
        };

        MinHeap<Tile> frontier = new MinHeap<Tile>(heuristicComparison);
        frontier.Add(start);

        HashSet<Tile> visited = new HashSet<Tile>();
        visited.Add(start);

        start.path_data.prev_tile = null;

        while (frontier.Count > 0)
        {
            Tile current = frontier.Remove();

            if (current == end)
            {
                break;
            }

            foreach (var neighbor in current.GetNeighbors())
            {
                if (neighbor.IsWalkable())
                {
                    int newNeighborCost = current.path_data.cost + neighbor.path_data.weight;
                    if (newNeighborCost < neighbor.path_data.cost)
                    {
                        neighbor.path_data.cost = newNeighborCost;
                        neighbor.path_data.prev_tile = current;
                    }

                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        frontier.Add(neighbor);
                    }
                }
            }
        }

        List<Tile> path = BacktrackToPath(end);

        return path;
    }
    public static List<Tile> BFS_HexesMoveRange(Tile _start, float _move_range, List<Tile> _tiles)
    {
        List<Tile> tilesInRange = new List<Tile>();

        foreach (Tile tile in _tiles)
            tile.path_data.cost = int.MaxValue;

        _start.path_data.cost = 0;

        HashSet<Tile> visited = new HashSet<Tile>();
        visited.Add(_start);

        Queue<Tile> frontier = new Queue<Tile>();
        frontier.Enqueue(_start);

        _start.path_data.prev_tile = null;

        while (frontier.Count > 0)
        {
            Tile current = frontier.Dequeue();

            foreach (var neighbor in current.GetNeighbors())
            {
                if (neighbor.IsWalkable())
                {
                    int newNeighborCost = current.path_data.cost + neighbor.path_data.weight;

                    if (newNeighborCost < neighbor.path_data.cost)
                    {
                        neighbor.path_data.cost = newNeighborCost;
                        neighbor.path_data.prev_tile = current;
                    }


                    if (!visited.Contains(neighbor))
                    {
                        if (_move_range - newNeighborCost >= 0)
                        {
                            tilesInRange.Add(neighbor);
                        }
                        else
                        {
                            return tilesInRange;
                        }
                        visited.Add(neighbor);
                        frontier.Enqueue(neighbor);
                    }
                }
            }
        }
        return tilesInRange;
    }
    private static float GetEuclideanHeuristicCost(Tile current, Tile end)
    {
        float heuristicCost = (current.game_object.transform.position - end.game_object.transform.position).magnitude;
        return heuristicCost;
    }

    private static List<Tile> BacktrackToPath(Tile end)
    {
        Tile current = end;
        List<Tile> path = new List<Tile>();

        while (current != null)
        {
            path.Add(current);
            current = current.path_data.prev_tile;
        }

        path.Reverse();

        return path;
    }
}

public class MinHeap<T>
{
    private const int InitialCapacity = 4;

    private T[] _arr;
    private int _lastItemIndex;
    private IComparer<T> _comparer;

    public MinHeap()
        : this(InitialCapacity, Comparer<T>.Default)
    {
    }

    public MinHeap(int capacity)
        : this(capacity, Comparer<T>.Default)
    {
    }

    public MinHeap(Comparison<T> comparison)
        : this(InitialCapacity, Comparer<T>.Create(comparison))
    {
    }

    public MinHeap(IComparer<T> comparer)
        : this(InitialCapacity, comparer)
    {
    }

    public MinHeap(int capacity, IComparer<T> comparer)
    {
        _arr = new T[capacity];
        _lastItemIndex = -1;
        _comparer = comparer;
    }

    public int Count
    {
        get
        {
            return _lastItemIndex + 1;
        }
    }

    public void Add(T item)
    {
        if (_lastItemIndex == _arr.Length - 1)
        {
            Resize();
        }

        _lastItemIndex++;
        _arr[_lastItemIndex] = item;

        MinHeapifyUp(_lastItemIndex);
    }

    public T Remove()
    {
        if (_lastItemIndex == -1)
        {
            throw new InvalidOperationException("The heap is empty");
        }

        T removedItem = _arr[0];
        _arr[0] = _arr[_lastItemIndex];
        _lastItemIndex--;

        MinHeapifyDown(0);

        return removedItem;
    }

    public T Peek()
    {
        if (_lastItemIndex == -1)
        {
            throw new InvalidOperationException("The heap is empty");
        }

        return _arr[0];
    }

    public void Clear()
    {
        _lastItemIndex = -1;
    }

    private void MinHeapifyUp(int index)
    {
        if (index == 0)
        {
            return;
        }

        int childIndex = index;
        int parentIndex = (index - 1) / 2;

        if (_comparer.Compare(_arr[childIndex], _arr[parentIndex]) < 0)
        {
            // swap the parent and the child
            T temp = _arr[childIndex];
            _arr[childIndex] = _arr[parentIndex];
            _arr[parentIndex] = temp;

            MinHeapifyUp(parentIndex);
        }
    }

    private void MinHeapifyDown(int index)
    {
        int leftChildIndex = index * 2 + 1;
        int rightChildIndex = index * 2 + 2;
        int smallestItemIndex = index; // The index of the parent

        if (leftChildIndex <= _lastItemIndex &&
            _comparer.Compare(_arr[leftChildIndex], _arr[smallestItemIndex]) < 0)
        {
            smallestItemIndex = leftChildIndex;
        }

        if (rightChildIndex <= _lastItemIndex &&
            _comparer.Compare(_arr[rightChildIndex], _arr[smallestItemIndex]) < 0)
        {
            smallestItemIndex = rightChildIndex;
        }

        if (smallestItemIndex != index)
        {
            // swap the parent with the smallest of the child items
            T temp = _arr[index];
            _arr[index] = _arr[smallestItemIndex];
            _arr[smallestItemIndex] = temp;

            MinHeapifyDown(smallestItemIndex);
        }
    }

    private void Resize()
    {
        T[] newArr = new T[_arr.Length * 2];
        for (int i = 0; i < _arr.Length; i++)
        {
            newArr[i] = _arr[i];
        }

        _arr = newArr;
    }
}