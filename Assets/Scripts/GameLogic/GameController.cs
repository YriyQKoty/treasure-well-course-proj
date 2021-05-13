using System;
using System.Collections;
using System.Collections.Generic;
using GnomeBehaviour;
using HealthLogic;
using ObjectsBehaviours;
using RopeLogic;
using Singletons;
using SingletonTemplate;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;


namespace GameManagement
{
    /// <summary>
    /// Контролює проходження рівнів гри
    /// </summary>
    public class GameController : Singleton<GameController>
    {
        /// <summary>
        /// Делегат для створення гравця
        /// </summary>
        public delegate void GnomeEvent(GameObject obj);

        /// <summary>
        /// Подія - створити гравця
        /// </summary>
        public event GnomeEvent createPlayer;

        [Header("Object setups")]
        [Space]
        //starting point where Gnome spawns
        [SerializeField]
        public GameObject spawnPoint;

        //rope object to spawn
        /// <summary>
        /// Контроллер мотузки
        /// </summary>
        [SerializeField] public RopeControllerSimple rope;

        //object to follow by camera
        [SerializeField] private CameraFollow cameraFollow;

        //current sample of Gnome
        /// <summary>
        /// Поточний об'єкт гнома
        /// </summary>
        [SerializeField] public Gnome current;

        //prefab to create a Gnome object
        [SerializeField] private GameObject gnomePrefab;

        [Space]
        [Header("User interfaces")]

        //UI for main menu for "resume" or "restart:
        [SerializeField]
        private RectTransform mainMenu;

        //UI for gameplay with "up", "down" and "menu" buttons
        [SerializeField] private RectTransform gameplayMenu;

        //UI for gameover (victory)
        [SerializeField] private RectTransform gameOverMenu;

        /// <summary>
        /// Останній сегмент мотузки
        /// </summary>
        public GameObject TopRopeSegment;

        //state for testing
        /// <summary>
        /// Перевірка чи увімкнено безсмертя
        /// </summary>
        public bool IsInvincible { get; set; }

        [SerializeField] private float delayAfterDeath;

        [Space] [Header("Audio files")] [SerializeField]
        private AudioClip gameOverSound;

        [SerializeField] private AudioClip gnomeDied;

        public GameObject body;

        void Start()
        {
            Reset();
        }

        private void Reset()
        {
            //game over menu ui-> disable
            if (gameOverMenu)
            {
                gameOverMenu.gameObject.SetActive(false);
            }

            //main menu ui -> disable
            if (mainMenu)
            {
                mainMenu.gameObject.SetActive(false);
            }

            //enable gameplay UI
            if (gameplayMenu)
            {
                gameplayMenu.gameObject.SetActive(true);
            }

            //get all objects with Resetter component

            var resettableObjects = GetComponents<Resetter>();

            //for each object Reset their state to a default
            foreach (var obj in resettableObjects)
            {
                obj.Reset();
            }

            CreateGnome();

            Time.timeScale = 1.0f;
        }

        /// <summary>
        /// Створює гнома-леприкона
        /// </summary>
        public void CreateGnome()
        {
            //removing gnome
            RemoveGnome();

            //after removing old gnome -> create new
            current = Instantiate(gnomePrefab, spawnPoint.transform.position, Quaternion.identity)
                .GetComponent<Gnome>();

            Health.Instance.ResetHealth();

            var joint = GameObject.FindWithTag("Connection");

            TopRopeSegment = GameObject.Find("TopRopeSegment");

            if (joint != null)
            {
                TopRopeSegment.GetComponent<SpringJoint2D>().connectedBody =
                    joint.GetComponent<Rigidbody2D>();
            }

            rope.gameObject.SetActive(true);

            rope.whatIsHangingFromTheRope = joint.transform;

            rope.ResetLength();

            cameraFollow.followedObject = current.bodyPartToFollow;

            createPlayer?.Invoke(current.gameObject);
        }

        void RemoveGnome()
        {
            //if invincible - skip
            if (IsInvincible)
            {
                return;
            }

            //else
            if (current != null)
            {
                Destroy(GameObject.FindWithTag("Connection"));

                current.HoldingTreasure = false;

                current.gameObject.tag = "Untagged";

                foreach (Transform child in current.transform)
                {
                    child.gameObject.tag = "Untagged";
                }

                current = null;
            }
        }

        void KillGnome(Gnome.DamageType damageType)
        {
            var audio = GetComponent<AudioSource>();
            if (audio)
            {
                audio.PlayOneShot(this.gnomeDied);
            }

            if (!IsInvincible && Health.Instance.CurrentHealth == 0)
            {
                current.DestroyGnome(damageType);

                RemoveGnome();

                StartCoroutine(RemoveAfterDelay());
            }
        }


        //coroutine for resetting after destroying gnome
        IEnumerator RemoveAfterDelay()
        {
            cameraFollow.followedObject = null;
            rope.RemoveRope();
            yield return new WaitForSeconds(delayAfterDeath);
            Reset();
        }

        //call if Trap touched
        /// <summary>
        /// Спрацьовує, якщо торкнувся пастки
        /// </summary>
        public void TrapTouched()
        {
            KillGnome(Gnome.DamageType.Slicing);
        }

        //change sprite 
        /// <summary>
        /// Спрацбовує, якщо торкнувся скарбу
        /// </summary>
        public void TreasureCollected()
        {
            current.HoldingTreasure = true;
        }

        /// <summary>
        /// Вихід зі гри
        /// </summary>
        public void Exit()
        {
            //if gnome is alive and holding treasure
            if (current != null && current.HoldingTreasure)
            {
                var audio = GetComponent<AudioSource>();

                //if audio source exists
                if (audio)
                {
                    audio.PlayOneShot(gameOverSound);
                }

                //pause
                Time.timeScale = 0.0f;
                gameplayMenu.gameObject.SetActive(false);
                MenuController.OpenGameMenu(MenuController.MenuType.Victory);
            }
        }

        /// <summary>
        /// Повертає у меню
        /// </summary>
        public void BackToMenu()
        {
            SceneManager.LoadSceneAsync(0); //load main scene
        }

        //if Menu or Pause button was pressed
        /// <summary>
        /// Ставить на паузу
        /// </summary>
        /// <param name="paused">чи на паузі</param>
        public void SetPause(bool paused)
        {
            if (paused) //&& !isPause
            {
                Time.timeScale = 0.0f;

                gameplayMenu.gameObject.SetActive(false);
                MenuController.OpenGameMenu(MenuController.MenuType.Pause);
            }
            else
            {
                Time.timeScale = 1.0f;
                gameplayMenu.gameObject.SetActive(true);
            }
        }
    }
}