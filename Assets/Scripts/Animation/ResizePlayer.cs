using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using VRControlls.Events;
using UnityEngine;
using DG.Tweening;

namespace VRControlls.Animation
{
    public class ResizePlayer : MonoBehaviour
    {
        [Header("Settings")]
        [Range(0, 1)]
        //public float TargetSize = 0.1f;
        public float GroundLevel;
        public float Speed;
        private Camera m_mainCamera;
        private float m_originalFov;

        private float m_originalSize;
        private float m_currentSize;

        private UnityAction m_listenForMainStateChange;
        /// <summary>
        /// Set with percentage of current height
        /// </summary>
        public float CurrentSize { get { return (((gameObject.transform.position.y - GroundLevel) / m_originalSize) * 1); } set { Resize(value); } }

        // Use this for initialization
        void Awake()
        {
            m_listenForMainStateChange = new UnityAction(OnMainStateChange);
            m_mainCamera = Camera.main;
            m_originalFov = m_mainCamera.fieldOfView;
            m_originalSize = m_mainCamera.transform.position.y - GroundLevel;
            Debug.Log(CurrentSize);
           // CurrentSize = .5f;
        }
        void Start()
        {
            EventManager.Instance.StartListening("MAINSTATE_CHANGED", m_listenForMainStateChange);
        }
       

        public void Resize(float _size)
        {
            gameObject.transform.DOMoveY(gameObject.transform.position.y*_size, Speed);
            m_mainCamera.DOFieldOfView(m_originalFov*_size, Speed).OnComplete(()=>ResizeComplete());          
        }
        private void ResizeComplete()
        {
            EventManager.Instance.InvokeEvent("RESIZE_COMPLETE");
        }

        private void OnMainStateChange()
        {

        }
    }
}

