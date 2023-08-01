namespace Model.Health
{
    public interface IHealth
    {
        public float Value { get; set; }
        public void TakeDamage(float damage, bool invoke = true);
        public void SetHealth(float health, bool invoke = true);
        public void Die(bool invoke = true);
    }
}