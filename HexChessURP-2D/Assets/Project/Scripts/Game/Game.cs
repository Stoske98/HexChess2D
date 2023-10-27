public class Game
{
    public Map map;
    public ClassType class_on_turn;
    public int move;
    public ObjectManager object_manager;

    public Game(Map _map)
    {
        map = _map;
        move = 1;
        class_on_turn = ClassType.Light;
        object_manager = new ObjectManager();
        Spawner.SpawnChallengeRoyaleObjects(this);
    }

}
