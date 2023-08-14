using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JumpMan.Models
;

public abstract class SpawnableObject
{
    protected SpawnableObject(Texture2D texture, Vector2 position, float speed, float velocityAngle, float rotationAngle, float rotationSpeed, float scale, bool isSolid)
    {
        this.Texture = texture;
        this.Position = position;
        this.IsSolid = isSolid;
        this.VelocityAngle = velocityAngle;
        this.RotationAngle = rotationAngle;
        this.RotationSpeed = rotationSpeed;
        this.Speed = speed;
        this.Scale = scale;
        this.IsSolid = isSolid;
    }
    
    #region Properties
    
    public Texture2D Texture { get; protected set; }
    public Vector2 Position { get; set; }
    public float VelocityAngle { get; set;}
    public float RotationAngle { get; set; }
    public float RotationSpeed { get; set; }
    public float Speed { get; set; }
    public float Scale { get; set; }
    public bool IsSolid { get; protected set; }
    public bool MarkToDestroy { get; set; } = false;
    
    #endregion Properties

    #region Methods
    
    public abstract void OnCollision(SpawnableObject collider);

    public virtual RectangleF CreateColliderDetector()
    {
        return new RectangleF(x: (int)Math.Ceiling(this.Position.X), 
                              y: (int)Math.Ceiling(this.Position.Y),
                             width: this.Texture.Width * this.Scale,
                             height: this.Texture.Height * this.Scale);
    }
    
    #endregion Methods
}