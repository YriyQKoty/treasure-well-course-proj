using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace GnomeBehaviour
{
    /// <summary>
    /// Відповідає за частини тіла головного персонажа
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class BodyPart : MonoBehaviour
    {
        //sprite for detached sprite
        [SerializeField] private Sprite detachedPart;

        //transform object for blood fountain]
        public Transform bloodFountainOrigin;

        private bool detached;

        //detaches object from parent and sets tag to remove all the physics
        public void Detach()
        {
            detached = true;

            this.tag = "Untagged";

            transform.SetParent(null, true);
        }

        // Update is called once per frame
        void Update()
        {
            // if part is not detached
            if (!this.detached)
            {
                return;
            }

            //if sleeping - destroy all components and object
            var rigidBody = GetComponent<Rigidbody2D>();

            if (rigidBody.IsSleeping())
            {
                DestroyComponents();
            }
        }

        private void DestroyComponents()
        {
            foreach (var joint in GetComponentsInChildren<HingeJoint2D>())
            {
                Destroy(joint);
            }

            foreach (var rigid in GetComponentsInChildren<Rigidbody2D>())
            {
                Destroy(rigid);
            }

            foreach (var collider in GetComponentsInChildren<Collider2D>())
            {
                Destroy(collider);
            }

            Destroy(this);
        }

        //method for deciding which sprite to render after being damaged
        public void ApplyDamage(Gnome.DamageType damageType)
        {
            Sprite actualSprite = null;

            //switch between damage type
            switch (damageType)
            {
                case Gnome.DamageType.Slicing:
                    actualSprite = detachedPart;
                    break;
            }

            //if sprite to use exists
            if (actualSprite != null)
            {
                //render sprite
                GetComponent<SpriteRenderer>().sprite = actualSprite;
            }
        }
    }
}