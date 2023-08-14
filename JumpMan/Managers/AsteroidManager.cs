using System.Text;
using JumpMan.Core;
using JumpMan.Generators;
using JumpMan.Models;

namespace JumpMan.Managers
{
    internal class AsteroidManager : SpawnableObjectManager<Asteroid>
    {
        public AsteroidManager(Game1 game) : base(game) { }

        protected override void ElapsedTimeAction()
        {
            if(this.SpawnableList.Count < 50)
            {
                Asteroid astreoid = AsteroidGenerator.Instance.GenerateAsteroid();
                this.SpawnableList.Add(astreoid);
            }
        }

        public override void CheckToRemove(Asteroid item)
        {
            if(this.SpawnableList.TryPeek(out item))
            {
                if (item.MarkToDestroy)
                {
                    this.SpawnableList.TryTake(out item);
                    return;
                }

                if ((item.Position.X - item.Texture.Width / 2) - 7 > Globals.GraphicsManager.PreferredBackBufferWidth)
                {
                    this.SpawnableList.TryTake(out item);
                    return;
                }

                if ((item.Position.X + item.Texture.Width / 2) + 7 < 0)
                {
                    this.SpawnableList.TryTake(out item);
                    return;
                }

                if((item.Position.Y - item.Texture.Height / 2) - 7 > Globals.GraphicsManager.PreferredBackBufferHeight)
                {
                    this.SpawnableList.TryTake(out item);
                    return;
                }

                if((item.Position.Y + item.Texture.Height / 2) + 7 < 0)
                {
                    this.SpawnableList.TryTake(out item);
                }
            }
        }
    }
}
