using TriInspector;
using UnityEngine;

namespace Model.Info
{
    [HideMonoScript]
    public class Description : MonoBehaviour
    {
        [TextArea(15, 20)] [SerializeReference]
        private string note;

        public static implicit operator string(Description description)
        {
            return description.note;
        }
        
        public override string ToString()
        {
            return note;
        }
    }
}