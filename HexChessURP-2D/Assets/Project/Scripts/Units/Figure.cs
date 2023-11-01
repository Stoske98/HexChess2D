using System;
using UnityEditor;
using UnityEngine;

public class Figure : Unit, IDamagable
{
    public int max_health { get; set; }
    public int current_health { get; set; }
    public Figure(ClassType _class_type, UnitType _unit_type, FigureData _data) : base(_class_type, _unit_type, _data)
    {
        max_health = _data.max_health;
        current_health = _data.current_health;

        game_object.GetComponentInChildren<HealthAndDamage>().SetHealthTxt(current_health);
    }

    public Figure(Figure_ScriptableObject _figure_data) : base(_figure_data) 
    {
        max_health = _figure_data.BaseHealth;
        current_health = max_health;

        game_object.GetComponentInChildren<Trail>().CreateTrail(_figure_data.TrailSprite);
        game_object.GetComponentInChildren<HealthAndDamage>().SetHealthTxt(current_health);
    }

    public void Die()
    {
        Tile _unit_tile = MapController.Instance.game.map.GetTile(this);
        _unit_tile.RemoveObject(this);

        game_object.GetComponentInChildren<Canvas>().sortingOrder = 9;
        SpriteRenderer icon_sprite_renderer = game_object.GetComponentInChildren<SpriteRenderer>();
        icon_sprite_renderer.material = new Material(MapController.Instance.dissolve_mat);
        LeanTween.value(icon_sprite_renderer.material.GetFloat("_DissolveAmount"), 1.1f, 1f)
        .setOnUpdate((float value) =>
        {
            icon_sprite_renderer.material.SetFloat("_DissolveAmount", value);
        }).setOnComplete(RemoveStats());

        Dispose();

    }
    private Action RemoveStats()
    {
        return () =>
        {
            game_object.GetComponentInChildren<HealthAndDamage>().gameObject.SetActive(false);
        };
    }
    public void ReceiveDamage(Damage damage)
    {
        LeanTween.color(game_object.transform.GetChild(0).gameObject, Color.red, 0.1f).setLoopPingPong(1);
        current_health -= damage.amount;
        if(current_health <= 0)
        {
            game_object.GetComponentInChildren<HealthAndDamage>().SetHealthTxt(0);
            Die();
        }
        else
        {
            game_object.GetComponentInChildren<HealthAndDamage>().SetHealthTxt(current_health);
        }
    }
}
