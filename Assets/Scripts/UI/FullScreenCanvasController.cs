using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRControlls.Events;
using VRControlls.Templates;

namespace VRControlls.UI
{
    public class FullScreenCanvasController : MonoBehaviour
    {

        private UnityAction listenForChange;
        
        
        public CanvasGroup CanvasGroup;
        public GameObject BlockingSphere;
        public GameObject Title;
        public float Speed = 1;
        //-------------------------------------------------------------------------------------------------------
        private MeshRenderer m_MaterialRenderer;
        //-------------------------------------------------------------------------------------------------------
        public void Awake()
        {
            BlockingSphere.SetActive(true);
            Title.SetActive(true);
            m_MaterialRenderer = BlockingSphere.GetComponent<MeshRenderer>();
        }
        //-------------------------------------------------------------------------------------------------------
        public void Start()
        {           
            listenForChange = new UnityAction(OnGameStateChanged);
            EventManager.Instance.StartListening("GAMESTATE_CHANGED", listenForChange);
           
        }
        //-------------------------------------------------------------------------------------------------------
        public void FadeOut()
        {
            StartCoroutine(StartFade(1));
        }
        //-------------------------------------------------------------------------------------------------------
        public void FadeIn()
        {
            StartCoroutine(StartFade(0));
        }
        //-------------------------------------------------------------------------------------------------------
        private void OnGameStateChanged()
        {
            
            switch (GameFSM.Instance.Gamestate)
            {
                case (GameFSM.GAMESTATES.INTRO):
                    FadeOut();
                    break;
                default:
                    break;
            }
        }
        //-------------------------------------------------------------------------------------------------------
        IEnumerator StartFade(int _direction)
        {
           
            CanvasGroup.gameObject.SetActive(true);
   
            while (CanvasGroup.alpha > 0)
            {          
                CanvasGroup.alpha -= 0.01f * _direction*Speed;
                m_MaterialRenderer.material.SetColor("_TintColor", new Color(1, 1, 1, CanvasGroup.alpha));
                yield return null;
            }
            CanvasGroup.gameObject.SetActive(false);
            yield return null;
        }


    }
}