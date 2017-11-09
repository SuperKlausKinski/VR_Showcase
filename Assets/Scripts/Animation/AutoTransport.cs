using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

namespace VRControlls.Navigation
{


    public class AutoTransport : MonoBehaviour
    {
        public enum AUTOTRANSPORTSTATE { INACTIVE, MOVING }
        public AUTOTRANSPORTSTATE AutoTransportState { get; private set; }
        //-------------------------------------------------------------------------------------------------------
        public Transform[] wayPointList;
        public float Speed = 4f;
        //-------------------------------------------------------------------------------------------------------
        private List<Transform> m_wayPointList;
        private int currentWayPoint = 0;
        private Transform targetWayPoint;
        private Action m_callBack;
        //-------------------------------------------------------------------------------------------------------
        void Start()
        {
            AutoTransportState = AUTOTRANSPORTSTATE.INACTIVE;
        }
        //-------------------------------------------------------------------------------------------------------
        public void MoveAlongWayPoints(int _index, Action _callBack = null)
        {
            if (AutoTransportState == AUTOTRANSPORTSTATE.MOVING) return;
            m_callBack = _callBack;
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
        //-------------------------------------------------------------------------------------------------------
        private void walk(Transform _target, float _speed)
        {
            float _distance = Vector3.Distance(transform.position, _target.position);
            // todo should work with dotween path too
            transform.DOMove(_target.position, _distance / _speed).OnComplete(() => NextPoint());

        }
        //-------------------------------------------------------------------------------------------------------
        private void NextPoint()
        {

            currentWayPoint++;
            if (currentWayPoint <= this.m_wayPointList.Count - 1)
            {
                targetWayPoint = m_wayPointList[currentWayPoint];
                walk(m_wayPointList[currentWayPoint], Speed);

            }
            else
            {
                AutoTransportState = AUTOTRANSPORTSTATE.INACTIVE;
                m_callBack.Invoke();
            }
        }


    }
}