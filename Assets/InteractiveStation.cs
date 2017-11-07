using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRControlls.UI;
using DG.Tweening;
using GVR;

namespace VRControlls.Demo
{
    public class InteractiveStation : MonoBehaviour
    {
        public enum STATIONSTATE { IDLE,ROTATING,LIGHT}
        public STATIONSTATE StationState;
        public float RotationSpeed;
        public Transform Box;
        public GameObject Audio;

        public Label[]   Label;
        public Collider[] Collider;

       
        public AudioClip RightAudio;
        public AudioClip WrongAudio;
        //-------------------------------------------------------------------------------------------------------------
        private float m_direction;
        private Material m_lightMaterial;
        private GvrAudioSource m_audioSource;
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            ToggleStationInteractability(false);
            m_lightMaterial = Box.Find("light").GetComponent<Renderer>().material;
            m_audioSource = Audio.GetComponent<GvrAudioSource>();
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
                if (_col == null) { Debug.LogWarning("Collider missing in collider list at" + gameObject.name);  continue; }
                _col.enabled = _active;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void FlashGreen()
        {
            m_audioSource.PlayOneShot(RightAudio);
            m_lightMaterial.DOColor(Color.green, "_Color", 0.5f).SetLoops(3, LoopType.Yoyo).OnComplete(()=>m_lightMaterial.color = Color.white);          
            
        }
        public void FlashRed()
        {
            m_audioSource.PlayOneShot(WrongAudio);
            m_lightMaterial.DOColor(Color.red, "_Color", 0.5f).SetLoops(3, LoopType.Yoyo).OnComplete(() => m_lightMaterial.color = Color.white); ;
        }
    }

}
