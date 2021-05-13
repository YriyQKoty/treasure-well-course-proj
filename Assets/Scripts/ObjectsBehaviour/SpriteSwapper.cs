using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectsBehaviours
{
    /// <summary>
    /// Відповідає за зміну спрайтів ігрового об'єкта
    /// </summary>
    public class SpriteSwapper : MonoBehaviour
    {
        /// <summary>
        /// The sprite to use
        /// </summary>
        public Sprite spriteToUse;

        /// <summary>
        /// The sprite renderer
        /// </summary>
        public SpriteRenderer spriteRenderer;
        private Sprite originalSprite;

        /// <summary>
        /// Swaps the sprite.
        /// </summary>
        public void SwapSprite()
        {
            if (spriteToUse != spriteRenderer.sprite)
            {
                originalSprite = spriteRenderer.sprite;
                spriteRenderer.sprite = spriteToUse;
            }
        }

        /// <summary>
        /// Resets the sprite.
        /// </summary>
        public void ResetSprite()
        {
            if (originalSprite != null)
            {
                spriteRenderer.sprite = originalSprite;
            }
        }
    }

}
