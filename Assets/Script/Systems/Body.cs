using System.Collections.Generic;
using UnityEngine;

namespace Game.Systems {
    using Core;
    using Components;

    public class Body : MonoBehaviour {
        public static bool WillBore;
        private static List<Entity> FollowList = new List<Entity>();
        private static Body Instance;

        public static void Clear() {
            var foods = GameObject.FindGameObjectsWithTag("Food");

            for (int i = 0; i < foods.Length; i++) {
                Destroy(foods[i]);
            }
        }

        private static void Follow(Joint joint, Position position) {
            if (joint.prior == null || !Position.Map.ContainsKey(joint.prior.entity)) {
                return;
            }

            joint.laterPos = position.value;
            position.value = joint.prior.laterPos;
            Field.AdjustPosition(position);
        }

        private static void Bore() {
            var prior = Joint.Tail.entity;
            var obj = GameObject.Instantiate(Instance.bodyPrefab);
            var entity = obj.GetComponent<Entity>();
            
            Position.Map[entity].value = Position.Map[prior].value;
            Field.AdjustPosition(Position.Map[entity]);

            Joint.Map[prior].next = Joint.Map[entity];
            Joint.Map[entity].prior = Joint.Map[prior];
        }

        public GameObject bodyPrefab;

        protected void Awake() {
            Instance = this;

            Entity.NewTickEvent += this.NewTick;
            Entity.DestroyTickEvent += this.DestroyTick;
            Director.UpdateTickEvent += this.UpdateTick;
            Director.LateUpdateTickEvent += this.LateUpdateTick;
        }

        private void NewTick(Entity entity) {
            bool hasJoi = Joint.Map.ContainsKey(entity);
            bool hasPos = Position.Map.ContainsKey(entity);

            if (hasJoi && hasPos) {
                Body.FollowList.Add(entity);
            }
        }

        private void UpdateTick() {
            foreach (var entity in Body.FollowList) {
                Body.Follow(Joint.Map[entity], Position.Map[entity]);
            }
        }

        private void LateUpdateTick() {
            if (Body.WillBore) {
                Body.Bore();
                Body.WillBore = false;
            }
        }

        private void DestroyTick(Entity entity) {
            if (Body.FollowList.Contains(entity)) {
                Body.FollowList.Remove(entity);
            }
        }
    }
}