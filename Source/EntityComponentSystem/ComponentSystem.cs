using System.Collections.Generic;
using EntityComponentSystem;
using System.Numerics;
using System.Linq;
using System.Threading.Tasks;
using Monecs;
using System;

namespace EntityComponentSystem
{
    public class ComponentSystem
    {
        public List<Entity> GameEntityes = new List<Entity>();
        public List<IComponent> GameComponents = new List<IComponent>();
        public List<RenderComponent> WorldRenderComponents = new List<RenderComponent>();
        public List<UIRenderComponent> UIRenderComponents = new List<UIRenderComponent>();


        public void WorldRender()
        {
            List<RenderComponent> RenderList = WorldRenderComponents
                        .ConvertAll(C => (RenderComponent)C)
                            .OrderBy(C => C.Layer)
                                .ToList();

            foreach (RenderComponent E in RenderList)
            {
                if (E.GameEntity == null || E.GameEntity.EntityState == State.Disabled) continue;
                E.Update();
            }
        }
        public void UIRender()
        {
            List<UIRenderComponent> RenderList = UIRenderComponents
                                  .ConvertAll(C => (UIRenderComponent)C)
                                      .OrderBy(C => C.Layer)
                                          .ToList();

            foreach (UIRenderComponent E in RenderList)
            {
                if (E.GameEntity == null || E.GameEntity.EntityState == State.Disabled) continue;
                E.Update();
            }
        }
        public void Update()
        {
            foreach (IComponent E in GameComponents)
            {
                if (E.GameEntity == null || E.GameEntity.EntityState == State.Disabled) continue;
                E.Update();
            }
        }

        public Entity GetEntityById(int ID)
        {
            for (int i = 0; i < GameEntityes.Count; i++)
            {
                if (GameEntityes[i].ID == ID)
                    return GameEntityes[i];
            }

            return null;
        }
        public Entity GetEntityByTag(string Tag)
        {
            for (int i = 0; i < GameEntityes.Count; i++)
            {
                if (GameEntityes[i].Tag == Tag)
                    return GameEntityes[i];
            }
            return null;
        }

    }
}