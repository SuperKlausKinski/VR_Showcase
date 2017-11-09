using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRControlls.Events;
using UnityEngine.EventSystems;

namespace VRControlls.UI
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(EventTrigger))]
    public class Label : MonoBehaviour
    {
        public enum LABELSTATES { HIDDEN, CLOSED, IDLE }
        public LABELSTATES Labelstate
        {
            get; private set;
        }
        public bool SetLabelInteractable { set { GetComponent<Collider>().enabled = value; } }
        public bool UseOnlyOnce;
        public LabelTemplate LabelObjectData;
        private GameObject m_labelGameObject;
        private Animator m_labelAnimator;
        private Text m_labelText;
        private Image m_labelImage;
        private Camera m_Camera;
        //-------------------------------------------------------------------------------------------------------------
        void Awake()
        {
            InitLabel();
            AddEventListeners();
        }
        //-------------------------------------------------------------------------------------------------------------
        void Update()
        {
            if (Labelstate != LABELSTATES.HIDDEN)
            {
                transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                m_Camera.transform.rotation * Vector3.up);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private void AddEventListeners()
        {

            // add event listener
            EventTrigger _eventTrigger = GetComponent<EventTrigger>();
            EventTrigger.Entry _onPointerEnter = new EventTrigger.Entry();
            EventTrigger.Entry _onClick = new EventTrigger.Entry();
            EventTrigger.Entry _onPointerExit = new EventTrigger.Entry();
            // on Pointer Enter event
            _onPointerEnter.eventID = EventTriggerType.PointerEnter;
            _onPointerEnter.callback.AddListener((eventData) => { HoverLabel(); });
            _eventTrigger.triggers.Add(_onPointerEnter);
            // on Pointer Click event
            _onClick.eventID = EventTriggerType.PointerClick;
            _onClick.callback.AddListener((eventData) => { ActivateLabel(); });
            _eventTrigger.triggers.Add(_onClick);
            // on Pointer Click event
            _onPointerExit.eventID = EventTriggerType.PointerExit;
            _onPointerExit.callback.AddListener((eventData) => { HideLabel(); });
            _eventTrigger.triggers.Add(_onPointerExit);

        }
        //-------------------------------------------------------------------------------------------------------------
        void OnPointerEnter()
        {
          //  Debug.Log("ENTER!");
        }
        private void InitLabel()
        {
           // Debug.Log("init label");
            m_Camera = Camera.main;
            m_labelGameObject = Instantiate(LabelObjectData.LabelBaseObject, transform.position, transform.rotation);
            m_labelGameObject.transform.SetParent(gameObject.transform);

            // reposition the canvas so it sits with it's bottom right at the parent transform
            RectTransform _canvas = m_labelGameObject.GetComponent<RectTransform>();
            _canvas.localPosition = new Vector2((_canvas.sizeDelta.x / 2) * -0.01f, (_canvas.sizeDelta.y / 2) * -0.01f);

            m_labelText = m_labelGameObject.transform.Find("LabelRoot/LabelText").GetComponent<Text>();
            m_labelText.text = LabelObjectData.LabelText;
            m_labelText.gameObject.SetActive(false);
            m_labelImage = m_labelGameObject.transform.Find("LabelRoot/LabelImage").GetComponent<Image>();
            m_labelImage.sprite = LabelObjectData.DefaultLabel;
            Labelstate = LABELSTATES.CLOSED;
            m_labelAnimator = m_labelGameObject.GetComponent<Animator>();
          //  Debug.Log("Label Image" + m_labelImage.gameObject.name);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void ShowLabel()
        {
           // Debug.Log("Show");

        }
        //-------------------------------------------------------------------------------------------------------------
        private void HoverLabel()
        {
           // Debug.Log("Hover");
            if (Labelstate != LABELSTATES.CLOSED) { return; }
            m_labelAnimator.SetTrigger("HOVER");
            Labelstate = LABELSTATES.IDLE;
            m_labelImage.sprite = LabelObjectData.LabelImage;
            m_labelText.gameObject.SetActive(true);
        }
        private void ActivateLabel()
        {
          //  Debug.Log("Active");
            if (Labelstate != LABELSTATES.IDLE) { return; }
            m_labelAnimator.SetTrigger("CLICK");
            if (LabelObjectData.OnClickEvent != null)
            {
                EventManager.Instance.InvokeEvent(LabelObjectData.OnClickEvent);
                m_labelImage.sprite = LabelObjectData.DefaultLabel;
                m_labelText.gameObject.SetActive(false);
            }
            
            Labelstate = LABELSTATES.CLOSED;
            if (UseOnlyOnce)
            {
                Labelstate = LABELSTATES.CLOSED;
                gameObject.SetActive(false);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private void HideLabel()
        {
          //  Debug.Log("Hide");
            if (Labelstate != LABELSTATES.IDLE) { return; }
            m_labelAnimator.SetTrigger("EXIT");
            Labelstate = LABELSTATES.CLOSED;
            m_labelImage.sprite = LabelObjectData.DefaultLabel;
            m_labelText.gameObject.SetActive(false);
        }
 
    }
}