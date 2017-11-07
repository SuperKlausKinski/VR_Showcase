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

        void Awake()
        {
            base.Awake();
            m_waypoints = GetComponentsInChildren<Waypoint>();

        }

        public bool WaypointsEnabled
        {
            set
            {
                foreach (Waypoint _c in m_waypoints)
                {
                    if (_c.WaypointState == Waypoint.WAYPOINTSTATE.OCCUPIED)
                        continue;
                    _c.gameObject.GetComponent<Collider>().enabled = value;
                    if (value)
                    {
                        _c.WaypointState = Waypoint.WAYPOINTSTATE.IDLE;
                    }
                    else
                    {
                       
                        _c.WaypointState = Waypoint.WAYPOINTSTATE.INACTIVE;
                    }
                    
                }
            }
        }
    }
}

