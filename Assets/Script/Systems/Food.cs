using UnityEngine;

namespace Game.Systems {
    using Core;
    using Components;

    public class Food : MonoBehaviour {
        private static Food Instance;

        public static void Bore() {
            int count = (int)Mathf.Lerp(Instance.min, Instance.max, Random.value);

            for (int i = 0; i < count; i++) {
                var obj = GameObject.Instantiate(Instance.foodPrefab);
                var entity = obj.GetComponent<Entity>();
                var pos = Position.Map[entity];
                
                pos.value.x = (int)Mathf.Lerp(-Field.ASPECT_W + 1, Field.ASPECT_W - 1, Random.value);
                pos.value.y = (int)Mathf.Lerp(-Field.ASPECT_H + 1, Field.ASPECT_H - 1, Random.value);
                Field.AdjustPosition(pos);
            }
        }

        public static void Clear() {
            var bodys = GameObject.FindGameObjectsWithTag("Body");

            for (int i = 0; i < bodys.Length; i++) {
                Destroy(bodys[i]);
            }
        }

        public GameObject foodPrefab;
        public int min;
        public int max;

        protected void Awake() {
            Food.Instance = this;
        }

        protected void Start() {
            Food.Bore();
        }
    }
}