
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections;
using System;
using Monecs;
using static Monecs.Manager;

namespace EntityComponentSystem
{
    public interface IComponent
    {
        public Entity GameEntity { get; set; }
        void Update();
    }
    public interface RenderComponent
    {
        public Entity GameEntity { get; set; }
        public float Layer { get; set; }
        void Update();
    }
    public interface UIRenderComponent
    {
        public Entity GameEntity { get; set; }
        public float Layer { get; set; }
        void Update();
    }
    class ITranform : IComponent
    {
        public Entity GameEntity { get; set; }
        public Vector2 Positon = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public int Rotation = 0;
        public ITranform(Vector2 _Position, Vector2 _Scale, int _Rotation)
        {
            Positon = _Position;
            Scale = _Scale;
            Rotation = _Rotation;
        }
        public void Update()
        {
        }

    }
    class ITexture : RenderComponent
    {
        public Entity GameEntity { get; set; }
        public Color TextureColor;
        public Texture2D Texture;
        public Vector2 Origin;
        public float Layer { get; set; }
        public ITexture(Texture2D _Texture, Vector2 _Origin, Color _TextureColor, float _Layer)
        {
            Texture = _Texture;
            TextureColor = _TextureColor;
            Layer = _Layer;
            Origin = _Origin;
        }
        public void Update()
        {
            if (GameEntity == null) return;
            var T = GameEntity.GetComponent<ITranform>();
            if (T == null) return;
            Rectangle Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Manager.DrawBatch.Draw(Texture, T.Positon, Source, Color.White, 0, Origin, T.Scale, SpriteEffects.None, 0);

        }
    }
    class IGuiTexture : UIRenderComponent
    {
        public Entity GameEntity { get; set; }
        public float Layer { get; set; }
        public Color TextureColor;
        public Texture2D Texture;
        public Vector2 Origin;
        public IGuiTexture(Texture2D _Texture, Vector2 _Origin, Color _TextureColor, float _Layer)
        {
            Texture = _Texture;
            TextureColor = _TextureColor;
            Layer = _Layer;
            Origin = _Origin;
        }
        public void Update()
        {
            if (GameEntity == null) return;
            var T = GameEntity.GetComponent<ITranform>();
            if (T == null) return;
            Rectangle Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Manager.DrawBatch.Draw(Texture, T.Positon, Source, Color.White, 0, Origin, T.Scale, SpriteEffects.None, 1);

        }
    }
    class ICameraMovement : IComponent
    {
        public Entity GameEntity { get; set; }
        public ICameraMovement()
        {
            MainCamera.MinimumZoom = 0.1f;
            MainCamera.MaximumZoom = 10f;
            MainCamera.Origin = new Vector2(GDevice.Viewport.Width / 2, GDevice.Viewport.Height / 2);

        }
        static Vector2 ClickPos;
        static Vector2 TargetPos;
        static Vector2 InitialCamPos;
        public static float CameraZoomSpeed = 10f;
        public static float CameraLerpSpeed = 6f;

        float CurrentZoom;

        public void Update()
        {



            var MouseS = MouseExtended.GetState();


            CurrentZoom -= (MouseS.DeltaScrollWheelValue / 120) * Time.DeltaTime * CameraZoomSpeed * MainCamera.Zoom;
            CurrentZoom = Math.Clamp(CurrentZoom, MainCamera.MinimumZoom, MainCamera.MaximumZoom);
            MainCamera.Zoom = CurrentZoom;
            Vector2 MousePos = new Vector2(MouseS.X, MouseS.Y);
            MousePos = MainCamera.ScreenToWorld(MousePos);

            if (MouseS.WasButtonJustDown(MouseButton.Left))
            {
                ClickPos = MousePos;
                InitialCamPos = MainCamera.Position;
            }
            if (MouseS.LeftButton == ButtonState.Pressed)
                TargetPos = (ClickPos - MousePos) + InitialCamPos;

            MainCamera.Position = Vector2.Lerp(MainCamera.Position, TargetPos, CameraLerpSpeed * Time.DeltaTime);


        }

    }
    class IFrameRateDisplay : UIRenderComponent
    {
        public Entity GameEntity { get; set; }
        public float Layer { get; set; }
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;
        private Queue<float> _sampleBuffer = new Queue<float>();


        public void Update()
        {
            CurrentFramesPerSecond = 1.0f / Time.DeltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += Time.DeltaTime;
            DrawBatch.DrawString(Fonts.Default, $"FPS:{Math.Round(AverageFramesPerSecond)}", Vector2.One, Color.Black, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, 0);
        }


    }

}
