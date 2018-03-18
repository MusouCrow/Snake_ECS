using UnityEngine;

namespace Game.Entitys {
    using Core;
    using Components;

    public class Food : Entity {
        public Position position = new Position();

        protected void Awake() {
            this.position.Init(this);
        }

        protected new void OnDestroy() {
            base.OnDestroy();

            this.position.Destroy();
        }
    }
}
