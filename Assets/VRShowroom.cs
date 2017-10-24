using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRControlls.Demo
{
    public class VRShowroom : MonoBehaviour
    {

        public GameObject PlayerStartPosition;

        private GameObject m_player;

        void Awake()
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_player.transform.position = PlayerStartPosition.transform.position;
        }


    }
}

