using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    #region MapController Singleton
    private static MapController _instance;

    public static MapController Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    #endregion

    public Pallete pallete;
    public GameObject hexagon_prefab;
    public Game game;
    public Transform main_tiles_containter;
    public Transform objects_containter;
    public Transform trails_containter;
    public Material dissolve_mat;
    public List<Unit_ScriptableObject> units_so;
   /* public Material default_mat;
    public Material evil_mat;*/
    void Start()
    {
        Application.targetFrameRate = 60;
        Map map = new HexagonMap(4, 4, 1.05f, 0.5f, pallete);
        game = new Game(map);
    }

    private void Update()
    {
        game.object_manager.Update();
    }
    public void MarkAvailableMoves(MovementBehaviour _movement, Tile _unit_tile)
    {
        foreach (Tile tile in _movement.GetAvailableMoves(_unit_tile))
        {
            tile.game_object.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        //tile.SetColor(Color.cyan);
    }
    public void MarkAttackMoves(AttackBehaviour _attack, Tile _unit_tile)
    {
        foreach (Tile tile in _attack.GetAttackMoves(_unit_tile))
            tile.SetColor(Color.red);
    }
    public void MarkSelectedTile(Tile _selected_tile)
    {
        _selected_tile.SetColor(Color.green);
    }
    public void MarkMovementAndAttackFields(Unit _selected_unit, Tile _selected_tile)
    {
        //if (!Stun.IsStuned(selected_unit) && selected_unit.class_type == game.class_on_turn)
        {
            MovementBehaviour movement_behaviour = _selected_unit.GetBehaviour<MovementBehaviour>();
            AttackBehaviour attack_behaviour = _selected_unit.GetBehaviour<AttackBehaviour>();

            if (movement_behaviour != null)
                MarkAvailableMoves(movement_behaviour, _selected_tile);
            if (/*!Disarm.IsDissarmed(selected_unit) &&*/ attack_behaviour != null)
                MarkAttackMoves(attack_behaviour, _selected_tile);
        }
    }
    public void ResetFields()
    {
        foreach (Tile tile in game.map.tiles)
            tile.ResetColor();
    }
    public MapPallete GetPallete(Pallete _map_pallete)
    {
        switch (_map_pallete)
        {
            case Pallete.BLACK:
                return new MapPallete("#f7f8f9", "#9b9da8", "#1c1d23");
            case Pallete.BLUE:
                return new MapPallete("#4CC9F0", "#4895EF", "#4361EE");
            case Pallete.GREEN:
                return new MapPallete("#eeeed2", "#baca44", "#769656");
            case Pallete.RED:
                return new MapPallete("#FFFFFF", "#964d22", "#964d37");
            default:
                return null;
        }
    }

    public Unit_ScriptableObject GetUnitSciptableObject(UnitType _unit_type, ClassType _class_type)
    {
        foreach (Unit_ScriptableObject unit_so in units_so)
        {
            if (unit_so.ClassType == _class_type && unit_so.UnitType == _unit_type)
                return unit_so;
        }

        return null;
    }
}
