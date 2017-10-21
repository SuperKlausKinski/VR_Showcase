using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VRControlls.Events;

public class PerspectiveStation : MonoBehaviour {


    public void DeShrink()
    {
        DOVirtual.DelayedCall(10, () => EventManager.Instance.InvokeEvent("DESHRINK"));
    }

}
