
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
    public interface Component
    {
        public Entity GameEntity { get; set; }
        void Update();
        void Start();
    }
    public interface RenderComponent
    {
        public Entity GameEntity { get; set; }
        public float Layer { get; set; }
        void Start();
        void Update();
    }
    public interface UIComponent
    {
        public Entity GameEntity { get; set; }
        public float Layer { get; set; }
        void Start();
        void Update();
    }
    class ITranform : Component
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
        public void Start()
        {


        }
        public void Update()
        {
        }

    }
    class ITexture : RenderComponent
    {
        public Entity GameEntity { get; set; }
        public float Layer { get; set; }
        public Color TextureColor;
        public Texture2D Texture;
        public Pivots TexturePivot;
        private ITranform T;
        public ITexture(Texture2D _Texture, Pivots _Pivot, Color _TextureColor, float _Layer)
        {
            Texture = _Texture;
            TextureColor = _TextureColor;
            Layer = _Layer;
            TexturePivot = _Pivot;
        }
        public void Start()
        {
            T = GameEntity.GetComponent<ITranform>();
        }
        public void Update()
        {
            if (GameEntity == null || GameEntity.EntityState == State.Disabled) return;
            Layer = T.Positon.Y;
            Rectangle Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 Origin = TextureUtilities.GetOrigin(TexturePivot, Source);
            Manager.DrawBatch.Draw(Texture, T.Positon, Source, Color.White, 0, Origin, T.Scale, SpriteEffects.None, 0);
        }
    }
    class IGuiTexture : UIComponent
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
            Origin = _Origin;
            Layer = _Layer;
        }
        private ITranform T;
        public void Start()
        {
            T = GameEntity.GetComponent<ITranform>();
        }
        public void Update()
        {
            if (GameEntity == null) return;
            Rectangle Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Manager.DrawBatch.Draw(Texture, T.Positon, Source, TextureColor, 0, Origin, T.Scale, SpriteEffects.None, 0);

        }
    }
    class ICameraMovement : Component
    {
        public Entity GameEntity { get; set; }
        public ICameraMovement()
        {
            MainCamera.MinimumZoom = 0.1f;
            MainCamera.MaximumZoom = 0.85f;
            MainCamera.Origin = new Vector2(GDevice.Viewport.Width / 2, GDevice.Viewport.Height / 2);
        }
        static Vector2 ClickPos;
        static Vector2 TargetPos;
        static Vector2 InitialCamPos;
        public static float CameraZoomSpeed = 20f;
        public static float CameraLerpSpeed = 6f;

        float CurrentZoom;


        public void Start()
        {

        }
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
    class IFrameRateDisplay : UIComponent
    {
        public Entity GameEntity { get; set; }
        public float Layer { get; set; }
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 100;
        private Queue<float> _sampleBuffer = new Queue<float>();

        public void Start()
        {

        }
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
            DrawBatch.DrawString(Fonts.Default, $"FPS:{Math.Round(AverageFramesPerSecond)}", Vector2.One * 20, Color.Black, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, 0);
        }


    }

}
