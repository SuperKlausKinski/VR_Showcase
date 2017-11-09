using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VRControlls.Animation
{
    public class Hover : MonoBehaviour
    {

        public float HoverSpeed=2;
        public float HoverRange=0.25f;
 
        public bool HoverAtStart = true;

        private float m_originalY;

        public bool IsHovering {
        set
            {
                if (value)
                {
                    gameObject.transform.DOLocalMoveY(m_originalY + HoverRange, HoverSpeed).SetEase(Ease.OutCubic).SetLoops(-1,LoopType.Yoyo);
                }
                else
                {
                    gameObject.transform.DOLocalMoveY(m_originalY, HoverSpeed).SetEase(Ease.InOutCubic);
                }
            }
        }

        void Awake()
        {
            m_originalY = gameObject.transform.localPosition.y;
            if (HoverAtStart) IsHovering = true;
        }

    }
}

