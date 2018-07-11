/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

//Add This script
using System.Collections;
using System.Collections.Generic;
using System;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class UltraSong : MonoBehaviour,
                                                ITrackableEventHandler
    {

        //------------Begin Sound----------
        public AudioSource soundTarget;
        public AudioClip clipTarget;
        private AudioSource[] allAudioSources;
        public Boolean emExecucao = false, musicaUm = false, musicaDois = false;

        //function to stop all sounds
        void StopAllAudio()
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
        }

        void StopAudio(String a)
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                if (audioS.name == a)
                {
                    audioS.Stop();
                }
            }

        }

        void unMute(String a)
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                if (audioS.name == a)
                {
                    audioS.volume = 100;
                }
            }

        }

        void mute(String a)
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                if (audioS.name == a)
                {
                    audioS.volume = 0;
                }
            }

        }

        //function to play sound
        void playSound(string ss)
        {
            clipTarget = (AudioClip)Resources.Load(ss);
            soundTarget.clip = clipTarget;
            soundTarget.loop = false;
            soundTarget.playOnAwake = false;
            //soundTarget.Play();
            soundTarget.PlayOneShot(clipTarget);
            //soundTarget.volume = 0;
        }


        void playAllSomething()
        {
            playSound("sounds/edited/KeysSomething");
            playSound("sounds/edited/VozSomething");
            playSound("sounds/edited/GuitarraSomething");
            playSound("sounds/edited/BaixoSomething");
            playSound("sounds/edited/BateriaSomething");
        }

        
        void playAllFeeling()
        {
            
            playSound("sounds/musica2/Feeling.Keys");
            playSound("sounds/musica2/Feeling.George");
            playSound("sounds/musica2/Feeling.Keys");
            playSound("sounds/musica2/Feeling.Paul");
            playSound("sounds/musica2/Feeling.Ring");
        }

        //-----------End Sound------------


        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

            //Register / add the AudioSource as object
            soundTarget = (AudioSource)gameObject.AddComponent<AudioSource>();

        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");


            //Play Sound, IF detect an target


            //Something - musica 1
            if (mTrackableBehaviour.TrackableName == "YS")
            {
                musicaUm = true;
                playAllSomething();
            }

            if (musicaUm)
            {
            if (mTrackableBehaviour.TrackableName == "GH")
            {
                unMute(mTrackableBehaviour.TrackableName);
            }

            if (mTrackableBehaviour.TrackableName == "JL")
            {
                unMute(mTrackableBehaviour.TrackableName);
            }

            if (mTrackableBehaviour.TrackableName == "PM")
            {
                unMute(mTrackableBehaviour.TrackableName);
            }

            if (mTrackableBehaviour.TrackableName == "RS")
            {
                unMute(mTrackableBehaviour.TrackableName);
            }
            }


            //Feeling - musica 2
            if (mTrackableBehaviour.TrackableName == "YS2")
            {
                musicaDois = true;
                playAllFeeling();
            }

            if (musicaDois)
            {
                if (mTrackableBehaviour.TrackableName == "GH")
                {
                    unMute(mTrackableBehaviour.TrackableName);
                }

                if (mTrackableBehaviour.TrackableName == "JL")
                {
                    unMute(mTrackableBehaviour.TrackableName);
                }

                if (mTrackableBehaviour.TrackableName == "PM")
                {
                    unMute(mTrackableBehaviour.TrackableName);
                }

                if (mTrackableBehaviour.TrackableName == "RS")
                {
                    unMute(mTrackableBehaviour.TrackableName);
                }
            }



        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost" + " Audio stop teste");

            //Parar uma unica musica Func
            if ((musicaUm == true && musicaDois == false) || (musicaUm == false && musicaDois == true))
            {
                mute(mTrackableBehaviour.TrackableName);
            }
            else if ((musicaUm == true && musicaDois == true) || (musicaUm == false && musicaDois == false))
            {
                StopAllAudio();
            }
            else
            {
                Debug.Log("Deu bug");
            }
            //StopAudio(mTrackableBehaviour.TrackableName);
        }

        #endregion // PRIVATE_METHODS
    }
}
