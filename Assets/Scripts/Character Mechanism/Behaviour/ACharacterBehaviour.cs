using UnityEngine;
using CharacterMechanism.System;
using CharacterMechanism.Information;

namespace CharacterMechanism.Behaviour
{
    /// <inheritdoc />
    /// <summary>
    /// Base class to create a generic character behaviour
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterSystem))]
    public abstract class ACharacterBehaviour : MonoBehaviour
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        protected CharacterSystem characterSystem = null;

        ///////////////////////////
        ////////// Method //////////
        ////////////////////////////

        //////////////////////////////
        ////////// Callback //////////

        ////////// Activation //////////

        protected abstract void OnDestroy();

        protected abstract void OnDisable();

        protected abstract void OnEnable();

        ////////// Information //////////

        /// <summary>
        /// Load all the components use as information
        /// </summary>
        /// <remarks>
        /// Call at beginning before InitializeInformation
        /// </remarks>
        protected abstract void LoadInformationComponents();

        /// <summary>
        /// Initialize all the information used to drive the action
        /// </summary>
        /// <remarks>
        /// Call at the beginning after LoadInformationComponents
        /// </remarks>
        protected abstract void InitializeInformation();

        /// <summary>
        /// Override the reset method of the input information
        /// </summary>
        /// <remarks>
        /// Note that InputInformation.Reset still happen before. Call every Update before UpdateInputInformation
        /// </remarks>
        protected abstract void OverrideInputInformationReset(InputInformation inputInformation);

        /// <summary>
        /// Update the required input information used to drive the action
        /// </summary>
        /// <remarks>
        /// Call every Update after OverrideInputInformationReset
        /// </remarks>
        protected abstract void UpdateInputInformation(InputInformation inputInformation);

        ////////////////////////////////////////////
        ////////// MonoBehaviour Callback //////////

        protected virtual void Awake()
        {
            this.characterSystem = GetComponent<CharacterSystem>();
            this.LoadInformationComponents();
            this.InitializeInformation();
        }

        protected virtual void Update()
        {
            this.characterSystem.InputInformation.Reset();
            this.OverrideInputInformationReset(this.characterSystem.InputInformation);
            this.UpdateInputInformation(this.characterSystem.InputInformation);
        }
    }

}

