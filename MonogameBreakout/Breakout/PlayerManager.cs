using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameBreakout.Breakout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout
{
    class PlayerManager : DrawableGameComponent
    {
        const int HUDStartY = 10;
        const int HUDHeight = 40;
        const string HUDSpriteFontName = "HUDFont";
        Vector2 livesTextPos;
        Vector2 scoreTextPos;
        Vector2 blockTextPos;
        Vector2 levelTextPos;
        Vector2 gameOverTextPos;

        SpriteBatch sb;
        SpriteFont HUDFont;

        int playerLives;
        int Lives { get { return playerLives; } }
        int playerScore;
        int level;

        public int blocks;

        BreakoutPaddle paddle;
        BreakoutBall ball;

        Vector2 ballVerticalOffset;

        BlockRegion upperSpawner;
        BlockRegion lowerSpawner;

        public bool gameOver { get; private set; }
        bool gameWon;

        public PlayerManager(Game game) : base(game)
        {
            this.playerScore = 0;
            this.playerLives = 3;
            this.blocks = 0;
            this.level = 0;

            gameOver = false;
            gameWon = false;

            livesTextPos = new Vector2(20, HUDStartY);
            scoreTextPos = new Vector2(150, HUDStartY);
            blockTextPos = new Vector2(300, HUDStartY);
            levelTextPos = new Vector2(480, HUDStartY);

            gameOverTextPos = new Vector2(Game.GraphicsDevice.Viewport.Width / 2f - 125, Game.GraphicsDevice.Viewport.Height / 2f);
        }

        public void Spawn()
        {
            Vector2 paddleStartPos = new Vector2(Game.GraphicsDevice.Viewport.Width / 2f, Game.GraphicsDevice.Viewport.Height - 20);

            this.ball = new BreakoutBall(Game, "Sprites/Ball/ballSmall", new BallController(Game), 220f);
            PlayerController paddleController = new PlayerController(Game, 0);
            this.paddle = new BreakoutPaddle(Game, "Sprites/Player/paddleBig", paddleController, ball, 270f, 0f, 0f);

            upperSpawner = new BlockRegion(Game, new Rectangle(20, 20 + HUDHeight, Game.GraphicsDevice.Viewport.Width - 40, 200), Game.GraphicsDevice);
            lowerSpawner = new BlockRegion(Game, new Rectangle(20, 120 + HUDHeight, Game.GraphicsDevice.Viewport.Width - 40, 200), Game.GraphicsDevice);

            Game.Components.Add(paddleController);
            Game.Components.Add(paddle);
            Game.Components.Add(ball);
            Game.Components.Add(upperSpawner);
            Game.Components.Add(lowerSpawner);

            this.paddle.Transform.position = paddleStartPos;

            LevelOne();
        }

        void LevelOne()
        {

            upperSpawner.ClearBlocks();
            lowerSpawner.ClearBlocks();

            upperSpawner.SpawnBlock<DefaultBlock>(21);
            upperSpawner.SpawnBlock<RedBlock>(21);
            lowerSpawner.SpawnBlock<YellowBlock>(21);
        }

        void LevelTwo()
        {
            ball.Speed = 190f;

            upperSpawner.ClearBlocks();
            lowerSpawner.ClearBlocks();
            upperSpawner.VerticalOffset = 5;
            upperSpawner.HorizontalOffset = 5;

            for (int i = 0; i < 72; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        upperSpawner.SpawnBlock<DefaultBlock>();
                        break;
                    case 1:
                        upperSpawner.SpawnBlock<YellowBlock>();
                        break;
                    case 2:
                        upperSpawner.SpawnBlock<RedBlock>();
                        break;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (ball.Controller.direction == Vector2.Zero)
            {
                ballVerticalOffset = -Vector2.UnitY * (paddle.spriteData.Height / 2f + ball.spriteData.Height / 2f);
                this.ball.Transform.position = paddle.Transform.position + ballVerticalOffset;
            }

            if (blocks <= 0)
            {
                if (level == 1)
                {
                    ball.ballController.direction = Vector2.Zero;
                    gameWon = true;
                    paddle.lockedX = true;
                }
                else
                {
                    ball.ballController.direction = Vector2.Zero;
                    LevelTwo();
                }
            }

            base.Update(gameTime);
        }

        public void BallDeath()
        {
            playerLives--;
            ball.ballController.direction = Vector2.Zero;

            if (playerLives == 0)
            {
                gameOver = true;
                ball.IsEnabled = false;
                paddle.lockedX = true;
            }
        }

        public void AddScore(int amount)
        {
            playerScore += amount;
            Console.WriteLine("Score " + playerScore.ToString());
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
            HUDFont = Game.Content.Load<SpriteFont>(HUDSpriteFontName);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.DrawString(HUDFont, "Lives: " + playerLives.ToString(), livesTextPos, Color.Black);
            sb.DrawString(HUDFont, "Score: " + playerScore.ToString(), scoreTextPos, Color.Black);
            sb.DrawString(HUDFont, "Blocks Left: " + blocks.ToString(), blockTextPos, Color.Black);
            sb.DrawString(HUDFont, "Level: " + (level + 1).ToString(), levelTextPos, Color.Black);

            if (gameOver) sb.DrawString(HUDFont, "GAME OVER", gameOverTextPos, Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
            if (gameWon) sb.DrawString(HUDFont, "YOU WON!!", gameOverTextPos, Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);

            sb.End();
            base.Draw(gameTime);
        }
    }
}
