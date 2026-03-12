namespace JustMobyTest.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Player/Stats", fileName = nameof(PlayerStatsCollection))]
    public class PlayerStatsCollection : ScriptableObject
    {
        [field: SerializeField, SerializeReference, SubclassSelector]
        public List<APlayerStats> Stats { get; private set; }
    }
}