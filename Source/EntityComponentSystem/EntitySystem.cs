using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EntityComponentSystem;
using static Monecs.Manager;
using Monecs;
using MonoGame.Extended;
using System;

namespace EntityComponentSystem
{
    public class Entity
    {
        public string Tag;
        public int ID;
        public State EntityState;
        public Entity(string _Tag = "Entity", int _ID = 0, State _State = State.Enabled)
        {
            Tag = _Tag;
            ID = _ID;
            EntityState = _State;
        }
        public void AddComponent(Component C)
        {
            C.GameEntity = this;
            ECSManager.GameComponents.Add(C);
            C.Start();
        }
        public void AddGUIComponent(UIComponent C)
        {
            C.GameEntity = this;
            ECSManager.UIComponents.Add(C);
            C.Start();
        }
        public void AddRenderComponent(RenderComponent C)
        {
            C.GameEntity = this;
            ECSManager.WorldComponents.Add(C);
            C.Start();
        }
        public T GetComponent<T>() where T : Component
        {
            foreach (var Component in ECSManager.GameComponents)
            {
                if (this != Component.GameEntity) continue;
                if (Component.GetType().Equals(typeof(T))) return (T)Component;
            }
            return default(T);
        }
        public T GetGUIComponent<T>() where T : UIComponent
        {
            foreach (var Component in ECSManager.UIComponents)
            {
                if (this != Component.GameEntity) continue;
                if (Component.GetType().Equals(typeof(T))) return (T)Component;
            }
            return default(T);
        }
        public T GeRenderComponent<T>() where T : RenderComponent
        {
            foreach (var Component in ECSManager.WorldComponents)
            {
                if (this != Component.GameEntity) continue;
                if (Component.GetType().Equals(typeof(T))) return (T)Component;
            }
            return default(T);
        }
    }

}