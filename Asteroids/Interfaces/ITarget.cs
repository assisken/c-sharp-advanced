namespace Asteroids.Interfaces
{
    public interface ITarget
    {
        public delegate void HitMessage(int score);

        public void Hit();
    }
}