using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using EntityComponentSystem;
using Monecs;
using System;

namespace Monecs
{
    public enum State
    {
        Enabled,
        Disabled
    }


    public static class ExtendedMath
    {

        public static readonly Random Rand = new Random();

        public static Vector2 RandomInsideRadius(float Radius)
        {
            float angle = Rand.Next();
            float value = angle * (MathF.PI / 180f);
            float x = (float)(Radius * Rand.NextDouble() * MathF.Cos(value));
            float y = (float)(Radius * Rand.NextDouble() * MathF.Sin(value));
            return new Vector2(x, y);

        }

    }

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
        public static Texture2D TestSprite;
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