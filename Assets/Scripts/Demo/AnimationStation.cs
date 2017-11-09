using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GVR;

namespace VRControlls.Demo
{
    public class AnimationStation : MonoBehaviour
    {
        public Collider[] Collider;
        public GvrAudioSource AudioSource;

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
        public void Audio(bool _active)
        {
            if (_active) AudioSource.Play();
            else AudioSource.Stop();
        }
        //-------------------------------------------------------------------------------------------------------------
    }

}
