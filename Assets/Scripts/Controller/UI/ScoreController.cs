using System;
using UnityEngine;

namespace Controller.UI
{
    public class ScoreController : MonoBehaviour
    {
        public Scores[] sideScoreModels;
        
        public void OnValidate()
        {
            sideScoreModels ??= gameObject.GetComponentsInChildren<Scores>(true);
        }

        public void OnEnable()
        {
            foreach (var scoreModel in sideScoreModels)
            {
                scoreModel.gameObject.SetActive(false);
            }
        }

        public void ShowScore(int index)
        {
            sideScoreModels[index].gameObject.SetActive(true);
        }
    }
}