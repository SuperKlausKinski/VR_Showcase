using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRControlls.Templates;

namespace VRControlls.Navigation
{
    public class Waypoints : Singleton<Waypoints>
    {
        private Waypoint[] m_waypoints;

        void Start()
        {
     
            m_waypoints = GetComponentsInChildren<Waypoint>();

        }

        public bool WaypointsEnabled
        {
            set
            {
                foreach (Waypoint _c in m_waypoints)
                {
                    Debug.Log(_c.name);
                    _c.gameObject.GetComponent<Collider>().enabled = value;
                }
            }
        }
    }
}

