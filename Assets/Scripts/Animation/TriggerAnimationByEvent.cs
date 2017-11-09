using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRControlls.Animation
{
    [RequireComponent(typeof(Animator))]
    public class TriggerAnimationByEvent : MonoBehaviour
    {
        [TextArea]
        public string Description = "Parent broadcasts a message to call EventFromParent, along with a trigger name.Requires an animator";
        //-------------------------------------------------------------------------------------------------------------
        private Animator m_animator;
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            m_animator = GetComponent<Animator>();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void EventFromParent(string _triggerName)
        {
            Debug.Log("Trigger animation " + _triggerName + " on " + m_animator);
            
                    m_animator.SetTrigger(_triggerName);
 
        }
    }

}
