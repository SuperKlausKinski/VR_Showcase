using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VRControlls.Navigation
{

    public class DistanceTrigger : MonoBehaviour
    {

        public float Radius;
        public bool TriggerOnlyOnce;
        public UnityEvent EventToTrigger;
        public UnityEvent CloseEvent;
        //-------------------------------------------------------------------------------------------------------------
        private Transform m_player;
        private bool m_triggered;
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            if (EventToTrigger == null)
                EventToTrigger = new UnityEvent();
            if (CloseEvent == null)
                CloseEvent = new UnityEvent();
            m_player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        //-------------------------------------------------------------------------------------------------------------
        void Update()
        {


            float _distance = (Vector3.Distance(m_player.position, transform.position));

            if (_distance < Radius && !m_triggered)
            {
                m_triggered = true;
                this.enabled = !TriggerOnlyOnce;
                EventToTrigger.Invoke();
            }
            else if (_distance > Radius + 5)
            {
                m_triggered = false;
            }
        }
    }
}