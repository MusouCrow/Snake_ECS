using System.Collections.Generic;
using UnityEngine;

namespace Game.Systems {
    using Core;
    using Components;
    using Entitys;

    public class Field : MonoBehaviour {
        public const int ASPECT_W = 16;
        public const int ASPECT_H = 9;
        public const float SIZE = 0.32f;

        private static List<Entity> SyncList = new List<Entity>();

        public static Vector2 ToPosition(int x, int y) {
            return new Vector2(x * SIZE + SIZE * 0.5f, y * SIZE + SIZE * 0.5f);
        }

        public static void AdjustPosition(Position position, Transform transform=null) {
            transform = transform == null ? position.entity.transform : transform;
            transform.position = Field.ToPosition(position.value.x, position.value.y);
        }

        private static void Collide(Position a, Position b) {
            if (a.value == b.value) {
                if (a.collsionSlot != null) {
                    a.collsionSlot.Run(a.entity, b.entity);
                }

                if (b.collsionSlot != null) {
                    b.collsionSlot.Run(b.entity, a.entity);
                }
            }
        }

        private static void Sync(Position position, Joint joint) {
            joint.laterPos = position.value;
        }

        private float width;
        private float height;

        protected void Awake() {
            Entity.NewTickEvent += this.NewTick;
            Entity.DestroyTickEvent += this.DestroyTick;
            Director.UpdateTickEvent += this.UpdateTick;
        }

        protected void Start() {
            this.width = Camera.main.orthographicSize / 9 * 16;
            this.height = Camera.main.orthographicSize;
        }

        protected void OnDrawGizmos() {
            for (int i = -ASPECT_W + 1; i <= ASPECT_W - 1; i++) {
                Gizmos.DrawLine(new Vector3(i * SIZE, -this.height, 0), new Vector3(i * SIZE, this.height, 0));
            }
            
            for (int i = -ASPECT_H; i <= ASPECT_H; i++) {
                Gizmos.DrawLine(new Vector3(-this.width, i * SIZE, 0), new Vector3(this.width, i * SIZE, 0));
            }
        }

        private void NewTick(Entity entity) {
            bool hasPos = Position.Map.ContainsKey(entity);
            bool hasJoi = Joint.Map.ContainsKey(entity);

            if (hasPos) {
                Field.AdjustPosition(Position.Map[entity]);
            }

            if (hasPos && hasJoi) {
                Field.SyncList.Add(entity);
            }
        }

        private void UpdateTick() {
            for (int i = 0; i < Position.List.Count; i++) {
                for (int j = i + 1; j < Position.List.Count; j++) {
                    Field.Collide(Position.List[i], Position.List[j]);
                }
            }

            foreach (var entity in Field.SyncList) {
                Field.Sync(Position.Map[entity], Joint.Map[entity]);
            }
        }

        private void DestroyTick(Entity entity) {
            if (Field.SyncList.Contains(entity)) {
                Field.SyncList.Remove(entity);
            }
        }
    }
}