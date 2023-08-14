using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using JumpMan.Models;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace JumpMan.Managers
{
    // Make a 
    public abstract class SpawnableObjectManager<T> where T: SpawnableObject
    {
        public SpawnableObjectManager(Game1 game)
        {
            SpawnableList = new ConcurrentBag<T>();
            SpawnPoints = new List<Vector2>();
            
            _timer = new Timer(1000);
            _timer.Elapsed += OnElapsedTime;
            _timer.Start();
        }

        private Timer _timer;
        public ConcurrentBag<T> SpawnableList { get; private set; }
        public List<Vector2> SpawnPoints { get; private set; }
        public Game1 Game { get; private set; }

        protected abstract void ElapsedTimeAction();

        protected void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            this.ElapsedTimeAction();
        }

        public abstract void CheckToRemove(T item);

        public virtual void CheckCollisionWithObject(SpawnableObject item)
        {
            RectangleF itemDetector = item.CreateColliderDetector();
            foreach (SpawnableObject collider in SpawnableList)
            {
                RectangleF colliderDectector = collider.CreateColliderDetector();
                if( collider != item && itemDetector.IntersectsWith(colliderDectector))
                {
                    item.OnCollision(collider);
                }
            }
        }
    }
}
