using UnityEngine;

public static class Spawner
{
    public static void SpawnObject()
    {

    }
    public static Unit SpawnUnit(Game _game, Tile _tile, UnitType _unit_type, ClassType _class_type)
    {
        Unit_ScriptableObject _unit_data = MapController.Instance.GetUnitSciptableObject(_unit_type, _class_type);
        if (_unit_data is Figure_ScriptableObject _figure_data)
        {
            Figure figure = new Figure(_figure_data); 
            _game.map.PlaceObject(figure, _tile);
            _game.object_manager.AddObject(figure);
            return figure;
        }
        return null;
    }
   /* //SWORDSMAN
    public static Unit SpawnLightSwordsman(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Light Swordsman",
            game_object_path = "Figures/Swordsman/Light/swordsman",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 1,
            current_health = 1,

            movement_type = "normal_movement",
            movement_range = 1,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 1,

        };

        Figure light_swordsman = new Figure(ClassType.Light, UnitType.Swordsman, data);
        light_swordsman.AddBehaviour(MovementBehaviourFactory(light_swordsman, data.movement_type, data.movement_range));
        light_swordsman.AddBehaviour(AttackBehaviourFactory(light_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(light_swordsman, _tile);
        _game.object_manager.AddObject(light_swordsman);
        return light_swordsman;
    }
    public static Unit SpawnDarkSwordsman(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Dark Swordsman",
            game_object_path = "Figures/Swordsman/Dark/swordsman",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 1,
            current_health = 1,

            movement_type = "normal_movement",
            movement_range = 1,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 1,

        };

        Figure dark_swordsman = new Figure(ClassType.Dark, UnitType.Swordsman, data);
        dark_swordsman.AddBehaviour(MovementBehaviourFactory(dark_swordsman, data.movement_type, data.movement_range));
        dark_swordsman.AddBehaviour(AttackBehaviourFactory(dark_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(dark_swordsman, _tile);
        _game.object_manager.AddObject(dark_swordsman);
        return dark_swordsman;
    }

    //ARCHER
    public static Unit SpawnLightArcher(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Light Archer",
            game_object_path = "Figures/Archer/Light/archer",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 2,
            current_health = 2,

            movement_type = "normal_movement",
            movement_range = 1,

            attack_type = "ranged_attack",
            damage_type = "physical_damage",
            attack_range = 2,
            damage_amount = 1,

        };

        Figure light_swordsman = new Figure(ClassType.Light, UnitType.Archer, data);
        light_swordsman.AddBehaviour(MovementBehaviourFactory(light_swordsman, data.movement_type, data.movement_range));
        light_swordsman.AddBehaviour(AttackBehaviourFactory(light_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount, Resources.Load<GameObject>("Figures/Archer/Light/arrow/arrow")));
        _game.map.PlaceObject(light_swordsman, _tile);
        _game.object_manager.AddObject(light_swordsman);
        return light_swordsman;
    }
    public static Unit SpawnDarkArcher(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Dark Archer",
            game_object_path = "Figures/Archer/Dark/archer",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 2,
            current_health = 2,

            movement_type = "normal_movement",
            movement_range = 1,

            attack_type = "ranged_attack",
            damage_type = "physical_damage",
            attack_range = 2,
            damage_amount = 1,

        };

        Figure dark_swordsman = new Figure(ClassType.Dark, UnitType.Archer, data);
        dark_swordsman.AddBehaviour(MovementBehaviourFactory(dark_swordsman, data.movement_type, data.movement_range));
        dark_swordsman.AddBehaviour(AttackBehaviourFactory(dark_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount, Resources.Load<GameObject>("Figures/Archer/Dark/arrow/arrow")));
        _game.map.PlaceObject(dark_swordsman, _tile);
        _game.object_manager.AddObject(dark_swordsman);
        return dark_swordsman;
    }
    //Knight
    public static Unit SpawnLightKnight(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Light Knight",
            game_object_path = "Figures/Knight/Light/knight",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 2,
            current_health = 2,

            movement_type = "knight_movement",
            movement_range = 2,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 1,

        };

        Figure light_swordsman = new Figure(ClassType.Light, UnitType.Knight, data);
        light_swordsman.AddBehaviour(MovementBehaviourFactory(light_swordsman, data.movement_type, data.movement_range));
        light_swordsman.AddBehaviour(AttackBehaviourFactory(light_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(light_swordsman, _tile);
        _game.object_manager.AddObject(light_swordsman);
        return light_swordsman;
    }
    public static Unit SpawnDarkKnight(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Dark Knight",
            game_object_path = "Figures/Knight/Dark/knight",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 2,
            current_health = 2,

            movement_type = "knight_movement",
            movement_range = 2,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 1,

        };

        Figure dark_swordsman = new Figure(ClassType.Dark, UnitType.Knight, data);
        dark_swordsman.AddBehaviour(MovementBehaviourFactory(dark_swordsman, data.movement_type, data.movement_range));
        dark_swordsman.AddBehaviour(AttackBehaviourFactory(dark_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(dark_swordsman, _tile);
        _game.object_manager.AddObject(dark_swordsman);
        return dark_swordsman;
    }
    //Tank
    public static Unit SpawnLightTank(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Light Tank",
            game_object_path = "Figures/Tank/Light/tank",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 4,
            current_health = 4,

            movement_type = "directional_movement",
            movement_range = 2,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 1,

        };

        Figure light_swordsman = new Figure(ClassType.Light, UnitType.Tank, data);
        light_swordsman.AddBehaviour(MovementBehaviourFactory(light_swordsman, data.movement_type, data.movement_range));
        light_swordsman.AddBehaviour(AttackBehaviourFactory(light_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(light_swordsman, _tile);
        _game.object_manager.AddObject(light_swordsman);
        return light_swordsman;
    }
    public static Unit SpawnDarkTank(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Dark Tank",
            game_object_path = "Figures/Tank/Dark/tank",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 4,
            current_health = 4,

            movement_type = "directional_movement",
            movement_range = 2,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 1,

        };

        Figure dark_swordsman = new Figure(ClassType.Dark, UnitType.Tank, data);
        dark_swordsman.AddBehaviour(MovementBehaviourFactory(dark_swordsman, data.movement_type, data.movement_range));
        dark_swordsman.AddBehaviour(AttackBehaviourFactory(dark_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(dark_swordsman, _tile);
        _game.object_manager.AddObject(dark_swordsman);
        return dark_swordsman;
    }
    //Wizard
    public static Unit SpawnLightWizard(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Light Wizard",
            game_object_path = "Figures/Wizard/Light/wizard",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 3,
            current_health = 3,

            movement_type = "teleport_movement",
            movement_range = 2,

            attack_type = "",
            damage_type = "",
            attack_range = 0,
            damage_amount = 0,

        };

        Figure light_swordsman = new Figure(ClassType.Light, UnitType.Wizard, data);
        light_swordsman.AddBehaviour(MovementBehaviourFactory(light_swordsman, data.movement_type, data.movement_range));
        light_swordsman.AddBehaviour(AttackBehaviourFactory(light_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(light_swordsman, _tile);
        _game.object_manager.AddObject(light_swordsman);
        return light_swordsman;
    }
    public static Unit SpawnDarkWizard(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Dark Wizard",
            game_object_path = "Figures/Wizard/Dark/wizard",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 3,
            current_health = 3,

            movement_type = "teleport_movement",
            movement_range = 2,

            attack_type = "",
            damage_type = "",
            attack_range = 0,
            damage_amount = 0,

        };

        Figure dark_swordsman = new Figure(ClassType.Dark, UnitType.Wizard, data);
        dark_swordsman.AddBehaviour(MovementBehaviourFactory(dark_swordsman, data.movement_type, data.movement_range));
        dark_swordsman.AddBehaviour(AttackBehaviourFactory(dark_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(dark_swordsman, _tile);
        _game.object_manager.AddObject(dark_swordsman);
        return dark_swordsman;
    }
    //Jester
    public static Unit SpawnLightJester(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Light Jester",
            game_object_path = "Figures/Jester/Light/jester",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 2,
            current_health = 2,

            movement_type = "normal_movement",
            movement_range = 2,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 2,

        };

        Figure light_swordsman = new Figure(ClassType.Light, UnitType.Jester, data);
        light_swordsman.AddBehaviour(MovementBehaviourFactory(light_swordsman, data.movement_type, data.movement_range));
        light_swordsman.AddBehaviour(AttackBehaviourFactory(light_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(light_swordsman, _tile);
        _game.object_manager.AddObject(light_swordsman);
        return light_swordsman;
    }
    public static Unit SpawnDarkJester(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Dark Jester",
            game_object_path = "Figures/Jester/Dark/jester",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 2,
            current_health = 2,

            movement_type = "normal_movement",
            movement_range = 2,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 2,

        };

        Figure dark_swordsman = new Figure(ClassType.Dark, UnitType.Jester, data);
        dark_swordsman.AddBehaviour(MovementBehaviourFactory(dark_swordsman, data.movement_type, data.movement_range));
        dark_swordsman.AddBehaviour(AttackBehaviourFactory(dark_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(dark_swordsman, _tile);
        _game.object_manager.AddObject(dark_swordsman);
        return dark_swordsman;
    }
    //Jester
    public static Unit SpawnLightQueen(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Light Queen",
            game_object_path = "Figures/Queen/Light/queen",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 3,
            current_health = 3,

            movement_type = "directional_movement",
            movement_range = 3,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 3,

        };

        Figure light_swordsman = new Figure(ClassType.Light, UnitType.Queen, data);
        light_swordsman.AddBehaviour(MovementBehaviourFactory(light_swordsman, data.movement_type, data.movement_range));
        light_swordsman.AddBehaviour(AttackBehaviourFactory(light_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(light_swordsman, _tile);
        _game.object_manager.AddObject(light_swordsman);
        return light_swordsman;
    }
    public static Unit SpawnDarkQueen(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Dark Queen",
            game_object_path = "Figures/Queen/Dark/queen",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 3,
            current_health = 3,

            movement_type = "directional_movement",
            movement_range = 3,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 3,

        };

        Figure dark_swordsman = new Figure(ClassType.Dark, UnitType.Queen, data);
        dark_swordsman.AddBehaviour(MovementBehaviourFactory(dark_swordsman, data.movement_type, data.movement_range));
        dark_swordsman.AddBehaviour(AttackBehaviourFactory(dark_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(dark_swordsman, _tile);
        _game.object_manager.AddObject(dark_swordsman);
        return dark_swordsman;
    }
    //King
    public static Unit SpawnLightKing(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Light King",
            game_object_path = "Figures/King/Light/king",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 5,
            current_health = 5,

            movement_type = "normal_movement",
            movement_range = 1,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 1,

        };

        Figure light_swordsman = new Figure(ClassType.Light, UnitType.King, data);
        light_swordsman.AddBehaviour(MovementBehaviourFactory(light_swordsman, data.movement_type, data.movement_range));
        light_swordsman.AddBehaviour(AttackBehaviourFactory(light_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(light_swordsman, _tile);
        _game.object_manager.AddObject(light_swordsman);
        return light_swordsman;
    }
    public static Unit SpawnDarkKing(Game _game, Tile _tile)
    {
        FigureData data = new FigureData()
        {
            name = "Dark King",
            game_object_path = "Figures/King/Dark/king",
            sprite_path = "UI/Unit/King/Light/sprite",

            max_health = 5,
            current_health = 5,

            movement_type = "normal_movement",
            movement_range = 1,

            attack_type = "melee_attack",
            damage_type = "physical_damage",
            attack_range = 1,
            damage_amount = 1,

        };

        Figure dark_swordsman = new Figure(ClassType.Dark, UnitType.King, data);
        dark_swordsman.AddBehaviour(MovementBehaviourFactory(dark_swordsman, data.movement_type, data.movement_range));
        dark_swordsman.AddBehaviour(AttackBehaviourFactory(dark_swordsman, data.attack_type, data.damage_type, data.attack_range, data.damage_amount));
        _game.map.PlaceObject(dark_swordsman, _tile);
        _game.object_manager.AddObject(dark_swordsman);
        return dark_swordsman;
    }
    private static MovementBehaviour MovementBehaviourFactory(Unit _unit, string _movement_type, int _range)
    {
        MovementBehaviour movement_behaviour = null;

        switch (_movement_type)
        {
            case "normal_movement":
                //movement_behaviour = new NormalMovement(_unit, _range);
                break;
            case "directional_movement":
               // movement_behaviour = new DirectionMovement(_unit, _range);
                break;
            case "knight_movement":
               // movement_behaviour = new KnightMovement(_unit, _range);
                break;
            case "teleport_movement":
              // movement_behaviour = new TeleportMovement(_unit, _range);
                break;
            default:
                movement_behaviour = new NoMovement(_unit);
                break;
        }

        return movement_behaviour;
    }
    private static AttackBehaviour AttackBehaviourFactory(Unit _unit, string _attack_type, string _damage_type, int _range, int _amount, GameObject projectil_prefab = null)
    {
        AttackBehaviour attack_behaviour = null;
        Damage damage = null;
        switch (_damage_type)
        {
            case "physical_damage":
                damage = new PhysicalDamage(_unit, _amount);
                break;

            case "magic_damage":
                damage = new MagicDamage(_unit, _amount);
                break;

            default:
                break;
        }
        switch (_attack_type)
        {
            case "melee_attack":
                //attack_behaviour = new MeleeAttack(_unit, damage, _range, 0);
                break;

            case "ranged_attack":
                //attack_behaviour = new RangedAttack(_unit, damage, _range, projectil_prefab);
                break;

            default:
                attack_behaviour = new NoAttack(_unit);
                break;
        }
        return attack_behaviour;
    }*/

