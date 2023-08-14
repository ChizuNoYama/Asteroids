using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JumpMan.Utilities
{
    public  static class CustomExtensions
    {
        public static void DrawOrigin(this SpriteBatch sb, Texture2D texture, Vector2 position, Vector2 origin)
        {
            sb.Draw(texture, position, null, Color.White, 0f, origin, Vector2.One, SpriteEffects.None, 0f);
        }

        public static void DrawOriginRotation(this SpriteBatch sb, Texture2D texture, Vector2 position, Vector2 origin, float rotation)
        {
            sb.Draw(texture, position, null, Color.White, rotation, origin, Vector2.One, SpriteEffects.None, 0f);
        }

        public static void DrawOriginRotationScale(this SpriteBatch sb, Texture2D texture, Vector2 position, Vector2 origin, float rotation, Vector2 scale)
        {
            sb.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        public static float NextFloat(this Random random, float min, float max)
        {
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }
    }
}
