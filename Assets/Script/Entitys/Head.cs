using UnityEngine;

namespace Game.Entitys {
    using Core;
    using Components;

    public class Head : Entity {
        public static Head Instance;

        public Position position = new Position();
        public Direction direction = new Direction();
        public Joint joint = new Joint();

        protected void Awake() {
            Head.Instance = this;

            this.position.Init(this);
            this.direction.Init(this);
            this.joint.Init(this);
        }

        protected new void OnDestroy() {
            base.OnDestroy();

            this.position.Destroy();
            this.direction.Destroy();
            this.joint.Destroy();
            Head.Instance = null;
        }
    }
}
