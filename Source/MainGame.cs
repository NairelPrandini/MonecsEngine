using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EntityComponentSystem;
using MonoGame.Extended;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MonoGame;
using Monecs;
using System;
using System.Timers;
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
        DrawBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, MainCamera.GetViewMatrix());
        ECSManager.WorldRender();
        DrawBatch.End();
        DrawBatch.Begin(sortMode: SpriteSortMode.Immediate, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);
        ECSManager.UIRender();
        DrawBatch.End();

    }
    protected override void LoadContent()
    {
        base.LoadContent();
        Textures.Miku = Content.Load<Texture2D>(@"Textures\Miku");
        Textures.TestSprite = Content.Load<Texture2D>(@"Textures\TestSprite");
        Fonts.Default = Content.Load<SpriteFont>(@"Fonts\Default");
        Effects.Default = Content.Load<Effect>(@"Effects\Default");
        Effects.Sepia = Content.Load<Effect>(@"Effects\Sepia");
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


        var CameraMover = new Entity("Camera");
        CameraMover.AddComponent(new ICameraMovement());
        var FrameCounterDisplay = new Entity("FrameDisplay");
        FrameCounterDisplay.AddGUIComponent(new IFrameRateDisplay());


        for (int i = 0; i < 15000; i++)
        {
            var A = new Entity();
            var T = new ITranform(ExtendedMath.RandomInsideRadius(10000), Vector2.One * 4, 0);
            A.AddComponent(T);
            A.AddRenderComponent(new ITexture(Textures.TestSprite, Pivots.BottomCenter, Color.White, 0));
        }

        var PBMiku = new Entity("PBMiku");
        PBMiku.AddComponent(new ITranform(new Vector2(100, 0), Vector2.One, 0));
        PBMiku.AddGUIComponent(new IGuiTexture(Textures.Miku, Vector2.Zero, Color.White, 6));
        var GUIMiku = new Entity();
        GUIMiku.AddComponent(new ITranform(new Vector2(700, 200), Vector2.One * 10, 0));
        GUIMiku.AddGUIComponent(new IGuiTexture(Textures.TestSprite, Vector2.Zero, Color.White, -1));

    }
}
