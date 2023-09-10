using System.Collections;
using UnityEngine;

namespace Controller.Flock
{
    public class NvBoidsRb : MonoBehaviour
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
        Rigidbody[] _boatsRigidbody;
        Transform[] _boatsTransform;
        float[] _boatsSpeed, _boatsSpeedCur;
        static WaitForSeconds _delay0;

        void Awake()
        {
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
            float rotationSpeed = soaring * deltaTime;

            for (int b = 0; b < boatsNum; b++)
            {
                AvoidObstacles(b); // Check for and avoid other boats
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

                _boatsRigidbody[b].rotation = rotationCur;
                _boatsRigidbody[b].AddForce(_boatsTransform[b].forward * (_boatsSpeedCur[b] * boatSpeed), ForceMode.VelocityChange);
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
                        _boatsRigidbody[currentBoatIndex].AddForce(avoidDirection * 0.1f, ForceMode.VelocityChange);
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
            _boatsRigidbody = new Rigidbody[boatsNum];
            _boatsTransform = new Transform[boatsNum];
            _boatsSpeed = new float[boatsNum];
            _boatsSpeedCur = new float[boatsNum];

            for (int b = 0; b < boatsNum; b++)
            {
                GameObject boatObj = Instantiate(boatPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)),
                    Quaternion.identity);
                _boatsRigidbody[b] = boatObj.GetComponent<Rigidbody>();
                _boatsTransform[b] = boatObj.transform;
                _boatsTransform[b].localRotation = Quaternion.Euler(0, Random.value * 360, 0);
                _boatsSpeed[b] = Random.Range(3.0f, 7.0f);
            }
        }
    }
}