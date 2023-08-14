using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace JumpMan.Models;

public class Projectile : SpawnableObject
{
    public Projectile(int damage, Texture2D texture, Vector2 position, float speed, float velocityAngle, float rotationAngle, float rotationSpeed, float scale, bool isSolid = true) 
        : base(texture, position, speed, velocityAngle, rotationAngle, rotationSpeed, scale, isSolid)
    {
        this.Damage = damage;
    }

    public int Damage { get; set; }

    public override void OnCollision(SpawnableObject collider)
    {
        if (collider is Asteroid)
        {
            this.MarkToDestroy = true;
        }
    }
}