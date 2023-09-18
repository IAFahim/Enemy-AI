using System.Linq;
using Controller.Star;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    [HideMonoScript, ExecuteInEditMode]
    public class StarView : MonoBehaviour
    {
        public StarManager starManager;
        public Image[] images;

        private void OnValidate()
        {
            images ??= GetComponentsInChildren<Image>(true).Where(img => img.gameObject != gameObject).ToArray();
        }

        private void OnEnable()
        {
            starManager.onStarChangedEvent.AddListener(SetStars);
        }

        private void OnDisable()
        {
            starManager.onStarChangedEvent.RemoveListener(SetStars);
        }
        
        [Button]
        public void SetStars(int stars)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].enabled = i < stars;
            }
        }
    }
}