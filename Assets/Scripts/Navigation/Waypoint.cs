using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace VRControlls.Navigation
{
    [RequireComponent(typeof(EventTrigger))]
    [RequireComponent(typeof(Collider))]
    public class Waypoint : MonoBehaviour
    {
        public enum WAYPOINTSTATE { IDLE, INACTIVE, OCCUPIED }
        public WAYPOINTSTATE WaypointState { get { return m_wayPointState; } set { changeState(value); } }
      

        [Header("Optional sub gameobjects root")]
        public GameObject WaypointRoot;
        [Header("Broadcast Messages/Trigger to children")]

        public string OnEnter;
        public string OnClick;
        public string OnExit;

        [Header("Optional Events")]
        public UnityEvent OnOccupiedEvent;
        public UnityEvent OnLeaveEvent;

        private WAYPOINTSTATE m_wayPointState;
        private GameObject m_player;
        //-------------------------------------------------------------------------------------------------------------
        private void changeState(WAYPOINTSTATE _state)
        {
           
            m_wayPointState = _state;
            switch (_state)
            {
                case (WAYPOINTSTATE.OCCUPIED):
                    HideWaypoint();
                    break;
                case (WAYPOINTSTATE.IDLE):
                    if(WaypointRoot!=null)ActivateWayPointAsset();
                    ShowWaypoint();
                    break;
                case (WAYPOINTSTATE.INACTIVE):
                    if (WaypointRoot != null) DeactivateWayPointAsset();
                    break;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            WaypointState = WAYPOINTSTATE.INACTIVE;
            m_player = GameObject.FindGameObjectWithTag("Player");
            AddEventListeners();
        }//-------------------------------------------------------------------------------------------------------------
        private void ActivateWayPointAsset()
        {
       
                BroadcastMessage("EventFromParent", "ACTIVATE", SendMessageOptions.DontRequireReceiver);
             
        }
        //-------------------------------------------------------------------------------------------------------------
        private void DeactivateWayPointAsset()
        {
      
                BroadcastMessage("EventFromParent", "DEACTIVATE", SendMessageOptions.DontRequireReceiver);
                   
        }
        //-------------------------------------------------------------------------------------------------------------
        private void AddEventListeners()
        {

            // add event listener
            EventTrigger _trigger = GetComponentInParent<EventTrigger>();
            //on enter event
            EventTrigger.Entry _onEnterEntry = new EventTrigger.Entry();
            _onEnterEntry.eventID = EventTriggerType.PointerEnter;
            _onEnterEntry.callback.AddListener((eventData) => { OnPointerEnter(eventData); });
            GetComponent<EventTrigger>().triggers.Add(_onEnterEntry);
            //on exit event
            EventTrigger.Entry _onExitEntry = new EventTrigger.Entry();
            _onExitEntry.eventID = EventTriggerType.PointerExit;
            _onExitEntry.callback.AddListener((eventData) => { OnPointerExit(eventData); });
            GetComponent<EventTrigger>().triggers.Add(_onExitEntry);
            //click event
            EventTrigger.Entry _onClickEntry = new EventTrigger.Entry();
            _onClickEntry.eventID = EventTriggerType.PointerClick;
            _onClickEntry.callback.AddListener((eventData) => { OnPointerClick(eventData); });
            GetComponent<EventTrigger>().triggers.Add(_onClickEntry);

        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnWaypointLeave()
        {
            WaypointState = WAYPOINTSTATE.IDLE;
            OnLeaveEvent.Invoke();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnPointerEnter(BaseEventData _eventData)
        {
            Debug.Log("enter!");
            if (OnEnter != null) BroadcastMessage("EventFromParent", OnEnter, SendMessageOptions.DontRequireReceiver);
        }
        public void OnPointerExit(BaseEventData _eventData)
        {
            if (OnExit != null) BroadcastMessage("EventFromParent", OnExit, SendMessageOptions.DontRequireReceiver);
        }

        public void OnPointerClick(BaseEventData _eventData)
        {
            if (m_player.GetComponent<WaypointNavigation>().MoveTo(this, gameObject.transform.position))
            {
                if (OnEnter != null) BroadcastMessage("EventFromParent", OnExit, SendMessageOptions.DontRequireReceiver);
                OnOccupy();
            }

        }
        //-------------------------------------------------------------------------------------------------------------
        private void HideWaypoint()
        {
            if (WaypointRoot) { WaypointRoot.SetActive(false); }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            gameObject.GetComponent<Collider>().enabled = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        private void ShowWaypoint()
        {
            if (WaypointRoot)
            {           
                WaypointRoot.SetActive(true);             
                BroadcastMessage("EventFromParent", "ACTIVATE", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            gameObject.GetComponent<Collider>().enabled = true;
        }
        //-------------------------------------------------------------------------------------------------------------
        private void OnOccupy()
        {
            WaypointState = WAYPOINTSTATE.OCCUPIED;
            OnOccupiedEvent.Invoke();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void ToggleChildrenVisibity(bool _visible)
        {

        }

    }
}