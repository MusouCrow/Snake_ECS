using UnityEngine;
using UnityEngine.UI;

namespace Game {
    using Systems;
    using Components;

    public class Restart : MonoBehaviour {
        private Button button;

        protected void Awake() {
            this.button = this.GetComponent<Button>();
            this.button.onClick.AddListener(this.OnClick);
        }

        private void OnClick() {
            var uis = GameObject.FindGameObjectsWithTag("UI");

            for (int i = 0; i < uis.Length; i++) {
                uis[i].GetComponent<Gradients>().enabled = true;
            }

            Body.Clear();
            Food.Clear();
            Food.Bore();
            Head.Reset();
            Director.IsOver = false;
        }
    }
}
