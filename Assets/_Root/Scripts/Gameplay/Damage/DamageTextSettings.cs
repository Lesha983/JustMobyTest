namespace JustMobyTest.Gameplay
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "DamageTextSettings", fileName = nameof(DamageTextSettings))]
    public class DamageTextSettings : ScriptableObject
    {
        [field: SerializeField]
        public DamageText DamageTextPrefab { get; private set;}
    }
}