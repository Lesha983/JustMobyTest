namespace JustMobyTest.Gameplay
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Player/Settings", fileName = nameof(PlayerSettings))]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField]
        public float Health { get; private set; }
        [field: SerializeField]
        public float Damage { get; private set; }
        [field: SerializeField]
        public float Speed { get; private set; }
        [field: Space]
        [field: SerializeField]
        public float Sensitivity { get; private set; }
        [field: SerializeField]
        public float AimSensitivity { get; private set; }
        [field: SerializeField]
        public Vector2 VerticalRotationClamp { get; private set; }
    }
}