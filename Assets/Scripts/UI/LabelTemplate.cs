using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VRControlls.UI
{
    [CreateAssetMenu(menuName = "VR UI Labels/New Label")]
    public class LabelTemplate : ScriptableObject
    {
        public GameObject LabelBaseObject;
        public Image LabelImage;
        public string LabelText;
        public string OnClickEvent;
    }
}

