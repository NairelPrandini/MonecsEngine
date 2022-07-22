using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using EntityComponentSystem;
using Monecs;
using System;

namespace Monecs
{
    public enum Pivots
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }
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
            var r = Radius * MathF.Sqrt((float)Rand.NextDouble());
            var theta = (float)Rand.NextDouble() * 2 * MathF.PI;
            var x = r * MathF.Cos(theta);
            var y = r * MathF.Sin(theta);

            return new Vector2(x, y);

        }




    }
    public static class TextureUtilities
    {
        public static Vector2 GetOrigin(Pivots P, Rectangle Rect)
        {
            switch (P)
            {
                case Pivots.TopLeft:
                    return new Vector2(0, 0);
                case Pivots.TopCenter:
                    return new Vector2(Rect.Width / 2, 0);
                case Pivots.TopRight:
                    return new Vector2(Rect.Width, 0);
                case Pivots.MiddleLeft:
                    return new Vector2(0, Rect.Height / 2);
                case Pivots.MiddleCenter:
                    return new Vector2(Rect.Width / 2, Rect.Height / 2);
                case Pivots.MiddleRight:
                    return new Vector2(Rect.Width, Rect.Height / 2);
                case Pivots.BottomLeft:
                    return new Vector2(0, Rect.Height);
                case Pivots.BottomCenter:
                    return new Vector2(Rect.Width / 2, Rect.Height);
                case Pivots.BottomRight:
                    return new Vector2(Rect.Width, Rect.Height);
                default:
                    return new Vector2(Rect.Width / 2, Rect.Height / 2);
            }
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
    public static class Effects
    {
        public static Effect Default;
        public static Effect Sepia;
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