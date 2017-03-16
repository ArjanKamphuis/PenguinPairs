using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManagement;

namespace PenguinPairs.GameObjects
{
    class Tile : SpriteGameObject
    {
        public TileType Type { get; set; }

        public Tile(string assetName, int layer = 0, string id = "", int sheetIndex = 0) : base(assetName, layer, id, sheetIndex)
        {
            Type = TileType.Normal;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Type == TileType.Background)
                return;
            base.Draw(gameTime, spriteBatch);
        }
    }

    enum TileType
    {
        Normal,
        Background,
        Wall
    }
}
