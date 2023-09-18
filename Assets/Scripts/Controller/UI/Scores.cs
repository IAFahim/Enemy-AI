using BrunoMikoski.AnimationSequencer;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.UI
{
    [HideMonoScript, RequireComponent(typeof(AnimationSequencerController))]
    public class Scores : MonoBehaviour
    {
        public int index;
        public ScoreController scoreController;
        public AnimationSequencerController animationSequencerController;
        public Image backgroundImage;
        public Image sideBarImage;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI text;
        public virtual void OnValidate()
        {
            scoreController ??= GetComponentInParent<ScoreController>();
            animationSequencerController ??= GetComponent<AnimationSequencerController>();
            gameObject.FindAndAttachTwoComponents(ref backgroundImage, ref sideBarImage);
            gameObject.FindAndAttachTwoComponents(ref scoreText, ref text);
        }

        private void OnEnable()
        {
            // animationSequencerController.OnFinishedEvent.AddListener();
        }

        private void OnDisable()
        {
            // animationSequencerController.OnFinishedEvent.RemoveListener(onFinish.Invoke);
        }
    }
}