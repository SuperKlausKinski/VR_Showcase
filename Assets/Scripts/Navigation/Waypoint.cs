using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VRControlls.Navigation
{
    [RequireComponent(typeof(EventTrigger))]
    [RequireComponent(typeof(Collider))]
    public class Waypoint : MonoBehaviour
    {
        public enum WAYPOINTSTATE { IDLE, TRANSITIONING, OCCUPIED }
        public WAYPOINTSTATE WaypointState { get { return m_wayPointState; } set { changeState(value); } }

        [Header("Settings")]
        public bool DebugMode;

        private WAYPOINTSTATE m_wayPointState;
        private GameObject m_player;

        private void changeState(WAYPOINTSTATE _state)
        {
            m_wayPointState = _state;
            switch (_state)
            {
                case (WAYPOINTSTATE.OCCUPIED):
                    HideWaypoint();
                    break;
                case (WAYPOINTSTATE.IDLE):
                    ShowWaypoint();
                    break;
                case (WAYPOINTSTATE.TRANSITIONING):
                    break;
            }
        }

        void Awake()
        {
            WaypointState = WAYPOINTSTATE.IDLE;
            m_player = GameObject.FindGameObjectWithTag("Player");
            if (DebugMode) if (!m_player) Debug.Log("No Player found!"); if(!m_player.GetComponent<WaypointNavigation>())Debug.Log("No waypoint nav on Player!");
            AddEventListeners();
        }

        private void AddEventListeners()
        {
            
            // add event listener
            EventTrigger _trigger = GetComponentInParent<EventTrigger>();
            EventTrigger.Entry _entry = new EventTrigger.Entry();
            _entry.eventID = EventTriggerType.PointerClick;
            _entry.callback.AddListener((eventData) => { OnPointerClick(eventData); });
            GetComponent<EventTrigger>().triggers.Add(_entry);
 
        }
        public void OnWaypointLeave()
        {
            Debug.Log("yo show!");
            WaypointState = WAYPOINTSTATE.IDLE;
        }

        public void OnPointerClick(BaseEventData _eventData)
        {
            if (m_player.GetComponent<WaypointNavigation>().MoveTo(this,gameObject.transform.position))
            {
                OnOccupy();
            }
            if (DebugMode)Debug.Log(string.Format("Clicked {0}!", gameObject.name));
        }

        private void HideWaypoint()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
        private void ShowWaypoint()
        {
            Debug.Log("yo show!");
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
        }

        private void OnOccupy()
        {
            WaypointState = WAYPOINTSTATE.OCCUPIED;
        }

    }
}