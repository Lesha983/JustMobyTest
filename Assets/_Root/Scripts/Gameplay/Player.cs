namespace JustMobyTest.Gameplay
{
    using Input;
    using UnityEngine;
    using Zenject;

    public class Player : MonoBehaviour
    {
        [Inject]
        private IInputHandler InputHandler { get; set; }
        
        [SerializeField]
        private CharacterController controller;
        [SerializeField]
        private float speed;

        private void OnEnable()
        {
            InputHandler.OnMove += OnMove;
            InputHandler.OnAttack += OnAttack;
        }

        private void OnDisable()
        {
            InputHandler.OnMove -= OnMove;
            InputHandler.OnAttack -= OnAttack;
        }

        private void OnMove(Vector2 value)
        {
            var direction = new Vector3(value.x, 0, value.y);
            controller.Move(direction * speed * Time.deltaTime);
            // transform.position += direction * speed * Time.deltaTime;
        }

        private void OnAttack()
        {
            Debug.Log($"Attack!");
        }
    }
}