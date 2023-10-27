using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public GameObject trail_prefab;
    private GameObject trail_parent;

    public float spawn_distance;
    public int max_trails;
    public float alpha_start = 0.5f;
    public float time_to_disapear = 0.5f;
    public int sortOrder = 2;

    private bool spawn_trail = false;
    private Vector3 start_position;
    private Queue<GameObject> trail_queue = new Queue<GameObject>();

    public void Start()
    {
        trail_parent = new GameObject(gameObject.name + " trail container"); InitalizeTrailObjectPool(); StartCreatingTrails();
        trail_parent.transform.SetParent(MapController.Instance.trails_containter.transform);
    }
    private void InitalizeTrailObjectPool()
    {
        for (int i = 0; i < max_trails; i++)
        {
            GameObject trail = InstantiateTrail();
            trail.GetComponent<SpriteRenderer>().sortingOrder = sortOrder;
            trail_queue.Enqueue(trail);
            trail.transform.SetParent(trail_parent.transform);
            ReturnTrailToPool(trail);
        }
    }
    private GameObject InstantiateTrail()
    {
        GameObject trail = Instantiate(trail_prefab);
        return trail;
    }

    public void ReturnTrailToPool(GameObject trail)
    {
        trail.SetActive(false);
    }

    public GameObject GetTrailFromPool(Vector3 position)
    {
        GameObject trail = trail_queue.Dequeue();
        trail.transform.position = position;
        trail.SetActive(true);
        trail.LeanCancel();
        trail.gameObject.LeanAlpha(alpha_start, 0);
        trail.gameObject.LeanAlpha(0, 0.5f);
        trail_queue.Enqueue(trail);
        return trail;
    }

    private void Update()
    {
        if(spawn_trail)
        {
            if((transform.position - start_position).magnitude > spawn_distance)
            {
                GetTrailFromPool(transform.position);
                start_position = transform.position;
            }
        }
    }

    public void StartCreatingTrails()
    {
        start_position = transform.position;
        spawn_trail = true;
    }







}
