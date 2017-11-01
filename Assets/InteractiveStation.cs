using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControlls.UI;
using DG.Tweening;

namespace VRControlls.Demo
{
    public class InteractiveStation : MonoBehaviour
    {
        public enum STATIONSTATE { IDLE,ROTATING,LIGHT}
        public STATIONSTATE StationState;
        public float RotationSpeed;
        public Transform Box;

        public Label[]   Label;
        public Collider[] Collider;
        
        //-------------------------------------------------------------------------------------------------------------
        private float m_direction;
        private Material m_lightMaterial;
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            ToggleStationInteractability(false);
            m_lightMaterial = Box.Find("light").GetComponent<Renderer>().material;
        }
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
        public void ToggleStationInteractability(bool _active)
        {
            foreach (Collider _col in Collider)
            {
                _col.enabled = _active;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void FlashGreen()
        {
            Debug.Log("FlashGreen");
            m_lightMaterial.DOColor(Color.green, "_Color", 0.5f).SetLoops(3, LoopType.Yoyo).OnComplete(()=>m_lightMaterial.color = Color.white);
        }
        public void FlashRed()
        {
            Debug.Log("FlashRed");
            m_lightMaterial.DOColor(Color.red, "_Color", 0.5f).SetLoops(3, LoopType.Yoyo).OnComplete(() => m_lightMaterial.color = Color.white); ;
        }
    }

}
