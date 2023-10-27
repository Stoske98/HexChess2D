using System.Collections.Generic;
using System.Linq;

public class ObjectManager
{
    public List<IObject> objects { set; get; }
    private List<IObject> objects_to_add { set; get; }
    private List<IObject> objects_to_remove { set; get; }

    public ObjectManager()
    {
        objects = new List<IObject>();
        objects_to_add = new List<IObject>();
        objects_to_remove = new List<IObject>();
    }
    public void Update()
    {
        UpdateObjects();

        ProcessPendingActions();
    }
    public void AddObject(IObject obj)
    {
        objects_to_add.Add(obj);
    }
    public void RemoveObject(IObject obj)
    {
        obj.Dispose();
        objects_to_remove.Add(obj);
    }

    private void UpdateObjects()
    {
        foreach (var obj in objects.OfType<Unit>())
            obj.Update();
    }

    public void ProcessPendingActions()
    {
        if (objects_to_add.Count > 0)
        {
            objects.AddRange(objects_to_add);
            objects_to_add.Clear();
        }

        if (objects_to_remove.Count > 0)
        {
            objects.RemoveAll(obj => objects_to_remove.Contains(obj));
            objects_to_remove.Clear();
        }
    }
}
