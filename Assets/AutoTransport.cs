using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoTransport : MonoBehaviour
{
    public enum AUTOTRANSPORTSTATE { INACTIVE,MOVING}
    public AUTOTRANSPORTSTATE AutoTransportState { get; private set; }

    public Transform[] wayPointList;
    private List<Transform> m_wayPointList;

    private int currentWayPoint = 0;
    

    public float Speed = 4f;
    private Transform targetWayPoint;
 
    void Start()
    {
        AutoTransportState = AUTOTRANSPORTSTATE.INACTIVE;
    }

    public void MoveAlongWayPoints(int _index)
    {
        if (AutoTransportState == AUTOTRANSPORTSTATE.MOVING) return;
        currentWayPoint = 0;      
        m_wayPointList = new List<Transform>();
        foreach (Transform _waypoint in wayPointList[_index].transform)
        {
            m_wayPointList.Add(_waypoint);
            Debug.Log(_waypoint.localPosition);
            Debug.Log(_waypoint.position);
        }
        AutoTransportState = AUTOTRANSPORTSTATE.MOVING;

        walk(m_wayPointList[currentWayPoint], Speed);
    }


    void walk(Transform _target, float _speed)
    {
        float _distance = Vector3.Distance(transform.position, _target.position);
        if (Vector3.Distance(transform.position, _target.position) < 0.25)
        {
           
        }
        transform.DOMove(_target.position, _distance / _speed).OnComplete(()=>NextPoint());

        
    }
    private void NextPoint()
    {
        Debug.Log("walk!");
        currentWayPoint++;
        if (currentWayPoint <= this.m_wayPointList.Count-1)
        {
            targetWayPoint = m_wayPointList[currentWayPoint];
            walk(m_wayPointList[currentWayPoint], Speed);
            
        }
        else
        {
            AutoTransportState = AUTOTRANSPORTSTATE.INACTIVE;
        }
    }


}
