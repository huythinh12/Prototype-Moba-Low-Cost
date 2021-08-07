using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterMechanism.Information;
using CharacterMechanism.ScriptableObject;
using CharacterMechanism.Attribute;
using CharacterMechanism.DataBase;
using CharacterMechanism.Behaviour;
using System;
using CharacterMechanism.History;

namespace CharacterMechanism.System
{
    /// <inheritdoc />
    /// <summary>
    /// Class to create a character system
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    [DefaultExecutionOrder(200)]
    public class CharacterSystem : MonoBehaviour
    {
        private bool isAlive = true;

        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        ///////////////////////////////
        ////////// Component //////////

        ////////// In Object //////////

        private Animator animator = null;
        new private BoxCollider collider = null;
        new private Rigidbody rigidbody = null;
        private GameObject gameObjectClone;

        //////// In Child Object ////////

        private Canvas controlPanel = null;

        private KDAUI kdaUI = null;
        private StatsBar statsBar = null;
        private TargetsDetecter targetsDetecter = null;

        /////////////////////////////
        ////////// Profile //////////

        [Header("Profile")]
        [SerializeField] private CharacterHistory history = new CharacterHistory();
        [SerializeField] private Profile profile = null;
        private ProfileData profileData;

        ////////////////////////////////////////////////
        ////////// Action State Configuration //////////

        [SerializeField] private AActionState startActionState;

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

        public bool IsAlive => isAlive;

        ///////////////////////////////
        ////////// Component //////////

        ////////// In Object //////////

        public Animator GetAnimator => animator;
        public BoxCollider GetCollider => collider;
        public Rigidbody GetRigidbody => rigidbody;

        //////// In Child Object ////////

        public TargetsDetecter GetTargetsDetecter => targetsDetecter;

        /////////////////////////////
        ////////// Profile //////////

        public CharacterHistory GetHistory => history;
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

        public CharacterSystem()
        {
            this.profileData = new ProfileData();
            profile = new Profile(this.profileData);
        }

        public CharacterSystem(ProfileData profileData)
        {
            this.profileData = profileData;
            profile = new Profile(this.profileData);
        }

        ///////////////////////////////
        /////////// Event /////////////
        ///////////////////////////////

        public event Action<CharacterSystem> OnPositionChange;
        public event Action<CharacterSystem> OnSpawn;
        public event Action<CharacterSystem> OnRevival;
        public event Action<CharacterSystem> OnDie;
        public event Action<CharacterSystem> OnHealthChange;
        public event Action<Vector3, float, DamageType, bool> OnTakeDamage;
        public event Action<float> OnHealHealth;
        public event Action<CharacterSystem> OnManaChange;
        public event Action<CharacterSystem> OnLevelChange;
        public event Action<CharacterSystem> OnKDAChange;


        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        //////////////////////////////
        ////////// Callback //////////


        protected void LoadDataFromDataBase(string nameID)
        {
            CharacterSystem characterSystemFormDataBase = CharacterSystemDatabase.Instance.GetCharacter(nameID);
            this.profile = characterSystemFormDataBase.GetProfile;
        }

        /// <summary>
        /// Load all the components to drive the action
        /// </summary>
        /// <remarks>
        /// Call at the beginning before InitializeComponents
        /// </remarks>
        protected void LoadComponents()
        {
            this.animator = GetComponent<Animator>();
            this.collider = GetComponent<BoxCollider>();
            this.rigidbody = GetComponent<Rigidbody>();

            this.profile = new Profile(new ProfileData());
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
            this.collider.center = new Vector3(0f, 0.8989141f, 0f);
            this.collider.size = new Vector3(1f, 1.791575f, 1f);

            this.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            this.rigidbody.useGravity = false;
        }

        ////////////////////////////////////////////
        ////////// MonoBehaviour Callback //////////

        protected virtual void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Character");

            this.LoadComponents();
            this.InitializeComponents();

            this.AttemptToLoadStartActionState();
        }

        protected virtual void FixedUpdate()
        {
            this.currentActionState.UpdateAction(this, this.inputInformation);
        }