    public static void SpawnChallengeRoyaleObjects(Game _game)
    {/*
        //Light
        SpawnLightKing(_game, _game.map.GetTile(0, -4));
        SpawnLightQueen(_game, _game.map.GetTile(0, -3));

        SpawnLightSwordsman(_game, _game.map.GetTile(0, -2));
        SpawnLightSwordsman(_game, _game.map.GetTile(-2, -1));
        SpawnLightSwordsman(_game, _game.map.GetTile(2, -3));

        SpawnLightTank(_game, _game.map.GetTile(1, -4));
        SpawnLightTank(_game, _game.map.GetTile(-1, -3));

        SpawnLightArcher(_game, _game.map.GetTile(-3, -1));
        SpawnLightArcher(_game, _game.map.GetTile(3, -4));

        SpawnLightKnight(_game, _game.map.GetTile(-1, -2));
        SpawnLightKnight(_game, _game.map.GetTile(1, -3));

        SpawnLightJester(_game, _game.map.GetTile(-2, -2));
        SpawnLightWizard(_game, _game.map.GetTile(2, -4));


        //Dark
        SpawnDarkKing(_game, _game.map.GetTile(0, 4));
        SpawnDarkQueen(_game, _game.map.GetTile(0, 3));

        SpawnDarkSwordsman(_game, _game.map.GetTile(0, 2));
        SpawnDarkSwordsman(_game, _game.map.GetTile(2, 1));
        SpawnDarkSwordsman(_game, _game.map.GetTile(-2, 3));

        SpawnDarkTank(_game, _game.map.GetTile(1, 3));
        SpawnDarkTank(_game, _game.map.GetTile(-1, 4));

        SpawnDarkArcher(_game, _game.map.GetTile(3, 1));
        SpawnDarkArcher(_game, _game.map.GetTile(-3, 4));

        SpawnDarkKnight(_game, _game.map.GetTile(-1, 3));
        SpawnDarkKnight(_game, _game.map.GetTile(1, 2));

        SpawnDarkJester(_game, _game.map.GetTile(2, 2));
        SpawnDarkWizard(_game, _game.map.GetTile(-2, 4));
*/
        //light
        SpawnUnit(_game, _game.map.GetTile(0, -4), UnitType.King, ClassType.Light);
        SpawnUnit(_game, _game.map.GetTile(0, -3), UnitType.Queen, ClassType.Light);

        SpawnUnit(_game, _game.map.GetTile(0, -2), UnitType.Swordsman, ClassType.Light);
        SpawnUnit(_game, _game.map.GetTile(-2, -1), UnitType.Swordsman, ClassType.Light);
        SpawnUnit(_game, _game.map.GetTile(2, -3), UnitType.Swordsman, ClassType.Light);

        SpawnUnit(_game, _game.map.GetTile(-3, -1), UnitType.Archer, ClassType.Light);
        SpawnUnit(_game, _game.map.GetTile(3, -4), UnitType.Archer, ClassType.Light);

        SpawnUnit(_game, _game.map.GetTile(1, -4), UnitType.Tank, ClassType.Light);
        SpawnUnit(_game, _game.map.GetTile(-1, -3), UnitType.Tank, ClassType.Light);

        SpawnUnit(_game, _game.map.GetTile(-1, -2), UnitType.Knight, ClassType.Light);
        SpawnUnit(_game, _game.map.GetTile(1, -3), UnitType.Knight, ClassType.Light);

        SpawnUnit(_game, _game.map.GetTile(-2, -2), UnitType.Jester, ClassType.Light);
        SpawnUnit(_game, _game.map.GetTile(2, -4), UnitType.Wizard, ClassType.Light);

        // dark
        SpawnUnit(_game, _game.map.GetTile(0, 4), UnitType.King, ClassType.Dark);
        SpawnUnit(_game, _game.map.GetTile(0, 3), UnitType.Queen, ClassType.Dark);

        SpawnUnit(_game, _game.map.GetTile(0, 2), UnitType.Swordsman, ClassType.Dark);
        SpawnUnit(_game, _game.map.GetTile(2, 1), UnitType.Swordsman, ClassType.Dark);
        SpawnUnit(_game, _game.map.GetTile(-2, 3), UnitType.Swordsman, ClassType.Dark);

        SpawnUnit(_game, _game.map.GetTile(3, 1), UnitType.Archer, ClassType.Dark);
        SpawnUnit(_game, _game.map.GetTile(-3, 4), UnitType.Archer, ClassType.Dark);

        SpawnUnit(_game, _game.map.GetTile(-1, 4), UnitType.Tank, ClassType.Dark);
        SpawnUnit(_game, _game.map.GetTile(1, 3), UnitType.Tank, ClassType.Dark);

        SpawnUnit(_game, _game.map.GetTile(-1, 3), UnitType.Knight, ClassType.Dark);
        SpawnUnit(_game, _game.map.GetTile(1, 2), UnitType.Knight, ClassType.Dark);

        SpawnUnit(_game, _game.map.GetTile(2, 2), UnitType.Jester, ClassType.Dark);
        SpawnUnit(_game, _game.map.GetTile(-2, 4), UnitType.Wizard, ClassType.Dark);
    }
}
