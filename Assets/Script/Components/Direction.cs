using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components {
    using Core;

    [Serializable]
    public class Direction : Component {
        public enum Enum {
            up,
            down,
            right,
            left
        }

        public static Dictionary<Entity, Direction> Map = new Dictionary<Entity, Direction>();

        [SerializeField]
        private Enum value;

        public Enum Value {
            get {
                return this.value;
            }
            set {
                if (this.value == value) {
                    return;
                }

                bool ban1 = value == Enum.down && this.value == Enum.up;
                bool ban2 = value == Enum.up && this.value == Enum.down;
                bool ban3 = value == Enum.left && this.value == Enum.right;
                bool ban4 = value == Enum.right && this.value == Enum.left;

                if (!(ban1 || ban2 || ban3 || ban4)) {
                    this.value = value;
                }
            }
        }

        public override void Init(Entity entity) {
            base.Init(entity);
            
            Direction.Map.Add(entity, this);
        }

        public void Destroy() {
            Direction.Map.Remove(this.entity);
        }
    }
}