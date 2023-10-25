using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boid : MonoBehaviour
{
    private Neighborhood neighborhood;
    private Rigidbody rigid;

    // Use this for initialization
    void Awake()
    {
        neighborhood = GetComponent<Neighborhood>();
        rigid = GetComponent<Rigidbody>();
        // Set a random initial velocity
        vel = Random.onUnitSphere * Spawner.SETTINGS.velocity;                  // b
        
        LookAhead();                                                            // c
        Colorize();
    }

    // FixedUpdate is called once per physics update (i.e., 50x/second)
    void FixedUpdate()
    {                                                     // c
        BoidSettings bSet = Spawner.SETTINGS;                                 // d

        // sumVel sums all the influences acting to change the Boid’s direction
        Vector3 sumVel = Vector3.zero;

        // ____ATTRACTOR____ - Move towards or away from the Attractor
        Vector3 delta = Attractor.POS - pos;                                  // e
        // Check whether we’re attracted or avoiding the Attractor
        if (delta.magnitude > bSet.attractPushDist)
        {
            sumVel += delta.normalized * bSet.attractPull;                    // f
        }
        else
        {
            sumVel -= delta.normalized * bSet.attractPush;
        }

        // ____COLLISION_AVOIDANCE____ – Avoid neighbors who are too near
        Vector3 velAvoid = Vector3.zero;
        Vector3 tooNearPos = neighborhood.avgNearPos;
        // If the response is Vector3.zero, then no need to react
        if (tooNearPos != Vector3.zero)
        {
            velAvoid = pos - tooNearPos;
            velAvoid.Normalize();
            sumVel += velAvoid * bSet.nearAvoid;
        }

        // ____VELOCITY_MATCHING____ – Try to match velocity with neighbors
        Vector3 velAlign = neighborhood.avgVel;
        // Only do more if the velAlign is not Vector3.zero
        if (velAlign != Vector3.zero)
        {
            // We’re really interested in direction, so normalize the velocity
            velAlign.Normalize();
            // and then set it to the speed we chose
            sumVel += velAlign * bSet.velMatching;
        }

        // ____FLOCK_CENTERING____ – Move towards the center of local neighbors
        Vector3 velCenter = neighborhood.avgPos;
        if (velCenter != Vector3.zero)
        {
            velCenter -= transform.position;
            velCenter.Normalize();
            sumVel += velCenter * bSet.flockCentering;
        }

        // ____INTERPOLATE VELOCITY____ - Between normalized vel & sumVel
        sumVel.Normalize();
        vel = Vector3.Lerp(vel.normalized, sumVel, bSet.velocityEasing);
        // Set the magnitude of vel to the velocity set on Spawner.SETTINGS
        vel *= bSet.velocity;

        // In Code Listing 27.8, we’ll add additional influences to sumVel here

        // ____INTERPOLATE VELOCITY____ - Between normalized vel & sumVel
        sumVel.Normalize();                                                   // g
        vel = Vector3.Lerp(vel.normalized, sumVel, bSet.velocityEasing);      // h
        // Set the magnitude of vel to the velocity set on Spawner.SETTINGS
        vel *= bSet.velocity;                                                 // i
        
        // Look in the direction of the new velocity
        LookAhead();
    }

    // Orients the Boid to look at the direction it’s flying
    void LookAhead()
    {                                                          // c
        transform.LookAt(pos + rigid.velocity);
    }

    // Give the Boid a random color, but make sure it’s not too dark
    void Colorize()
    {                                                           // d
        // Choose a random color
        Color randColor = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1);            // e
        
        // Assign the color to the Renderers of both the Fuselage and Wing
        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();      // f
        foreach (Renderer r in rends)
        {
            r.material.color = randColor;
        }
        
        // Also assign the color to the TrailRenderer 
        TrailRenderer trend = GetComponent<TrailRenderer>();                    // g
        trend.startColor = randColor;
        randColor.a = 0;
        trend.endColor = randColor;
        trend.endWidth = 0;
    }

    // Property used to easily get and set the position of this Boid
    public Vector3 pos
    {                                                        // h
        get { return transform.position; }
        private set { transform.position = value; }

    }

    // Property used to easily get and set the velocity of this Boid
    public Vector3 vel
    {                                                        // h
        get { return rigid.velocity; }
        private set { rigid.velocity = value; }
    }

}
