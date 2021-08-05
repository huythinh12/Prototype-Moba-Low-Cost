using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.System;
using System;

namespace CharacterMechanism.System
{
    /// <inheritdoc />
    /// <summary>
    /// Detector of targets within range
    /// </summary>
    [RequireComponent(typeof(SphereCollider))]
    public sealed class TargetsDetecter : MonoBehaviour
    {
        ///////////////////////////////
        ////////// General ////////////
        ///////////////////////////////

        static readonly float DetectRange = 10f;

        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        ///////////////////////////////
        ///////// List Target /////////

        private List<CharacterSystem> charactersInDetectRange = new List<CharacterSystem>();
        private CharacterSystem characterSystem = null;

        ///////////////////////////////
        ////////// Component //////////

        new private SphereCollider collider = null;

        //////////////////////////////
        ////////// Property //////////
        //////////////////////////////

        ///////////////////////////////
        ////////// Component //////////

        public List<CharacterSystem> CharactersInDetectRange => charactersInDetectRange;
        public Collider GetCollider => collider;


        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        //////////////////////////////
        //////////// API /////////////

        /// <summary>
        /// Create a GameObject Targets Detecter for CharacterSytem and return TargetsDetecter    
        /// </summary>
        static public TargetsDetecter Create(CharacterSystem characterSystem)
        {
            TargetsDetecter detecter = new GameObject("Targets Detecter", typeof(SphereCollider), typeof(TargetsDetecter)).GetComponent<TargetsDetecter>();
            detecter.transform.SetParent(characterSystem.transform);
            detecter.transform.localPosition = Vector3.zero;
            detecter.characterSystem = characterSystem;

            return detecter;
        }

        //////////////////////////////
        //////////// ??? /////////////

        private void OnTriggerEnter(Collider other)
        {

            if (other == characterSystem.GetCollider)
            {
                // Not getting collider parent
            }
            else
            {
                // The TargetsDetectors do not interact with each other
                if (other.GetComponent<TargetsDetecter>() == null)
                {
                    CharacterSystem characterSystemCollider = other.GetComponent<CharacterSystem>();

                    if (characterSystemCollider != null)
                    {
                        if (CharacterSystem.IsEnemy(characterSystem, characterSystemCollider))
                        {
                            charactersInDetectRange.Add(characterSystemCollider);
                            OnTargetChange?.Invoke(GetNextTransformTarget());
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // The TargetsDetectors do not interact with each other
            if (other.GetComponent<TargetsDetecter>() == null)
            {
                CharacterSystem characterSystemCollider = other.GetComponent<CharacterSystem>();

                if (characterSystemCollider != null)
                {
                    if (CharacterSystem.IsEnemy(characterSystem, characterSystemCollider))
                    {
                        charactersInDetectRange.Remove(characterSystemCollider);
                        OnTargetChange?.Invoke(GetNextTransformTarget());
                    }
                }
            }
        }

        //////////////////////////////
        ////////// Callback //////////

        /// <summary>
        /// Load all the components to drive the action
        /// </summary>
        /// <remarks>
        /// Call at the beginning before InitializeComponents
        /// </remarks>
        private void LoadComponents()
        {
            this.collider = GetComponent<SphereCollider>();
        }

        /// <summary>
        /// Initialize all the loaded components to drive the action
        /// </summary>
        /// <remarks>
        /// Call at the beginning after LoadComponents
        /// </remarks>
        private void InitializeComponents()
        {
            this.collider.isTrigger = true;
            this.collider.radius = DetectRange;
        }

        ////////////////////////////////////////////
        ////////// MonoBehaviour Callback //////////

        private void Awake()
        {
            this.LoadComponents();
            this.InitializeComponents();
        }

        public event Action<Transform> OnTargetChange;


        public Transform GetNextTransformTarget()
        {
            if (charactersInDetectRange.Count == 0)
            {
                return SpawnManager.Instance.GetTransformUltimateTowerTarget(characterSystem.GetProfile.GetTeamCharacter);
            }
            else
            {
                CharacterSystem charactersPrioritized = charactersInDetectRange[0];
                for (int i = 1; i < charactersInDetectRange.Count; i++)
                {
                    // Character with smaller enum TypeCharacter will be prioritized
                    if ((int)(charactersInDetectRange[i].GetProfile.GetTypeCharacter) < (int)(charactersPrioritized.GetProfile.GetTypeCharacter))
                    {
                        charactersPrioritized = charactersInDetectRange[i];
                    }
                }

                return charactersPrioritized.transform;
            }

        }
    }
}