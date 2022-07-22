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
        public List<Entity> EntitiesList = new List<Entity>();
        public List<Component> GameComponents = new List<Component>();
        public List<RenderComponent> WorldComponents = new List<RenderComponent>();
        public List<UIComponent> UIComponents = new List<UIComponent>();

        public void WorldRender()
        {
            WorldComponents = WorldComponents.OrderBy(C => C.Layer).ToList();
            WorldComponents.ForEach(C => C.Update());
        }
        public void UIRender()
        {
            UIComponents = UIComponents.OrderBy(C => C.Layer).ToList();
            UIComponents.ForEach(C => C.Update());
        }
        public void Update()
        {
            GameComponents.ForEach(C => C.Update());
        }

    }
}