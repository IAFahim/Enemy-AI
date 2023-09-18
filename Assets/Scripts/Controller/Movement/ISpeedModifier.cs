using UnityEngine.Events;

namespace Controller.Movement
{
    public interface ISpeedModifier
    {
        public void SetSpeedModifier(float value);
        public void BalanceSpeedOverTime();

    }
}