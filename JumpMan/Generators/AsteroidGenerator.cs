using JumpMan.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using JumpMan.Models;

namespace JumpMan.Generators
{
    public class AsteroidGenerator
    {
        private AsteroidGenerator(float difficultyMultiplier, Texture2D texture, int xConstraint, int yConstraint)
        {
            _difficultyMultiplier = difficultyMultiplier;
            _texture = texture;
            _xConstraint = xConstraint;
            _yConstraint = yConstraint;
            
        }

        private float _difficultyMultiplier;
        private Texture2D _texture;
        private int _xConstraint;
        private int _yConstraint;


        public static AsteroidGenerator Instance { get; private set; }

        public static void Initialize(float difficultyMultiplier, Texture2D texture, int xPositionConstraint, int yPositionConstraint)
        {
            Instance = new AsteroidGenerator(difficultyMultiplier, texture, xPositionConstraint, yPositionConstraint);
        }

        public Asteroid GenerateAsteroid()
        {
            Random randomizer = new Random();
            float speed = randomizer.NextSingle() * 100f + 50f;
            float rotationSpeed = 0.5f * randomizer.Next(1,10);
            float scale = 0.1f + randomizer.NextSingle();

            StartingRegion region = (StartingRegion)randomizer.Next(1, 5);
            Vector2 startingPosition;
            int centerX = _xConstraint / 2;
            int centerY = _yConstraint / 2;

            switch (region)
            {
                case StartingRegion.Top:
                    startingPosition = new Vector2(randomizer.Next(0, _xConstraint), - Constants.STARTING_POSITION_BUFFER);                    
                    Logger.Log("Starting Region: TOP");
                    break;
                case StartingRegion.Bottom:
                    startingPosition = new Vector2(randomizer.Next(0, _xConstraint), _yConstraint + Constants.STARTING_POSITION_BUFFER);
                    Logger.Log( "Starting Region: BOTTOM");
                    
                    break;
                case StartingRegion.Right:
                    startingPosition = new Vector2(_xConstraint + 5, randomizer.Next(0, _yConstraint));
                    
                    Logger.Log("Starting Region: RIGHT");
                    
                    break;
                case StartingRegion.Left:
                    startingPosition = new Vector2(- 5, randomizer.Next(0, _yConstraint));
                    Logger.Log("Starting Region: LEFT");
                    break;
                default:
                    startingPosition = new Vector2(100f, 100f);
                    break;
            }
            float deltaX = centerX - startingPosition.X;
            float deltaY = centerY - startingPosition.Y;
            float velocityAngle = (float)Math.Atan2(deltaY, deltaX); // TODO: randomize deviating from going toward the middle

            //return new Asteroid(_texture, startingPosition, speed, velocityAngle, (float)Math.PI, rotationSpeed, scale);
            return new Asteroid(_texture, startingPosition, speed, velocityAngle, (float)Math.PI, rotationSpeed, 1f);
        } 
    }
}
