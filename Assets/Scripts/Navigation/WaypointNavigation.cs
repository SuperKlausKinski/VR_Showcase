using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gvr;

namespace VRControlls.Navigation
{
    public class WaypointNavigation : MonoBehaviour
    {

        public GvrAudioSource AudioSource;
        public AudioClip AudioClip;

        private Camera m_mainCamera;
        private Waypoint m_currentWaypoint;
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            m_mainCamera = Camera.main;
        }
        //-------------------------------------------------------------------------------------------------------------
        public bool MoveTo(Waypoint _wayPoint,Vector3 _endPos, Vector3 _rotation = default(Vector3))
        {
            if (m_currentWaypoint != _wayPoint &&m_currentWaypoint) { m_currentWaypoint.OnWaypointLeave(); }
            m_currentWaypoint = _wayPoint;
            // TODO sometimes there might be a reason to prohibit movement
            bool _canMove = true;
            MoveTransform(_endPos);
            if (AudioSource) { AudioSource.PlayOneShot(AudioClip); }
            return _canMove;
        }
        //-------------------------------------------------------------------------------------------------------------
        private void MoveTransform(Vector3 _endPos)
        {
            gameObject.transform.position = _endPos;
        }

    }

}
