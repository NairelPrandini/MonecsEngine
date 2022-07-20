using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EntityComponentSystem;
using MonoGame.Extended;
using MonoGame;
using Monecs;
using System;
using static Monecs.Manager;

public class MainGame : Game
{
    public MainGame()
    {
        GraphicsManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    protected override void Update(GameTime DeltaTime)
    {
        base.Update(DeltaTime);
        Time.DeltaTime = (float)DeltaTime.ElapsedGameTime.TotalSeconds;
        Time.GTime = DeltaTime;
        ECSManager.Update();

    }
    protected override void Draw(GameTime DeltaTime)
    {
        base.Draw(DeltaTime);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        DrawBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: MainCamera.GetViewMatrix());
        ECSManager.WorldRender();
        DrawBatch.End();
        DrawBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        ECSManager.UIRender();
        DrawBatch.End();
    }
    protected override void LoadContent()
    {
        base.LoadContent();
        Textures.Miku = Content.Load<Texture2D>(@"Textures\Miku");
        Fonts.Default = Content.Load<SpriteFont>(@"Fonts\Default");
    }
    protected override void Initialize()
    {
        base.Initialize();
        DrawBatch = new SpriteBatch(GraphicsDevice);
        MainCamera = new OrthographicCamera(GraphicsDevice);
        ECSManager = new ComponentSystem();

        Window.Title = "Monecs";
        GDevice = GraphicsDevice;
        GraphicsManager.PreferredBackBufferWidth = 1024;
        GraphicsManager.PreferredBackBufferHeight = 768;
        GraphicsManager.SynchronizeWithVerticalRetrace = false;
        IsFixedTimeStep = false;
        GraphicsManager.ApplyChanges();


        //---------------------------------------------------------
        var CameraMover = new Entity();
        CameraMover.AddComponent(new ICameraMovement());
        var FrameCounterDisplay = new Entity();
        FrameCounterDisplay.AddGUIComponent(new IFrameRateDisplay());



    }

}
