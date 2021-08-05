using UnityEngine;
using UnityEngine.AI;
using CharacterMechanism.System;

namespace CharacterMechanism.Behaviour
{
    /// <inheritdoc/>
    /// <summary>
    /// Base class to create a generic nav character behaviour
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class ANavCharacterBehaviour : ACharacterBehaviour
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        protected NavMeshAgent navMeshAgent = null;

        //////////////////////////////
        ////////// Property //////////
        //////////////////////////////

        /// <summary>
        /// Return the next local direction of the Nav Mesh Agent
        /// </summary>
        protected Vector3 GetNextDirection => navMeshAgent.desiredVelocity.normalized;

        /// <summary>
        /// Verify if the GameObject is arrived to his destination
        /// </summary>
        protected bool IsArrived => (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        //////////////////////////////
        ////////// Callback //////////

        /// <summary>
        /// Configure the Nav Mesh Agent
        /// </summary>
        /// <remarks>
        /// Call at the beginning after InitializeInformationComponents
        /// </remarks>
        protected virtual void ConfigureNavMeshAgent()
        {
            this.navMeshAgent.acceleration = 0.01f;
            this.navMeshAgent.angularSpeed = 0.01f;
            this.navMeshAgent.radius = 0.01f;
            this.navMeshAgent.speed = 0.01f;
            this.navMeshAgent.stoppingDistance = 2f;
            this.navMeshAgent.updatePosition = true;
            this.navMeshAgent.updateRotation = false;
        }

        //////////////////////////////
        ////////// Override //////////

        protected override void Awake()
        {
            this.navMeshAgent = GetComponent<NavMeshAgent>();
            base.Awake();
            this.ConfigureNavMeshAgent();
        }
    }
}

