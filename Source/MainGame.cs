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


        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            for (int i = 0; i < ECSManager.GameEntityes.Count; i++)
            {
                var E = ECSManager.GameEntityes[i];

                if (E.Tag != "Camera" && E.Tag != "FrameDisplay")
                {
                    E.EntityState = State.Disabled;
                }
            }
        }

    }
    protected override void Draw(GameTime DeltaTime)
    {
        base.Draw(DeltaTime);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        DrawBatch.Begin(sortMode: SpriteSortMode.Texture, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp, transformMatrix: MainCamera.GetViewMatrix());
        ECSManager.WorldRender();
        DrawBatch.End();
        DrawBatch.Begin(sortMode: SpriteSortMode.Texture, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);
        ECSManager.UIRender();
        DrawBatch.End();
    }
    protected override void LoadContent()
    {
        base.LoadContent();
        Textures.Miku = Content.Load<Texture2D>(@"Textures\Miku");
        Textures.TestSprite = Content.Load<Texture2D>(@"Textures\TestSprite");
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
        var CameraMover = new Entity("Camera");
        CameraMover.AddComponent(new ICameraMovement());

        var FrameCounterDisplay = new Entity("FrameDisplay");
        FrameCounterDisplay.AddGUIComponent(new IFrameRateDisplay());

        for (int i = 0; i < 1000; i++)
        {
            var Miku = new Entity();
            Miku.AddComponent(new ITranform(ExtendedMath.RandomInsideRadius(3000), Vector2.One / 10, 0));
            Miku.AddRenderComponent(new ITexture(Textures.Miku, Vector2.Zero, Color.White, 0));
        }


        var GuiMiku = new Entity();
        GuiMiku.AddComponent(new ITranform(Vector2.Zero, Vector2.One, 0));
        GuiMiku.AddGUIComponent(new IGuiTexture(Textures.Miku, Vector2.Zero, Color.White, 0));






    }

}
