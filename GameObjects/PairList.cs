using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManagement;

namespace PenguinPairs.GameObjects
{
    class PairList : SpriteGameObject
    {
        protected int[] pairs;
        protected SpriteGameObject pairSprite;

        public bool Completed
        {
            get
            {
                for (int i = 0; i < pairs.Length; i++)
                {
                    if (pairs[i] == 7)
                        return false;
                }
                return true;
            }
        }

        public PairList(int nrPairs, int layer = 0, string id = "", int sheetIndex = 0) : base("Sprites/spr_frame_goal", layer, id, sheetIndex)
        {
            pairSprite = new SpriteGameObject("Sprites/spr_penguin_pairs@8");
            pairSprite.Parent = this;
            pairs = new int[nrPairs];
            for (int i = 0; i < nrPairs; i++)
                pairs[i] = 7;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (!Visible)
                return;
            for (int i = 0; i < pairs.Length; i++)
            {
                pairSprite.Position = new Vector2(110 + i * Sprite.Height, 8);
                pairSprite.Sprite.SheetIndex = pairs[i];
                pairSprite.Draw(gameTime, spriteBatch);
            }
        }

        public void AddPair(int index)
        {
            int i = 0;
            while (i < pairs.Length && pairs[i] != 7)
                i++;
            if (i < pairs.Length)
                pairs[i] = index;
        }
    }
}
