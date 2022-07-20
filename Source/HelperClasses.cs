using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using EntityComponentSystem;
using Monecs;

namespace Monecs
{
    public static class Manager
    {
        public static GraphicsDeviceManager GraphicsManager;
        public static GraphicsDevice GDevice;
        public static OrthographicCamera MainCamera;
        public static SpriteBatch DrawBatch;
        public static ComponentSystem ECSManager;

    }

    public static class Textures
    {
        public static Texture2D Miku;
    }
    public static class Fonts
    {
        public static SpriteFont Default;
    }
    public static class Time
    {
        public static GameTime GTime;
        public static float DeltaTime;
    }
}