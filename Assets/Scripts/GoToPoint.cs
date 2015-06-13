using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using Assets.Scripts;

public class GoToPoint : MonoBehaviour
{
    public float Speed = 1;
    public float MaxDistanceToGoal = 0.1f;

    private Vector3 currentPoint;
    public bool started = false;

    
    public void start(Vector3 point)
    {
        Debug.Log("goto " + point.ToString());
        currentPoint = point;
        started = true;
    }

    public void Update()
    {
        if (started)
        {
            if (currentPoint == null)
                return;

                transform.position = Vector3.MoveTowards(transform.position, currentPoint, Time.deltaTime * Speed);

            var distanceSquared = (transform.position - currentPoint).magnitude;
            if (distanceSquared < MaxDistanceToGoal)
            {
                Debug.Log("stopped at " + transform.position.ToString());
                started = false;
                FightManager.Instance.incrementState();
            }
        }
    }

    public bool isFinished()
    {
        return !started;
    }
}