using System;
using DG.Tweening;
using UnityEngine;

namespace Controller.Effect
{
    public class LockInEffect : MonoBehaviour
    {
        public GameObject target;
        public Animator animator;
        public Camera camera;

        private void OnValidate()
        {
            animator = GetComponent<Animator>();
        }

        private void Awake()
        {
            camera = Camera.main;
        }

        public async void Start()
        {
            await transform.DOMove(target.transform.position, 0.5f).AsyncWaitForCompletion();
            animator.enabled = true;
        }

        public void ScreenShake()
        {
            camera.transform.DOShakePosition(10.5f, 0.5f, 10, 90, false, true).onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        private void OnDisable()
        {
            animator.enabled = false;
            transform.DOKill();
        }
    }
}