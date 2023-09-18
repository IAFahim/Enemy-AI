using System;
using Controller.ScriptAbles.Spawner;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Projectile
{
    public class TorpedoLauncher : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public Transform player;
        public GameObject lockOn;
        [SerializeReference] private ScriptablePool pool;

        private void OnValidate()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>(true);
            player ??= GameObject.FindWithTag("Player").transform;
        }
        
        [Button]
        public void DrawLineToPlayer()
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, player.position);
        }
        
        [Button]
        public void ResetLine()
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }

        public void Update()
        {
            
        }
    }
}