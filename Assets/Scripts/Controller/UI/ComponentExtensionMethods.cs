using UnityEngine;

namespace Controller.UI
{
    public static class ComponentExtensionMethods
    {
        public static void FindAndAttachTwoComponents<T>(this GameObject gameObject,ref T component1, ref T component2) where T : Component
        {
            var componentsInChild = gameObject.GetComponentsInChildren<T>(true);
            if (componentsInChild.Length < 2) return; 
            component1 ??= componentsInChild[0];
            component2 ??= componentsInChild[1];
        }
        
        public static void FindThreeComponents<T> (this GameObject gameObject,ref T component1, ref T component2, ref T component3) where T : Component
        {
            var componentsInChild = gameObject.GetComponentsInChildren<T>(true);
            if (componentsInChild.Length < 3) return; 
            component1 ??= componentsInChild[0];
            component2 ??= componentsInChild[1];
            component3 ??= componentsInChild[2];
        }
    }
}