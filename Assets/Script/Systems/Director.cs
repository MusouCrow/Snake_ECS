using System;
using UnityEngine;

namespace Game.Systems {
    using Core;
    using Components;

    public class Director : MonoBehaviour {
        public static event Action UpdateTickEvent;
        public static event Action LateUpdateTickEvent;
        private static Director Instance;
        private static int Process;

        public static bool IsOver {
            set {
                Instance.gameObject.SetActive(!value);
                Instance.canvas.SetActive(value);

                if (value) {
                    Instance.timer = 0;
                    Director.Process = 0;
                    Director.GainInterval();
                }
            }
        }

        public static void GainInterval() {
            Director.Process++;
            Director.Instance.interval = (29f / (9f * Director.Process + 20f));
        } 

        public float interval;
        public GameObject canvas;

        private float timer;

        protected void Awake() {
            Director.Instance = this;
            Director.GainInterval();
        }

        protected void Update() {
            this.timer += Time.deltaTime;

            if (this.timer >= this.interval) {
                Director.UpdateTickEvent();
                Director.LateUpdateTickEvent();
                this.timer -= this.interval;
            }
        }
    }
}