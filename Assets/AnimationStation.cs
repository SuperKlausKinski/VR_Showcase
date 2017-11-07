using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VRControlls.Demo
{
    public class AnimationStation : MonoBehaviour
    {
        public Collider[] Collider;

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
