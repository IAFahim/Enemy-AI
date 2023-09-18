using TriInspector;
using UnityEngine;

namespace Model.Info
{
    [HideMonoScript]
    public class Title : MonoBehaviour
    {
        [SerializeReference] private string note;

        public static implicit operator string(Title title)
        {
            return title.note;
        }

        public override string ToString()
        {
            return note;
        }
    }
}