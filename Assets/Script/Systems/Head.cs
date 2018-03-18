using System.Collections.Generic;
using UnityEngine;

namespace Game.Systems {
    using Core;
    using Components;

    public class Head : MonoBehaviour {
        public static void Reset() {
            var entity = Entitys.Head.Instance;
            entity.position.value = Vector2Int.zero;
            Joint.Tail = entity.joint;
            Field.AdjustPosition(entity.position);
        }
        
        private static void Move(Position position, Direction direction, Joint joint) {
            joint.laterPos = position.value;
            
            if (direction.Value == Direction.Enum.up) {
                position.value.y += 1;
            }
            else if (direction.Value == Direction.Enum.down) {
                position.value.y -= 1;
            }
            else if (direction.Value == Direction.Enum.left) {
                position.value.x -= 1;
            }
            else if (direction.Value == Direction.Enum.right) {
                position.value.x += 1;
            }

            if (position.value.x > Field.ASPECT_W - 1) {
                position.value.x = -Field.ASPECT_W;
            }
            else if (position.value.x < -Field.ASPECT_W) {
                position.value.x = Field.ASPECT_W - 1;
            }

            if (position.value.y > Field.ASPECT_H - 1) {
                position.value.y = -Field.ASPECT_H;
            }
            else if (position.value.y < -Field.ASPECT_H) {
                position.value.y = Field.ASPECT_H - 1;
            }

            Field.AdjustPosition(position);
        }

        protected void Awake() {
            Director.UpdateTickEvent += this.UpdateTick;
        }

        protected void Update() {
            for (int i = 0; i < 4; i++) {
                if (Input.GetKeyDown((KeyCode)(i + KeyCode.UpArrow))) {
                    var v = (Direction.Enum)i;
                    
                    foreach (var direction in Direction.Map.Values) {
                        direction.Value = v;
                    }

                    break;
                }
            }
        }

        private void UpdateTick() {
            var entity = Entitys.Head.Instance;
            Head.Move(entity.position, entity.direction, entity.joint);
        }
    }
}