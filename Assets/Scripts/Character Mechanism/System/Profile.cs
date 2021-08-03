using System;
using UnityEngine;

namespace CharacterMechanism.System
{
    /// <summary>
    /// Class containing all the locomotion settings
    /// </summary>
    [Serializable]
    public sealed class Profile
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        [SerializeField] TeamCharacter team;
        [SerializeField] TypeCharacter type;

        [SerializeField, Range(80f, 160f)] private float angularSpeed = 140f;
        [SerializeField, Range(0f, 4f)] private float movementSpeed = 2f;
        [SerializeField, Range(2f, 15f)] private float rangeAttack = 3f;

        //////////////////////////////
        ////////// Property //////////
        //////////////////////////////

        public TeamCharacter GetTeamCharacter { get => team; }
        public TypeCharacter GetTypeCharacter { get => type; }

        public float AngularSpeed => angularSpeed;
        public float MovementSpeed => movementSpeed;
        public float RangeAttack => rangeAttack;
    }
}