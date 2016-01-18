using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout.Breakout
{
    class BlockRegion : DrawableGameComponent
    {
        public enum BlockAlignment { LEFT, CENTER, RIGHT, JUSTIFIED };

        private SpriteBatch spriteBatch;
        private Rectangle blockRect;
        private List<IBlock> blocks;
        private BlockAlignment alignment;
        private int xOffset;
        private int yOffset;

        public int BlockCount { get { return (blocks != null) ? blocks.Count : 0; } }
        public Rectangle Region { get { return blockRect; } }
        public int HorizontalOffset
        {
            get { return xOffset; }
            set { xOffset = value >= 0 ? value : 0; RecalculateBlockPositions(); }
        }
        public int VerticalOffset
        {
            get { return yOffset; }
            set { yOffset = value >= 0 ? value : 0; RecalculateBlockPositions(); }
        }


        public BlockRegion(Game game, Rectangle region, GraphicsDevice graphicsDevice, BlockAlignment alignment = BlockAlignment.LEFT)
            : base(game)
        {
            blocks = new List<IBlock>();
            blockRect = region;
            spriteBatch = new SpriteBatch(graphicsDevice);
            this.alignment = alignment;
            this.xOffset = 0;
            this.yOffset = 0;
        }


        public void SpawnBlock<BlockType>(int count = 1) where BlockType : GameComponent, IBlock
        {
            for (int iNewBlocks = 0; iNewBlocks < count; iNewBlocks++)
            {
                BlockType block = (BlockType)Activator.CreateInstance(typeof(BlockType), this.Game);
                blocks.Add(block);
                block.LoadSprite();

                if (block.BlockSprite.spriteData.Width > blockRect.Width)
                    throw new ArgumentException("Block width is longer than region width. Consider changing region size or block asset: " + typeof(BlockType).ToString());
            }

            RecalculateBlockPositions();
        }

        protected void RecalculateBlockPositions()
        {
            if (blocks.Count == 0)
                return;
            switch (alignment)
            {
                case BlockAlignment.LEFT:
                    int curBlockX = 0;
                    int curBlockY = 0;
                    int curLineY = blocks[0].BlockSprite.spriteData.Height;
                    foreach (IBlock b in blocks)
                    {
                        if (curBlockX + b.BlockSprite.spriteData.Width + xOffset > blockRect.Width)
                        {
                            curBlockX = 0;
                            curBlockY += curLineY + yOffset;
                            if (curBlockY + yOffset > blockRect.Height)
                                return;

                            curLineY = b.BlockSprite.spriteData.Height + yOffset;
                        }
                        else if (b.BlockSprite.spriteData.Height + yOffset > curLineY)
                            curLineY = b.BlockSprite.spriteData.Height + yOffset;

                        b.BlockSprite.Transform.position = new Vector2(blockRect.X + curBlockX + xOffset, blockRect.Y + curBlockY + yOffset);
                        curBlockX += b.BlockSprite.spriteData.Width + xOffset;
                    }
                    return;
                default:
                    throw new NotImplementedException("Only left block alignment is implemented. Sorry ):");
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (IBlock b in blocks)
            {
                spriteBatch.Begin();
                b.DrawSelf(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        public void ClearBlocks()
        {
            foreach (IBlock b in blocks)
                b.IsActive = false;
            blocks.Clear();
        }
    }
}
