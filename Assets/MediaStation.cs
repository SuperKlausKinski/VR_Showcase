using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GVR;

namespace VRControlls.Demo
{
    public class MediaStation : MonoBehaviour
    {
        public GvrAudioSource AudioSourceLeft;
        public GvrAudioSource AudioSourceRight;
        public Collider[] Collider;
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            ToggleStationInteractability(false);
            PlayAudio(false);
        }
        //-------------------------------------------------------------------------------------------------------------
        public void PlayAudio(bool _active)
        {
            if (_active)
            {
                AudioSourceLeft.Play();
                AudioSourceRight.Play();
            }
            else
            {
                AudioSourceLeft.Stop();
                AudioSourceRight.Stop();
            }
            
        }
        //-------------------------------------------------------------------------------------------------------------
        public void ToggleStationInteractability(bool _active)
        {
            foreach (Collider _col in Collider)
            {
                if (_col == null) { Debug.LogWarning("Collider missing in collider list at" + gameObject.name); continue; }
                _col.enabled = _active;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }

}
