using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInputHandler : MonoBehaviour
{
    public Camera cam;
    private Vector2 hover_position;
    public PlayerInput player_input_controller;
    public MapController map_controller;
    private Tile hovered_tile;
    private Tile selected_tile;
    private Unit selected_unit;

    private bool is_draging;
    private Unit drag_unit;

    public void SwitchActionMap(string new_action_map)
    {
        // Debug.Log("Current map DISABLE: " + player_input_controller.currentActionMap.name);
        player_input_controller.currentActionMap?.Disable();
        player_input_controller.currentActionMap = player_input_controller.actions.FindActionMap(new_action_map);
        player_input_controller.currentActionMap?.Enable();
        //  Debug.Log("Current map ENABLE: " + player_input_controller.currentActionMap.name);
    }

    public void OnMouseScreenPosition(InputAction.CallbackContext value)
    {
        hover_position = cam.ScreenToWorldPoint(value.ReadValue<Vector2>());
        Tile tile = map_controller.game.map.OnHoverMapGetTile(hover_position);
        if (tile != null && tile != hovered_tile)
        {
            hovered_tile = tile;
        }
    }
    public void OnSelectBasicHandler(InputAction.CallbackContext value)
    {
        if (value.started)
        {
           // if (!EventSystem.current.IsPointerOverGameObject())
            {
                Tile _tile = hovered_tile;
                if (_tile != null)
                {
                    if (TryToMoveOrAttack(_tile))
                        return;

                    Unit _unit = _tile.GetObjectOfType<Unit>();
                    if (_unit != null)
                    {
                        map_controller.ResetFields();
                        SelectUnit(_unit, _tile);
                    }
                }
            }
        }
    }

    public void OnDeselectBasicHandler(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            selected_tile = null;
            selected_unit = null;
            map_controller.ResetFields();
        }
    }
    public void Drag(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (selected_unit != null && selected_tile == hovered_tile)
            {
                selected_unit.game_object.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
                is_draging = true;
                drag_unit = selected_unit;
                //selected_unit.game_object.GetComponent<SpriteRenderer>().material = map_controller.evil_mat;

                StartCoroutine(DragUpdate(selected_unit));
            }
        }

        if (value.canceled)
        {
            if (is_draging)
            {
                drag_unit.game_object.transform.position = selected_tile.game_object.transform.position;
                drag_unit.game_object.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;

                if (hovered_tile != null)
                    TryToMoveOrAttack(hovered_tile);

                //drag_unit.game_object.GetComponent<SpriteRenderer>().material = map_controller.default_mat;
                drag_unit = null;
                is_draging = false;

            }
        }
    }

    public bool TryToMoveOrAttack(Tile _tile)
    {
        if (selected_unit != null)
        {
            MovementBehaviour _movement_behaviour = selected_unit.GetBehaviour<MovementBehaviour>();
            if (_movement_behaviour != null && _movement_behaviour.GetType() != typeof(NoMovement) && _movement_behaviour.GetAvailableMoves(map_controller.game.map.GetTile(selected_unit)).Contains(_tile))
            {
                selected_unit.Move(map_controller.game.map.GetTile(selected_unit), _tile);
                map_controller.ResetFields();
                selected_unit = null;
                selected_tile = null;
                return true;
            }
            AttackBehaviour _attack_behaviour = selected_unit.GetBehaviour<AttackBehaviour>();
            if (_attack_behaviour != null && _attack_behaviour.GetType() != typeof(NoAttack) && _attack_behaviour.GetAttackMoves(map_controller.game.map.GetTile(selected_unit)).Contains(_tile))
            {
                Unit _target = _tile.GetObjectOfType<Unit>();
                if (_target != null && _target is IDamagable damagable)
                {
                    selected_unit.Attack(damagable);
                    map_controller.ResetFields();
                    selected_unit = null;
                    selected_tile = null;
                    return true;
                }
            }

        }
        return false;
    }
    public void SelectUnit(Unit _unit, Tile _tile)
    {
        selected_unit = _unit;
        selected_tile = _tile;
        map_controller.MarkSelectedTile(selected_tile);
        map_controller.MarkMovementAndAttackFields(selected_unit, hovered_tile);

    }
    private IEnumerator DragUpdate(IObject _obj)
    {
        while (is_draging)
        {
            _obj.game_object.transform.position = hover_position;
            yield return null;
        }
    }
}
