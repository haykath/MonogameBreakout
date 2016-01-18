using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameLibrary.Debug
{
    
    public class FPSCounter : DrawableGameComponent
    {
        private string lastWindowName;

        private bool isEnabled;

        public bool Enabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                if (value && !isEnabled)
                    lastWindowName = this.Game.Window.Title;
                else if (!value && isEnabled)
                    this.Game.Window.Title = lastWindowName;

                isEnabled = value;
            }
        }

        private int elapsedFrames;
        private float elapsedTime;

        public int lastFPSCount { get; private set; }

        public FPSCounter(Game game, bool enabled = true) : base(game)
        {
            this.elapsedFrames = 0;
            this.elapsedTime = 0f;
            this.lastFPSCount = 0;

            this.lastWindowName = game.Window.Title;
            this.isEnabled = enabled;

            if(enabled) Game.Window.Title = "FPS: " + lastFPSCount.ToString();
        }

        public override void Update(GameTime gameTime)
        {
            if (isEnabled)
            {
                this.elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (this.elapsedTime >= 1000f)
                {
                    this.lastFPSCount = elapsedFrames;
                    this.elapsedFrames = 0;
                    this.elapsedTime = 0f;

                    this.Game.Window.Title = "FPS: " + lastFPSCount.ToString();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.elapsedFrames++;

            base.Draw(gameTime);
        }
    }
}
