using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.Information;
using CharacterMechanism.ScriptableObject;
using CharacterMechanism.Attribute;
using CharacterMechanism.DataBase;
using System;

namespace CharacterMechanism.System
{
    /// <inheritdoc />
    /// <summary>
    /// Class to create a character system
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(Rigidbody))]
    public class CharacterSystem : MonoBehaviour
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        ///////////////////////////////
        ////////// Component //////////

        private Animator animator = null;
        new private Collider collider = null;
        new private Rigidbody rigidbody = null;
        private TargetsDetecter targetsDetecter = null;

        /////////////////////////////
        ////////// Profile //////////

        [Header("Profile")]
        [SerializeField] private Profile profile = null;

        ////////////////////////////////////////////////
        ////////// Action State Configuration //////////

        [SerializeField] private AActionState startActionState = null;

        //////////////////////////////////////////////
        ////////// Action State Information //////////

        [ReadOnly, SerializeField] private AActionState currentActionState = null;
        [ReadOnly, SerializeField] private AActionState previousActionState = null;

        ///////////////////////////////////
        ////////// Debug Setting //////////

        [SerializeField] private bool shouldDisplayTransition = false;

        ///////////////////////////////////////
        ////////// Input Information //////////

        [SerializeField] private InputInformation inputInformation = null;

        ///////////////////////////////////////////
        ////////// Trigger Configuration //////////

        [SerializeField] private ActionTransition[] triggerActionTransitions = null;

        //////////////////////////////
        ////////// Property //////////
        //////////////////////////////

        ///////////////////////////////
        ////////// Component //////////

        public Animator GetAnimator => animator;
        public Collider GetCollider => collider;
        public Rigidbody GetRigidbody => rigidbody;
        public TargetsDetecter GetTargetsDetecter => targetsDetecter;

        /////////////////////////////
        ////////// Profile //////////

        public Profile GetProfile => profile;

        //////////////////////////////////////////////
        ////////// Action State Information //////////

        public AActionState GetCurrentActionState => currentActionState;
        public AActionState GetPreviousActionState => previousActionState;

        ///////////////////////////////////
        ////////// Debug Setting //////////

        public bool ShouldDisplayTransition
        {
            get { return shouldDisplayTransition; }
            set { shouldDisplayTransition = value; }
        }

        ///////////////////////////////////////
        ////////// Input Information //////////

        /// <summary>
        /// Return the input information of the system to control the action
        /// </summary>
        public InputInformation InputInformation => inputInformation;


        ////////////////////////////
        /////// Constructor ////////
        ////////////////////////////

        public CharacterSystem(ProfileData profileData)
        {
            profile = new Profile(profileData);
        }

        ///////////////////////////////
        /////////// Event /////////////
        ///////////////////////////////

        public event Action<CharacterSystem> OnSpawn;
        public event Action<CharacterSystem> OnRevival;
        public event Action<CharacterSystem> OnDie;
        public event Action<CharacterSystem> OnHealthChange;
        public event Action<float> OnTakeDamage;
        public event Action<float> OnHealHealth;
        public event Action<CharacterSystem> OnManaChange;
        public event Action<CharacterSystem> OnLevelChange;


        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        //////////////////////////////
        ////////// Callback //////////

        /// <summary>
        /// Load all the components to drive the action
        /// </summary>
        /// <remarks>
        /// Call at the beginning before InitializeComponents
        /// </remarks>
        protected void LoadComponents()
        {
            this.animator = GetComponent<Animator>();
            this.collider = GetComponent<Collider>();
            this.rigidbody = GetComponent<Rigidbody>();

            this.targetsDetecter = TargetsDetecter.Create(this);
        }

        /// <summary>
        /// Initialize all the loaded components to drive the action
        /// </summary>
        /// <remarks>
        /// Call at the beginning after LoadComponents
        /// </remarks>
        protected void InitializeComponents()
        {
            this.collider.isTrigger = true;

            this.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            this.rigidbody.useGravity = false;
        }

        ////////////////////////////////////////////
        ////////// MonoBehaviour Callback //////////

        protected virtual void Awake()
        {
            this.AttemptToLoadStartActionState();
            this.LoadComponents();
            this.InitializeComponents();
        }

        protected virtual void FixedUpdate()
        {
            this.currentActionState.UpdateAction(this, this.inputInformation);
        }

        protected virtual void Start()
        {
            this.currentActionState.BeginAction(this, this.inputInformation);
        }

        protected virtual void Update()
        {
            if (AttemptToTriggerActionTransition() == false)
            {
                AttemptToTransitToNextActionState();
            }
        }

        /////////////////////////////
        ////////// Service //////////

        /// <summary>
        /// Attempt to load the start action state
        /// </summary>
        /// <remarks>
        /// First function to be called at the beginning
        /// </remarks>
        private void AttemptToLoadStartActionState()
        {
            this.currentActionState = this.startActionState;
            if (this.currentActionState == null)
            {
                Debug.LogError("There is no start action state!", gameObject);
                enabled = false;
            }
        }

        /// <summary>
        /// Attempt to transit to a next action state using the associated action transitions 
        /// </summary>
        /// <remarks>
        /// Call every Update if AttemptToTriggerActionTransition is false
        /// </remarks>
        private void AttemptToTransitToNextActionState()
        {
            var nextActionState = this.currentActionState.AttemptToGetNextActionState(this, this.inputInformation);

            if (nextActionState)
            {
                this.TransitToNextActionState(nextActionState);
            }
        }

        /// <summary>
        /// Attempt to trigger one of the trigger action transition and transit to her action state
        /// </summary>
        /// <remarks>
        /// Call every Update ; if true AttemptToTransitToNextActionState is not called
        /// </remarks>
        private bool AttemptToTriggerActionTransition()
        {
            foreach (var triggerActionTransition in this.triggerActionTransitions)
            {
                var nextActionState = triggerActionTransition.Simulate(this, this.inputInformation);

                if (nextActionState)
                {
                    this.TransitToNextActionState(nextActionState);
                    return (true);
                }
            }
            return (false);
        }

        /// <summary>
        /// Transit to the next action state
        /// </summary>
        private void TransitToNextActionState(AActionState nextActionState)
        {
            this.currentActionState.EndAction(this, this.inputInformation);
            this.previousActionState = this.currentActionState;
            this.currentActionState = nextActionState;
            this.currentActionState.BeginAction(this, this.inputInformation);

            if (this.shouldDisplayTransition)
            {
                Debug.Log(this.previousActionState.GetType().Name + " --> " + this.currentActionState.GetType().Name, gameObject);
            }
        }
    }
}