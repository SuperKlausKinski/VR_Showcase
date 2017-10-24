using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using VRControlls.Events;
using UnityEngine;
using DG.Tweening;

public class InteractableObject : MonoBehaviour {

    public string ListenForEvent;
    public float Delay = 0;
    public UnityEvent EventToTrigger;
    private UnityAction m_listenForEvent;

    void Awake()
    {
        if (EventToTrigger == null)
            EventToTrigger = new UnityEvent();

        m_listenForEvent = new UnityAction(TriggerEvent);
    }

    void Start()
    {
        if(ListenForEvent!=null)
        EventManager.Instance.StartListening(ListenForEvent, TriggerEvent);
    }
    private void TriggerEvent()
    {
        DOVirtual.DelayedCall(Delay,()=> EventToTrigger.Invoke());    
    }

}
