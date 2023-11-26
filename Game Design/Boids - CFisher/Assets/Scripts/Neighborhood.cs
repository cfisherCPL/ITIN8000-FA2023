using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    [Header("Dynamic")]
    public List<Boid> neighbors;
    private SphereCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        neighbors = new List<Boid>();
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.SETTINGS.neighborDist / 2;
    }

    void FixedUpdate()
    {                                                 // b
        float nearRadius = Spawner.SETTINGS.neighborDist * 0.5f;
        if (!Mathf.Approximately(coll.radius, nearRadius))
        {
            coll.radius = nearRadius;
        }
    }

    void OnTriggerEnter(Collider other)
    {                                // c
        Boid b = other.GetComponent<Boid>();
        if (b != null)
        {
            if (!neighbors.Contains(b))
            {
                neighbors.Add(b);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {                                 // d
        Boid b = other.GetComponent<Boid>();
        if (b != null)
        {
            neighbors.Remove(b);
        }
    }

    public Vector3 avgPos
    {                                              // e
        get
        {
            Vector3 avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;
            for (int i = 0; i < neighbors.Count; i++)
            {
                avg += neighbors[i].pos;
            }
            avg /= neighbors.Count;
            return avg;

        }
    }

    public Vector3 avgVel
    {                                              // f
        get
        {
            Vector3 avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;
            for (int i = 0; i < neighbors.Count; i++)
            {
                avg += neighbors[i].vel;
            }
            avg /= neighbors.Count;
            return avg;
            }
    }

    public Vector3 avgNearPos
    {                                          // g
        get
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;
            for (int i = 0; i < neighbors.Count; i++)
            {
                delta = neighbors[i].pos - transform.position;
                if (delta.magnitude <= Spawner.SETTINGS.nearDist)
                {
                    avg += neighbors[i].pos;
                    nearCount++;
                }
            }
        // If there were no neighbors too close, return Vector3.zero
         if (nearCount == 0) return Vector3.zero;
    
        // Otherwise, average their locations
        avg /= nearCount;
        return avg;
        }
    }
}
