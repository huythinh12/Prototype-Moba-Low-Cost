using CharacterMechanism.System;
using CharacterMechanism.Information;

namespace CharacterMechanism.ScriptableObject
{
    /// <inheritdoc/>
    /// <summary>
    /// Base class to create an action condition
    /// </summary>
    public abstract class AActionCondition : UnityEngine.ScriptableObject
    {
        /// <summary>
        /// Verify is the action condition is fulfilled
        /// </summary>
        public abstract bool IsConditionFulfilled(CharacterSystem characterSystem, InputInformation inputInformation);
    }
}