using System;
using UnityEngine;

namespace Game.Core {
    public class Entity : MonoBehaviour {
        public static event Action<Entity> NewTickEvent;
        public static event Action<Entity> DestroyTickEvent;

        protected void Start() {
            if (Entity.NewTickEvent != null) {
                Entity.NewTickEvent(this);
            }
        }

        protected void OnDestroy() {
            if (Entity.DestroyTickEvent != null) {
                Entity.DestroyTickEvent(this);
            }
        }
    }
}
