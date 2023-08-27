using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Controller.Movement
{
    public class AISubmarineController : AIBoatController
    {
        public Animation subAnimation;
        public GameObject spotLight;
        protected override void OnValidate()
        {
            base.OnValidate();
            subAnimation = GetComponent<Animation>();
        }

        protected override void Start()
        {
            
            afterDetection += () =>
            {
                Surface();
            };
            base.Start();
        }
        
        public void Dive()
        {
            subAnimation.Play("Dive");
        }
        
        public void Surface()
        {
            subAnimation.Play("Surface");
        }

        public void ScanOneLastTime()
        {
            
        }
        
        public void ShootTorpedo()
        {
            subAnimation.Play("ShootTorpedo");
        }
        
    }
}