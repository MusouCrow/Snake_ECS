using UnityEngine;

namespace Game.Entitys {
    using Core;
    using Components;

    public class Body : Entity {
        public Position position = new Position();
        public Joint joint = new Joint();

        protected void Awake() {
            this.position.Init(this);
            this.joint.Init(this);
        }

        protected new void OnDestroy() {
            base.OnDestroy();

            this.position.Destroy();
            this.joint.Destroy();
        }
    }
}
