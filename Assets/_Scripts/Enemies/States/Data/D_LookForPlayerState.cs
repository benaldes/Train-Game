
    using UnityEngine;

    [CreateAssetMenu(fileName = "NewLookForPlayerStateData",menuName = "Data/State Data/Look For Player State Data")]
    public class D_LookForPlayerState : ScriptableObject
    {
        public int AmountOfTurns = 2;
        public float TimeBetweenTurns = 0.75f;
    }
