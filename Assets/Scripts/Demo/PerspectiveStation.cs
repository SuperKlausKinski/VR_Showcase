using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VRControlls.Events;
using VRControlls.Navigation;

namespace VRControlls.Demo
{

    public class PerspectiveStation : MonoBehaviour
    {

        public float TimeToStayShrunk;

        public Collider[] Collider;

        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            ToggleStationInteractability(false);
        }
        //-------------------------------------------------------------------------------------------------------------
        public void Shrink()
        {
            DOVirtual.DelayedCall(TimeToStayShrunk, DeShrink);
        }
        //-------------------------------------------------------------------------------------------------------------
        public void DeactivateWayPoints()
        {
            Waypoints.Instance.WaypointsEnabled = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void DeShrink()
        {
            EventManager.Instance.InvokeEvent("DESHRINK");
            DOVirtual.DelayedCall(2, () => Waypoints.Instance.WaypointsEnabled = true);
        }
        //-------------------------------------------------------------------------------------------------------------
        public void ToggleStationInteractability(bool _active)
        {
            foreach (Collider _col in Collider)
            {
                _col.enabled = _active;
            }
        }

    }
}