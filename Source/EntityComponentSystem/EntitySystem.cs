using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EntityComponentSystem;
using static Monecs.Manager;
using MonoGame.Extended;
using System;

namespace EntityComponentSystem
{
    public class Entity
    {
        public void AddComponent(IComponent C)
        {
            C.GameEntity = this;
            ECSManager.GameComponents.Add(C);
        }
        public void AddGUIComponent(UIRenderComponent C)
        {
            C.GameEntity = this;
            ECSManager.UIRenderComponents.Add(C);
        }
        public void AddRenderComponent(RenderComponent C)
        {
            C.GameEntity = this;
            ECSManager.WorldRenderComponents.Add(C);
        }
        public T GetComponent<T>() where T : IComponent
        {
            foreach (var Component in ECSManager.GameComponents)
            {
                if (this != Component.GameEntity) continue;
                if (Component.GetType().Equals(typeof(T))) return (T)Component;
            }
            return default(T);
        }
        public T GetGUIComponent<T>() where T : UIRenderComponent
        {
            foreach (var Component in ECSManager.UIRenderComponents)
            {
                if (this != Component.GameEntity) continue;
                if (Component.GetType().Equals(typeof(T))) return (T)Component;
            }
            return default(T);
        }
        public T GetWorldComponent<T>() where T : RenderComponent
        {
            foreach (var Component in ECSManager.WorldRenderComponents)
            {
                if (this != Component.GameEntity) continue;
                if (Component.GetType().Equals(typeof(T))) return (T)Component;
            }
            return default(T);
        }
    }

}