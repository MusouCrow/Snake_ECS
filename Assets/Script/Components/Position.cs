using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components {
    using Core;
    using Solts;

    [Serializable]
    public class Position : Component {
        public static Dictionary<Entity, Position> Map = new Dictionary<Entity, Position>();
        public static List<Position> List = new List<Position>();

        public Vector2Int value;
        public Collision collsionSlot;

        public override void Init(Entity entity) {
            base.Init(entity);

            Position.Map.Add(entity, this);
            Position.List.Add(this);
        }

        public void Destroy() {
            Position.Map.Remove(this.entity);
            Position.List.Remove(this);
        }
    }
}