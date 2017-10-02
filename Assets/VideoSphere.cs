using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;
using VRControlls.Templates;
using System;
namespace VRShowcase.Media
{
    public class VideoSphere : Singleton<VideoSphere>
    {
        public enum VIDEOPLAYERSTATES { OFF, TRANSITION, PLAYING, STOP }

        public VIDEOPLAYERSTATES VideoPlayerState { get; private set; }
        public VideoTemplate[] Videos;
        public bool StopButton;


        private VideoPlayer m_videoPlayer;
        private Material m_videoMaterial;
        private MeshRenderer m_meshRenderer;
        private MeshCollider m_collider;
        private float m_currentPlayingTime;
        private Action m_callBack;

        public override void Awake()
        {
            base.Awake();
            VideoPlayerState = VIDEOPLAYERSTATES.OFF;
            m_videoPlayer = GetComponent<VideoPlayer>();
            m_meshRenderer = GetComponent<MeshRenderer>();
            m_collider = GetComponent<MeshCollider>();
            m_videoMaterial = m_meshRenderer.material;
            m_videoMaterial.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, 0));
        }

        void Start()
        {
            m_meshRenderer.enabled = false;
            m_videoPlayer.enabled = false;
            m_collider.enabled = false;
        }
        public void PlayVideo(string _videoName)
        {
            foreach (VideoTemplate _vt in Videos)
            {
                if (_vt.VideoName == _videoName)
                {

                    TogglePlayer(true);
                    ToggleVisibility(1);
                    m_collider.enabled = true;
                    m_videoPlayer.clip = _vt.Video;
                    m_videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
                    StartCoroutine(StopVideoWhenFinished(_vt.Video.length));
                    m_videoPlayer.Play();
                    VideoPlayerState = VIDEOPLAYERSTATES.PLAYING;
                    return;
                }
            }
        }
        public void StopVideo()
        {
            // todo proper stop button!
            m_videoPlayer.Stop();
            VideoPlayerState = VIDEOPLAYERSTATES.STOP;
            ToggleVisibility(0);
        }

        private IEnumerator StopVideoWhenFinished(double _time)
        {
            Debug.Log((float)_time);
            yield return new WaitForSeconds((float)_time);
            if (VideoPlayerState != VIDEOPLAYERSTATES.STOP)
            {
                StopVideo();
            }

        }

        private void ToggleVisibility(byte _endAlpha = 1)
        {
            if (_endAlpha == 1)
            {
                m_videoMaterial.DOColor(new Color(0.5f, 0.5f, 0.5f, (0.5f * _endAlpha)), "_TintColor", 5f);
            }
            else
            {
                m_videoMaterial.DOColor(new Color(0.5f, 0.5f, 0.5f, (0.5f * _endAlpha)), "_TintColor", 5f).OnComplete(() => TogglePlayer(false));
            }


        }
        private void TogglePlayer(bool _active)
        {
            VideoPlayerState = (_active) ? VIDEOPLAYERSTATES.PLAYING : VIDEOPLAYERSTATES.OFF;
            m_videoPlayer.enabled = _active;
            m_meshRenderer.enabled = _active;
            m_collider.enabled = _active;
        }

    }
}