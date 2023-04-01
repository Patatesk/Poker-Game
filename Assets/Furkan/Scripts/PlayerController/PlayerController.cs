using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace PK.PokerGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        private Vector3 playerVelocity;
        private AnimationController anim;
        private JoystickController input;
        private CharacterController characterController;
        private float gravityValue = -9.81f;

        public bool applyGravity = true;
        public bool canMove = true;

        private void Awake()
        {
            input = new JoystickController();
            characterController = GetComponent<CharacterController>();
            anim = GetComponent<AnimationController>();
        }
        private void OnEnable()
        {
            input.Enable();
        }
        private void OnDestroy()
        {
            input.Disable();
        }

        public void Obstackle()
        {
            canMove = false;
            anim.FallAnim();
            applyGravity = false;
            Invoke("ChangeCanMove", 3f);
        }

        private void ChangeCanMove()
        {
            canMove = true;
        }

        void Update()
        {
            if (!canMove) return;
            if (characterController.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            Vector2 moveInput = input.Player.RotateAndMove.ReadValue<Vector2>();
            Vector3 move = new Vector3(-moveInput.x, 0, -moveInput.y);
            characterController.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player..
            if (applyGravity)
            {
                playerVelocity.y += gravityValue * Time.deltaTime;
                characterController.Move(playerVelocity * Time.deltaTime);

            }
            if (move.magnitude > 0f)
            {
                anim.MoveAnim();
            }
            else
            {
                anim.IdleAnim();
            }

        }

    }
}
