using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using JumpMan.Utilities;
using JumpMan.Generators;
using JumpMan.Managers;
using JumpMan.Controllers;
using System.Timers;
using JumpMan.Core;
using JumpMan.Models;
using Microsoft.Xna.Framework.Audio;

namespace JumpMan
{
    public class Game1 : Game
    {
        public Game1()
        {
            this.GraphicsManager = new GraphicsDeviceManager(this);
            Globals.GraphicsManager = this.GraphicsManager;
            
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        public GraphicsDeviceManager GraphicsManager { get; }
        private SpriteBatch _spriteBatch;

        private Texture2D _backgroundTexture;
        private Texture2D _planetTexture;
        private Texture2D _asteroidTexture;

        private Vector2 _planetPosition;

        //TODO
        // private float _boostSpeed;
        // private float _boostFuelAmount;

        private float _shuttleSpeed;
        private Vector2 _shuttlePosition; // TODO: Abstract to its own class.
        private Texture2D _shuttleTexture;
        private float _shuttleAngle;
        private float _shuttleRotationSpeed;
        private float _shuttleScale;

        private SpawnableObjectManager<Asteroid> _asteroidManager;

        private Timer _shootTimer;
        private bool _canShoot;
        private SoundEffect _laserFire;

        protected override void Initialize()
        {   
            _planetPosition = new Vector2(this.GraphicsManager.PreferredBackBufferWidth / 2, this.GraphicsManager.PreferredBackBufferHeight / 2);
            _shuttlePosition = new Vector2(this.GraphicsManager.PreferredBackBufferWidth / 2, this.GraphicsManager.PreferredBackBufferHeight / 2);

            _shuttleAngle = 0f;
            _shuttleRotationSpeed = 1.5f;
            _shuttleSpeed = 100f;
            _shuttleScale = 0.5f;

            _asteroidManager = new AsteroidManager(this);

            _canShoot = true;
            _shootTimer = new Timer(1000);
            _shootTimer.Elapsed += (sender, args) =>
            {
                _canShoot = true;
                _shootTimer.Stop();
            };
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(this.GraphicsDevice);

            // Textures
            _backgroundTexture = this.Content.Load<Texture2D>("stars");
            _planetTexture = this.Content.Load<Texture2D>("earth");
            _shuttleTexture = this.Content.Load<Texture2D>("shuttle");
            _asteroidTexture = this.Content.Load<Texture2D>("asteroid1");

            // Sounds
            _laserFire = this.Content.Load<SoundEffect>("laser_fire");
            SoundEffectLibrary.Initialize();
            SoundEffectLibrary.Instance.AddToLibrary(Constants.LASER_FIRE_SOUND_EFFECT, _laserFire);

            AsteroidGenerator.Initialize(1f, _asteroidTexture, this.GraphicsManager.PreferredBackBufferWidth, this.GraphicsManager.PreferredBackBufferHeight);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            
            // NOTE: Can be used to get the position of the mouse and click events;
            // MouseState mState = Mouse.GetState();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Put all this in Ship. Ship needs to be instantiated somewhere
            if (kState.IsKeyDown(Keys.W))
            {
                _shuttlePosition = MovementController.CalculateVelocity(_shuttlePosition, _shuttleAngle - (float)(Math.PI / 2), _shuttleSpeed, deltaTime, PosNegMultiplier.Positive);
                _shuttlePosition = MovementController.ClampPositionToBounds(position: _shuttlePosition, 
                                                                            leftBound: (_shuttleTexture.Width * _shuttleScale) / 2, 
                                                                            topBound: (_shuttleTexture.Height * _shuttleScale) / 2, 
                                                                            rightBound: this.GraphicsManager.PreferredBackBufferWidth - ((_shuttleTexture.Width * _shuttleScale) / 2), 
                                                                            bottomBound: this.GraphicsManager.PreferredBackBufferHeight - ((_shuttleTexture.Height * _shuttleScale) / 2));
            }

            if (kState.IsKeyDown(Keys.S))
            {
                _shuttlePosition = MovementController.CalculateVelocity(_shuttlePosition, _shuttleAngle - (float)(Math.PI / 2), _shuttleSpeed, deltaTime, PosNegMultiplier.Negative);
                _shuttlePosition = MovementController.ClampPositionToBounds(position: _shuttlePosition,
                                                                            leftBound: (_shuttleTexture.Width * _shuttleScale) / 2,
                                                                            topBound: (_shuttleTexture.Height * _shuttleScale) / 2,
                                                                            rightBound: this.GraphicsManager.PreferredBackBufferWidth - ((_shuttleTexture.Width*_shuttleScale) / 2),
                                                                            bottomBound: this.GraphicsManager.PreferredBackBufferHeight - ((_shuttleTexture.Height*_shuttleScale) / 2));
            }

            if (kState.IsKeyDown(Keys.A))
            {
                _shuttleAngle = MovementController.CalculateRotation(_shuttleAngle, _shuttleRotationSpeed, deltaTime, PosNegMultiplier.Negative);
            }

            if (kState.IsKeyDown(Keys.D))
            {
                _shuttleAngle = MovementController.CalculateRotation(_shuttleAngle, _shuttleRotationSpeed, deltaTime, PosNegMultiplier.Positive);
            }

            if (kState.IsKeyDown(Keys.Space))
            {
                // Shoot boolets
                if (_canShoot)
                {
                    
                    
                    SoundEffectLibrary.Instance.Play(Constants.LASER_FIRE_SOUND_EFFECT);
                    _canShoot = false;
                    _shootTimer.Start();
                }
            }

            if(!_asteroidManager.SpawnableList.IsEmpty)
            {
                foreach(Asteroid a in _asteroidManager.SpawnableList)
                { 
                    a.Update(deltaTime);
                    
                    _asteroidManager.CheckCollisionWithObject(a);
                    _asteroidManager.CheckToRemove(a);

                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // The most important parameter here is the origin param. Origin defines the point of reference of where to position the object.
            // In this case, the origin is the center of the sprite. Originally it is position (0,0), which is the top left of the image.

            // Learning: Rectangle is used to set the position and the size of the sprite.
            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, this.GraphicsManager.PreferredBackBufferWidth, this.GraphicsManager.PreferredBackBufferHeight), Color.White);
            _spriteBatch.DrawOrigin(_planetTexture, _planetPosition, new Vector2(_planetTexture.Width / 2, _planetTexture.Height / 2));
            _spriteBatch.DrawOriginRotationScale(_shuttleTexture, _shuttlePosition, new Vector2(_shuttleTexture.Width / 2, _shuttleTexture.Height / 2), _shuttleAngle, new Vector2(_shuttleScale));
            
            // Draw asteroids
            foreach(Asteroid a in _asteroidManager.SpawnableList)
            {
                _spriteBatch.DrawOriginRotationScale(a.Texture, a.Position, new Vector2(a.Texture.Width / 2, a.Texture.Height / 2), a.RotationAngle, new Vector2(a.Scale));
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}