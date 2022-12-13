// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;

namespace PixelCrushers.Tutorials
{

    /// <summary>
    /// Sets a sprite's sortingOrder according to its Y position. The tutorials use this
    /// script so the project doesn't have to set a custom sorting axis in Project Settings.
    /// </summary>
    public class PixelCrushersTutorialSortByY : MonoBehaviour
    {

        public SpriteRenderer shadow;

        public float multiplier = 10f;

        private SpriteRenderer m_spriteRenderer;

        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            SetSortingOrder();
        }

        public void Update()
        {
            SetSortingOrder();
        }

        public void SetSortingOrder()
        {
            if (m_spriteRenderer != null)
            {
                m_spriteRenderer.sortingOrder = -Mathf.FloorToInt(transform.position.y * multiplier);
                if (shadow != null) shadow.sortingOrder = m_spriteRenderer.sortingOrder - 1;
            }
        }
    }

}