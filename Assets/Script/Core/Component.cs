using System;

namespace Game.Core {
    public class Component {
        [NonSerialized]
        public Entity entity;

        public virtual void Init(Entity entity) {
            this.entity = entity;
        }
    }
}
