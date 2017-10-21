using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControlls.UI;

namespace VRControlls.Demo
{
    public class InteractiveStation : MonoBehaviour
    {
        public enum STATIONSTATE { IDLE,ROTATING,LIGHT}
        public STATIONSTATE StationState;
        public float RotationSpeed;
        public Transform Box;
        public Label[]   Label;
        //-------------------------------------------------------------------------------------------------------------
        private float m_direction;
        //-------------------------------------------------------------------------------------------------------------
        void Update()
        {
            if (StationState == STATIONSTATE.ROTATING)
            {
                Box.transform.Rotate((Vector3.forward * RotationSpeed) *m_direction);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void RotateBox(int _direction)
        {
            m_direction = _direction;
            StationState = STATIONSTATE.ROTATING;
            foreach(Label _label in Label)
            {
                _label.SetLabelInteractable = false;  // in order to prevent collision, set the colliders from the other labels inactive
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void StopRotation()
        {
            StationState = STATIONSTATE.IDLE;
            foreach (Label _label in Label)
            {
                _label.SetLabelInteractable = true; // and activate them again
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }

}
