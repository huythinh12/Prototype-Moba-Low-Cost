using CharacterMechanism.Attribute;
using CharacterMechanism.Behaviour;
using CharacterMechanism.Information;
using UnityEngine;

namespace CharacterMechanism.Behaviour
{
    /// <inheritdoc/>
    /// <summary>
    /// Example of follow AI behaviour using the generic character behaviour
    /// </summary>
    /// <remarks>
    /// ANavCharacterBehaviour is used because the script doesn't need collision and trigger detection
    /// </remarks>
    [DefaultExecutionOrder(1500)]
    public sealed class FollowAIBehaviour : ANavCharacterBehaviour
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        ///////////////////////////////////////
        ////////// Agent Information //////////

        [Header("Agent Information")]
        [ReadOnly, SerializeField] private Vector3 destinationPosition = Vector3.zero;

        ///////////////////////////////////
        ////////// Input Setting //////////

        [Header("Input Setting")]
        [SerializeField] private Transform target = null;

        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        //////////////////////////////
        ////////// Callback //////////

        ////////// Activation //////////

        protected override void OnDestroy()
        { }

        protected override void OnDisable()
        { }

        protected override void OnEnable()
        { }

        ////////// Information //////////

        protected override void ConfigureNavMeshAgent()
        {
            base.ConfigureNavMeshAgent();
            this.navMeshAgent.stoppingDistance = characterSystem.GetProfile.RangeAttack.Value;
        }

        protected override void InitializeInformation()
        {
            this.characterSystem.GetTargetsDetecter.OnTargetChange += SetTarget;
            this.SetTarget(characterSystem.GetTargetsDetecter.GetNextTransformTarget());
            this.navMeshAgent.SetDestination(target.position);
            this.destinationPosition = this.navMeshAgent.destination;
        }

        protected override void LoadInformationComponents()
        { }

        protected override void OverrideInputInformationReset(InputInformation inputInformation)
        { }

        protected override void UpdateInputInformation(InputInformation inputInformation)
        {
            //target = characterSystem.GetTargetsDetecter.GetNextTransformTarget();

            if (Vector3.Distance(this.destinationPosition, this.target.position) > this.navMeshAgent.stoppingDistance)
            {
                this.navMeshAgent.SetDestination(this.target.position);
                this.destinationPosition = this.navMeshAgent.destination;
            }

            inputInformation.MovementDirection = GetNextDirection;
        }

        private void SetTarget(Transform transform)
        {
            this.target = transform;
        }
    }
}