using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VRControlls.Animation
{
    public class RotateObject : MonoBehaviour
    {

        private Tween m_RotationTween;
        //-------------------------------------------------------------------------------------------------------------
        public void RotateObjectAround(int _direction)
        {
            if (m_RotationTween == null)
            {
                m_RotationTween = transform.DOLocalRotate((Vector3.up * 180f)*_direction, 3.0f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
            else
            {
                m_RotationTween.Play();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void StopObjectRotation()
        {
            Debug.Log("STop tween");
            if (m_RotationTween != null)
            {
                m_RotationTween.Pause();
            }

        }
        //-------------------------------------------------------------------------------------------------------------
    }

}
