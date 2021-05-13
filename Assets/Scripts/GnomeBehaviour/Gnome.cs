using System.Collections;
using System.Collections.Generic;
using ObjectsBehaviours;
using Singletons;
using UnityEngine;

namespace GnomeBehaviour
{
    /// <summary>
    /// Відповідає за логіку головного персонажа
    /// </summary>
    public class Gnome : MonoBehaviour
    {
        /// <summary>
        /// Частина тіла, за якою слідкуватиме камера
        /// </summary>
        public Transform bodyPartToFollow;

        /// <summary>
        /// Фізичне тіло точки з'єднання із вірьовкою
        /// </summary>
        public Rigidbody2D connectionToRope;
        
        [SerializeField] private GameObject bloodFountain;

        //Sprites for empty arm or with treasure
        [Space] [Header("Sprites")] [SerializeField]
        private Sprite handEmpty;

        [SerializeField] private Sprite handTreasure;
        
        [SerializeField] private SpriteRenderer holdingHand;

        [SerializeField] private float delayBeforeRemoving;

        private bool dead;

        private bool _holdingTreasure = false;

        //property for getting/setting whether Gnome holds treasure or not
        /// <summary>
        /// Перевіряє чи тримає герой скарб
        /// </summary>
        public bool HoldingTreasure
        {
            get => _holdingTreasure;
            set
            {
                if (dead)
                {
                    return;
                }

                _holdingTreasure = value;
                //if renderer not empty - set render due to state
                if (holdingHand != null)
                {
                    holdingHand.sprite = _holdingTreasure ? handTreasure : handEmpty;
                }
            }
        }

        //enumerator for damageType
        /// <summary>
        /// Тип шкоди
        /// </summary>
        public enum DamageType
        {
            Slicing
        }

        /// <summary>
        /// Видалення гнома-леприкона
        /// </summary>
        /// <param name="type">Тип шкоди</param>
        public void DestroyGnome(DamageType type)
        {
            HoldingTreasure = false;

            dead = true;

            foreach (BodyPart part in GetComponentsInChildren<BodyPart>())
            {
                switch (type)
                {
                    case DamageType.Slicing:
                    {
                        part.ApplyDamage(type);

                        break;
                    }
                }

                //one of three chances to detach
                bool shouldDetach = Random.Range(0, 2) == 0;

                if (shouldDetach)
                {
                    //detach all the components
                    part.Detach();

                    if (type == DamageType.Slicing)
                    {
                        if (bloodFountain != null && part.bloodFountainOrigin)
                        {
                            var fountain = Instantiate(bloodFountain, part.bloodFountainOrigin.position,
                                part.bloodFountainOrigin.rotation);

                            fountain.transform.SetParent(bodyPartToFollow, false);
                        }
                    }
                }


                //detach all joints
                var allJoints = part.GetComponentsInChildren<HingeJoint2D>();
                foreach (var joint in allJoints)
                {
                    Destroy(joint);
                }

                foreach (Transform child in transform)
                {
                    var removeAfterDelay = child.gameObject.AddComponent<RemoveAfterDelay>();
                    removeAfterDelay.delay = delayBeforeRemoving;
                }

                var remove = this.gameObject.AddComponent<RemoveAfterDelay>();
                remove.delay = delayBeforeRemoving;
            }
        }
    }
}