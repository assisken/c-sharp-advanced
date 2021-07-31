namespace Asteroids.Interfaces
{
    public interface IHeal
    {
        public delegate void HealMessage(int hp);

        public void Heal();
    }
}