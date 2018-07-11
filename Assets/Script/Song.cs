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
    public class Song : MonoBehaviour,
                                                ITrackableEventHandler
    {

        //------------Begin Sound----------
        public AudioSource soundTarget;
        public AudioClip clipTarget;
        private AudioSource[] allAudioSources;
        public Boolean emExecucao=false, musicaUm=false, musicaDois=false;

        //function to stop all sounds
        /*void StopAllAudio()
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
        }*/

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

        
        //function to play sound
        void playSound(string ss)
        {
            clipTarget = (AudioClip)Resources.Load(ss);
            soundTarget.clip = clipTarget;
            soundTarget.loop = false;
            soundTarget.playOnAwake = false;
            soundTarget.Play();
            //soundTarget.PlayScheduled(10);
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
                playSound("sounds/edited/KeysSomething");
            }

            if (mTrackableBehaviour.TrackableName == "GH")
            {
                playSound("sounds/edited/VozSomething");
            }

            if (mTrackableBehaviour.TrackableName == "JL")
            {
                playSound("sounds/edited/GuitarraSomething");
            }

            if (mTrackableBehaviour.TrackableName == "PM")
            {
                playSound("sounds/edited/BaixoSomething");
            }

            if (mTrackableBehaviour.TrackableName == "RS")
            {
                playSound("sounds/edited/BateriaSomething");
            }

            
            //Feeling - musica 2
            if (mTrackableBehaviour.TrackableName == "YS2")
            {
                playSound("sounds/musicaDois/Feeling.Completa");
            }

            if (mTrackableBehaviour.TrackableName == "GH2")
            {
                playSound("sounds/musicaDois/Feeling.George");
            }

            if (mTrackableBehaviour.TrackableName == "JL2")
            {
                playSound("sounds/musicaDois/Feeling.Keys");
            }

            if (mTrackableBehaviour.TrackableName == "PM2")
            {
                playSound("sounds/musicaDois/Feeling.Paul");
            }

            if (mTrackableBehaviour.TrackableName == "RS2")
            {
                playSound("sounds/musicaDois/Feeling.Ringo");
            }

            //Fixing a hole - musica 3
            if (mTrackableBehaviour.TrackableName == "IT")
            {
                playSound("sounds/musicaTres/Fixing.a.hole");
            }

            if (mTrackableBehaviour.TrackableName == "GH3")
            {
                playSound("sounds/musicaTres/Fixing.George");
            }

            if (mTrackableBehaviour.TrackableName == "JL3")
            {
                playSound("sounds/musicaTres/Fixing.John");
            }

            if (mTrackableBehaviour.TrackableName == "PM3")
            {
                playSound("sounds/musicaTres/Fixing.Paul");
            }

            if (mTrackableBehaviour.TrackableName == "RS3")
            {
                playSound("sounds/musicaTres/Fixing.Ringo");
            }

			//The Waking Dead
			if(mTrackableBehaviour.TrackableName == "wd"){
				playSound ("sounds/TWD/twd");
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

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

            //Parar uma unica musica Func
            StopAudio(mTrackableBehaviour.TrackableName);
            Debug.Log("Audio " + mTrackableBehaviour.TrackableName + " stoped");
        }

        #endregion // PRIVATE_METHODS
    }
}
