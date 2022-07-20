using System.Collections.Generic;
using EntityComponentSystem;
using System.Numerics;
using System.Linq;
using Monecs;
using System;

namespace EntityComponentSystem
{
    public class ComponentSystem
    {
        public List<IComponent> GameComponents = new List<IComponent>();
        public List<RenderComponent> WorldRenderComponents = new List<RenderComponent>();
        public List<UIRenderComponent> UIRenderComponents = new List<UIRenderComponent>();


        public void WorldRender()
        {
            List<RenderComponent> RenderList = WorldRenderComponents
                        .ConvertAll(C => (RenderComponent)C)
                            .OrderBy(C => C.Layer)
                                .ToList();
            RenderList.ForEach(E => E.Update());
        }
        public void UIRender()
        {
            List<UIRenderComponent> RenderList = UIRenderComponents
                          .ConvertAll(C => (UIRenderComponent)C)
                              .OrderBy(C => C.Layer)
                                  .ToList();
            RenderList.ForEach(E => E.Update());
        }
        public void Update()
        {
            GameComponents.ForEach(E => E.Update());
        }

    }
}