        protected virtual void Start()
        {
            this.statsBar = StatsBar.Create(this);
            this.targetsDetecter = TargetsDetecter.Create(this);

            this.currentActionState.BeginAction(this, this.inputInformation);
            gameObjectClone = gameObject;
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
            this.startActionState = Resources.Load<AActionState>("Character Behaviors/Action States/IdleState");
            this.currentActionState = this.startActionState;

            //this.triggerActionTransitions = startActionState.actionTransitions;
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
            if (this.triggerActionTransitions != null)
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

        static public CharacterSystem Create(string nameID, TeamCharacter teamCharacter, TypeBehavior typeBehavior)
        {
            CharacterSystem characterSystem = Instantiate(Resources.Load<CharacterSystem>("Character Models/" + nameID));
            characterSystem.LoadDataFromDataBase(nameID);
            characterSystem.GetProfile.SetTeam(teamCharacter);
            characterSystem.gameObject.name = string.Format("{0} ({1})", nameID, teamCharacter.ToString());
            characterSystem.AddBehaviorBasedOnType(typeBehavior, characterSystem.GetProfile.GetTypeCharacter);

            characterSystem.OnSpawn += MinimapManager.Instance.InitializeIcon;
            characterSystem.OnDie += MinimapManager.Instance.TurnOffIcon;
            characterSystem.OnRevival += MinimapManager.Instance.TurnOnIcon;
            characterSystem.OnPositionChange += MinimapManager.Instance.UpdatePosition;

            characterSystem.HandleEventSpawn();
            characterSystem.HandleEventPositionChange();

            return characterSystem;
        }

        static public bool IsAlly(CharacterSystem characterSystemA, CharacterSystem characterSystemB)
        {
            return characterSystemA.GetProfile.GetTeamCharacter == characterSystemB.GetProfile.GetTeamCharacter;
        }

        static public bool IsEnemy(CharacterSystem characterSystemA, CharacterSystem characterSystemB)
        {
            return IsAlly(characterSystemA, characterSystemB) == false;
        }

        private void AddBehaviorBasedOnType(TypeBehavior typeBehavior, TypeCharacter typeCharacter)
        {
            switch (typeBehavior)
            {
                case TypeBehavior.Player:
                    StartCoroutine(AddPlayerBehaviorAffterSeconds(3f));
                    break;
                case TypeBehavior.Computer:
                    switch (typeCharacter)
                    {
                        case TypeCharacter.Hero:
                            StartCoroutine(AddAIBehaviorAffterSeconds(5f));
                            break;
                        case TypeCharacter.Legion:
                            StartCoroutine(AddAIBehaviorAffterSeconds(0.1f));
                            break;
                    }
                    break;
            }
        }

        private IEnumerator AddPlayerBehaviorAffterSeconds(float seconds)
        {
            CameraFollow.Create(this);

            yield return new WaitForSeconds(seconds);

            GameObject controlPanel = new GameObject(string.Format("Control Panel - {0}", this.gameObject.name),
                typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            controlPanel.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            controlPanel.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            MovementJoystick movementJoystick = MovementJoystick.Create(this, controlPanel.GetComponent<Canvas>());
            KDAUI.Create(this, controlPanel.GetComponent<Canvas>());

            this.gameObject.AddComponent<PlayerBehaviour>().SetMovementJoystick(movementJoystick);
        }

        private IEnumerator AddAIBehaviorAffterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            this.gameObject.AddComponent<FollowAIBehaviour>();
        }


        public CharacterSystem Clone()
        {
            var characterSystem = new CharacterSystem(this.profileData);
            return characterSystem;
        }

        //////////////////////////////////
        ////////// Handle Event //////////

        public void HandleEventSpawn()
        {
            this.isAlive = true;
            this.profile.HealthCurrent = this.profile.HealthMax.Value;
            this.profile.ManaCurrent = this.profile.ManaMax.Value;

            this.OnSpawn?.Invoke(this);
        }

        public void HandleEventPositionChange()
        {
            this.OnPositionChange?.Invoke(this);
        }

        public void HandleEventDie()
        {
            //BattleDiaglog.
            CharacterHistory.HandleKDAWhenDie(this);
            BattleDiaglog.Instance.ShowKillDialog(this);

            this.isAlive = false;

            this.enabled = false;
            this.statsBar.gameObject.SetActive(false);
            this.collider.enabled = false;

            this.animator.SetTrigger("Die");
            this.OnDie?.Invoke(this);
        }

        public void HandleEventRevival()
        {
            this.history.Reset();
            this.OnRevival?.Invoke(this);
        }

        public void HandleHealthChange()
        {
            this.CheckDie();
            this.OnHealthChange?.Invoke(this);
        }

        public void HandleEventTakeDame()
        {

        }

        public void HandleEventKDAChange()
        {
            this.OnKDAChange?.Invoke(this);
        }

        private void CheckDie()
        {
            if (this.profile.HealthCurrent <= 0)
            {
                HandleEventDie();
            }
        }

        public void Attack()
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, GetProfile.RangeAttack.Value);
            List<CharacterSystem> characterSystemEnemyColliders = new List<CharacterSystem>();

            foreach (var collider in colliders)
            {
                CharacterSystem characterSystemEnemyCollider = collider.GetComponent<CharacterSystem>();

                if (characterSystemEnemyCollider != null)
                {
                    if (IsEnemy(this, characterSystemEnemyCollider))
                    {
                        characterSystemEnemyColliders.Add(characterSystemEnemyCollider);
                        characterSystemEnemyCollider.TakeDame(this, 300f);
                    }
                }
            }
        }

        public void TakeDame(CharacterSystem characterHit, float amountDamage)
        {
            this.profile.HealthCurrent -= amountDamage;
            this.history.AddCharacterHit(characterHit);

            this.HandleHealthChange();
        }

    }
}