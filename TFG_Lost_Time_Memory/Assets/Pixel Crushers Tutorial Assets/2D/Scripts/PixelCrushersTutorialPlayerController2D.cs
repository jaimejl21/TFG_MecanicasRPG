// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PixelCrushers.Tutorials
{

    /// <summary>
    /// Simple 2D player controller.
    /// </summary>
    public class PixelCrushersTutorialPlayerController2D : MonoBehaviour
    {

        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";

        [Tooltip("The fastest the player can travel left and right.")]
        public float maxHorizontalSpeed = 8f;

        [Tooltip("The fastest the player can travel up and down.")]
        public float maxVerticalSpeed = 5f;

        [Tooltip("Tracks which direction the player is facing.")]
        public bool facingLeft = false;

        private Rigidbody2D m_rigidbody2D;
        private Animator m_animator;

        public const string RunParameter = "Run";

        private void Awake()
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
            m_animator = GetComponent<Animator>();
            var m_sortByY = GetComponent<PixelCrushersTutorialSortByY>();
            if (m_rigidbody2D == null) Debug.LogError("No Rigidbody2D found on " + name, this);
            if (m_animator == null) Debug.LogError("No Animator found on " + name, this);
            if (m_sortByY == null) m_sortByY = gameObject.AddComponent<PixelCrushersTutorialSortByY>();
        }

        private void FixedUpdate()
        {
            // Move the character:
            var move = new Vector2(InputDeviceManager.GetAxis(horizontalAxis) * maxHorizontalSpeed, InputDeviceManager.GetAxis(verticalAxis) * maxVerticalSpeed);
            m_rigidbody2D.velocity = move;

            // Update the animator:
            m_animator.SetBool(RunParameter, move.magnitude > 0.1f);

            // Flip the character if necessary:
            var needToFlip = ((move.x < 0 && !facingLeft) || (move.x > 0 && facingLeft));
            if (needToFlip)
            {
                facingLeft = !facingLeft;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
    }
}