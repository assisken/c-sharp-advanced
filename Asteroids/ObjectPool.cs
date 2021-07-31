using System;
using System.Collections.Generic;
using System.Drawing;
using Asteroids.BackgroundObjects;
using Asteroids.Interfaces;

namespace Asteroids
{
    public class ObjectPool
    {
        private List<BackgroundObject> _backgroundObjects = new();
        private List<Projectile> _projectiles = new();
        public Ship Ship;
        private BackgroundObject.Log _logger;
        private ITarget.HitMessage _hit;
        private BackgroundObject.Message _die;


        public ObjectPool(BackgroundObject.Log logger, ITarget.HitMessage hit, BackgroundObject.Message die)
        {
            _logger = logger;
            _hit = hit;
            _die = die;
        }

        public void Load()
        {
            _logger("Loading objects...");
            var random = new Random();

            Ship = new Ship(new Point(0, Game.MaxHeight / 2), new Point(0, 10), new Size(32, 20), 1,
                _logger, DestroyObject);
            _backgroundObjects.Add(Ship);
            _projectiles.Add(Ship);
            Ship.MessageDie += _die;

            for (var i = 0; i < 15; i++)
            {
                var x = random.Next(0, Game.Width);
                var y = random.Next(0, Game.Height);
                var xDirection = random.Next(-25, 25);
                var yDirection = random.Next(-25, 25);
                var size = random.Next(10, 40);
                var asteroid = new Asteroid(
                    new Point(x, y), new
                        Point(xDirection, yDirection),
                    new Size(size, size),
                    2, _logger, DestroyObject, _hit);
                _backgroundObjects.Add(asteroid);
                _projectiles.Add(asteroid);
            }

            for (var i = 0; i < 15; i++)
            {
                var x = random.Next(0, Game.Width);
                var y = random.Next(0, Game.Height);
                var size = random.Next(1, 5);
                _backgroundObjects.Add(
                    new Star(new Point(x, y), new Point(-i, 0), new Size(size, size), -3, _logger)
                );
            }

            for (var i = 0; i < 1; i++)
            {
                var x = random.Next(0, Game.Width);
                var y = random.Next(0, Game.Height);
                var size = random.Next(100, 1000);
                _backgroundObjects.Add(
                    new Planet(new Point(x, y), new Point(-10, 0), new Size(size, size), -1, _logger)
                );
            }

            for (var i = 0; i < 1; i++)
            {
                var x = random.Next(0, Game.Width);
                var y = random.Next(0, Game.Height);
                var size = random.Next(10, 100);
                _backgroundObjects.Add(
                    new Sun(new Point(x, y), new Point(-5, 0), new Size(size, size), -2, _logger)
                );
            }

            for (var i = 0; i < 5; i++)
            {
                var x = random.Next(0, Game.Width);
                var y = random.Next(0, Game.Height);
                var medkit = new Medkit(new Point(x, y), new Point(-5, 0), new Size(25, 18), 0, _logger, DestroyObject,
                    Ship.Heal);
                _backgroundObjects.Add(medkit);
                _projectiles.Add(medkit);
            }

            _backgroundObjects.Sort((o1, o2) => o1.Layer.CompareTo(o2.Layer));
        }

        public void createBullet()
        {
            var bullet = new Bullet(
                new Point(Ship.Rectangle.X + Ship.Size.Width + 10, Ship.Rectangle.Y + 4),
                new Point(4, 0),
                new Size(4, 1),
                0, _logger, DestroyObject
            );

            _backgroundObjects.Add(bullet);
            _projectiles.Add(bullet);
        }

        public void DrawAll()
        {
            foreach (var obj in _backgroundObjects)
                obj.Draw();
        }

        private void DestroyObject(Projectile obj)
        {
            _backgroundObjects.Remove(obj);
            _projectiles.Remove(obj);
        }

        public void UpdateAll()
        {
            foreach (var obj in _backgroundObjects)
                obj.Update();
        }

        public void ProceedCollisions()
        {
            foreach (var obj1 in _projectiles.ToArray())
            {
                foreach (var obj2 in _projectiles.ToArray())
                {
                    if (obj1 == obj2)
                        continue;

                    if (obj1.IntersectsWith(obj2))
                        obj1.Collide(obj2);
                }
            }
        }
    }
}