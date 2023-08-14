using JumpMan.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpMan.Controllers
{
    internal static class MovementController
    {
        public static Vector2 CalculateVelocity(Vector2 position, float angle, float speed, float deltatTime, float direction)
        {
            float x = position.X;
            float y = position.Y;
            x += speed * (float)Math.Cos(angle) * deltatTime * direction;
            y += speed * (float)Math.Sin(angle) * deltatTime * direction;

            return new Vector2(x,y);
        }

        public static Vector2 ClampPositionToBounds(Vector2 position, float leftBound, float topBound, float rightBound, float bottomBound)
        {
            if (position.X > rightBound)
            {
                position.X = rightBound;
            }

            if(position.X < leftBound)
            {
                position.X = leftBound;
            }

            if(position.Y > bottomBound)
            {
                position.Y = bottomBound;
            }

            if(position.Y < topBound)
            {
                position.Y = topBound;
            }

            return position;
        }

        public static float CalculateRotation(float angle, float rotationSpeed, float deltaTime, float direction)
        {
            float newAngle = angle;
            float completeRotation = (float)Math.PI * 2;

            newAngle += rotationSpeed * deltaTime * direction;

            // Stops amount from increase indefinitely if not checked and reset
            if (newAngle >= completeRotation)
            {
                newAngle = newAngle - completeRotation;
            }

            if(newAngle <= completeRotation * -1)
            {
                newAngle = newAngle + completeRotation;
            }

            return newAngle;
        }
    }
}
