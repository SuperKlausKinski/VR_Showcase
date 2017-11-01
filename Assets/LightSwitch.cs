using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VRControlls.Animation
{


    public class LightSwitch : MonoBehaviour
    {

        public float Time = 1;
        private Light m_lightSource;


        private void Awake()
        {
            m_lightSource = GetComponent<Light>();
            m_lightSource.intensity = 0;
        }

        public void TweenLightIntensity(float _tweenTo)
        {
            m_lightSource.DOIntensity(_tweenTo, Time);
        }

    }
}