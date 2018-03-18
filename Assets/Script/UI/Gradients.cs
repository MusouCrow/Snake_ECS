using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class Gradients : MonoBehaviour {
        private const float TIME = 1;

        private Graphic graphic;

        private Color originColor;
        private Color targetColor;
        private float timer;

        protected void Awake() {
            this.graphic = this.GetComponent<Graphic>();

            this.targetColor = this.graphic.color;
            this.originColor = this.targetColor;
            this.originColor.a = 0;
        }

        protected void OnEnable() {
            this.timer = 0;
            this.graphic.color = this.originColor;
        }

        private void Update() {
            this.timer += Time.deltaTime;
            this.graphic.color = Color.Lerp(this.originColor, this.targetColor, this.timer / TIME);
            
            if (this.timer >= TIME) {
                this.enabled = false;
            }
        }
    }
}
