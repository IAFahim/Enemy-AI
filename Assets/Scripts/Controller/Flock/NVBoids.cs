using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controller.Flock
{
    public class NvBoids : MonoBehaviour
    {
        [Header("General Settings")] 
        public Vector2 behavioralCh = new Vector2(2.0f, 6.0f);
        public bool debug;

        [Header("Boat Settings")] 
        public GameObject boatPrefab;
        [Range(1, 9999)] public int boatsNum = 10;
        [Range(0, 100)] public float boatSpeed = 1;
        [Range(0, 1)] public float soaring = 0.5f;
        public bool rotationClamp = true;
        public float separationDistance = 3.0f; // Minimum separation distance between boats

        [Header("Player Target")] 
        public Transform playerTarget;

        // Private variables
        Transform _thisTransform;
        Transform[] _boatsTransform;
        private float[] _boatsSpeed, _boatsSpeedCur;
        static WaitForSeconds _delay0;

        void Awake()
        {
            _thisTransform = transform;
            CreateBoats();
            StartCoroutine(BehavioralChange());
        }

        void LateUpdate()
        {
            BoatsMove();
        }

        void BoatsMove()
        {
            float deltaTime = Time.deltaTime;
            float translateSpeed = boatSpeed * deltaTime;
            float rotationSpeed = soaring * deltaTime;

            for (int b = 0; b < boatsNum; b++)
            {
                AvoidObstacles(b); // Check for and avoid other boats
                float speedDiff = _boatsSpeed[b] - _boatsSpeedCur[b];

                if (Mathf.Abs(speedDiff) > 0.01f)
                {
                    _boatsSpeedCur[b] += Mathf.Sign(speedDiff) * Mathf.Min(Mathf.Abs(speedDiff), 0.5f);
                }

                Vector3 targetPosition = playerTarget.position - _boatsTransform[b].position;
                Quaternion rotationCur =
                    Quaternion.LookRotation(Vector3.RotateTowards(_boatsTransform[b].forward, targetPosition, rotationSpeed, 0),
                        Vector3.up);

                if (rotationClamp)
                {
                    Vector3 eulerAngles = rotationCur.eulerAngles;
                    eulerAngles.x = eulerAngles.z = 0;
                    rotationCur = Quaternion.Euler(eulerAngles);
                }

                _boatsTransform[b].rotation = rotationCur;
                _boatsTransform[b].Translate(Vector3.forward * (_boatsSpeedCur[b] * translateSpeed), Space.Self);
            }
        }

        void AvoidObstacles(int currentBoatIndex)
        {
            for (int b = 0; b < boatsNum; b++)
            {
                if (b != currentBoatIndex)
                {
                    float distance = Vector3.Distance(_boatsTransform[currentBoatIndex].position, _boatsTransform[b].position);

                    if (distance < separationDistance)
                    {
                        Vector3 avoidDirection = _boatsTransform[currentBoatIndex].position - _boatsTransform[b].position;
                        avoidDirection.Normalize();
                        avoidDirection.y = 0; // Keep it in the XZ plane

                        // Apply avoidance force to steer away from other boats
                        _boatsTransform[currentBoatIndex].Translate(avoidDirection * 0.1f, Space.Self);
                    }
                }
            }
        }

        IEnumerator BehavioralChange()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(behavioralCh.x, behavioralCh.y));

                for (int b = 0; b < boatsNum; b++)
                {
                    _boatsSpeed[b] = Random.Range(3.0f, 7.0f);
                }
            }
        }

        void CreateBoats()
        {
            _boatsTransform = new Transform[boatsNum];
            _boatsSpeed = new float[boatsNum];
            _boatsSpeedCur = new float[boatsNum];

            for (int b = 0; b < boatsNum; b++)
            {
                _boatsTransform[b] = Instantiate(boatPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                    Quaternion.identity, _thisTransform).transform;
                _boatsTransform[b].localRotation = Quaternion.Euler(0, Random.value * 360, 0);
                _boatsSpeed[b] = Random.Range(3.0f, 7.0f);
            }
        }
    }
}
