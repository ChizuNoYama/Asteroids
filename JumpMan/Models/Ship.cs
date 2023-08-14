using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpMan.Models;

public class Ship : SpawnableObject
{
    public Ship(Texture2D texture, Vector2 position, float speed, float velocityAngle, float rotationAngle, float rotationSpeed, float scale, bool isSolid = true)
        : base(texture, position, speed, velocityAngle, rotationAngle, rotationSpeed, scale, isSolid)
    {
    }

    public override void OnCollision(SpawnableObject item)
    {
        throw new System.NotImplementedException();
    }
}