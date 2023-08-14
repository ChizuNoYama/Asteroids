using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using JumpMan.Controllers;
using JumpMan.Utilities;

namespace JumpMan.Models
{
    public class Asteroid : SpawnableObject
    {
        public Asteroid(Texture2D texture, Vector2 position, float speed, float velocityAngle, float rotationAngle, float rotationSpeed, float scale, bool isSolid = true)
            : base(texture, position, speed, velocityAngle, rotationAngle, rotationSpeed, scale, isSolid)
        {
            _health = 3;
        }

        private int _health;
        protected int Health
        {
            get { return _health; }
        }

        public void Update(float deltaTime)
        {
            this.Position = MovementController.CalculateVelocity(this.Position, this.VelocityAngle, this.Speed, deltaTime, PosNegMultiplier.Positive);
            this.RotationAngle = MovementController.CalculateRotation(this.RotationAngle, this.RotationSpeed, deltaTime, PosNegMultiplier.Positive);
        }

        public override void OnCollision(SpawnableObject collider)
        {
            if (collider is Asteroid || collider is Ship)
            {
                this.VelocityAngle -= (float)Math.PI;
            }

            if (collider is Projectile)
            {
                Projectile projectile = (Projectile)collider;
                _health -= projectile.Damage;
                if (_health == 0)
                {
                    this.MarkToDestroy = true;
                }
            }
        }
    }
}
