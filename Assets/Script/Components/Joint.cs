using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components {
    using Core;
    using Solts;

    public class Joint : Component {
        public static Dictionary<Entity, Joint> Map = new Dictionary<Entity, Joint>();
        public static Joint Tail;

        public Joint prior;
        public Joint next;
        [NonSerialized]
        public Vector2Int laterPos;

        public override void Init(Entity entity) {
            base.Init(entity);

            Tail = this;
            Joint.Map.Add(entity, this);
        }

        public void Destroy() {
            Joint.Map.Remove(this.entity);
        }
    }
}