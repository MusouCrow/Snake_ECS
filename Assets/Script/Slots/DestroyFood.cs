using UnityEngine;

namespace Game.Solts {
    using Core;
    using Systems;

    [CreateAssetMenuAttribute(menuName="Game/Slot/DestroyFood")]
    public class DestroyFood : Collision {
        public override void Run(Entity self, Entity opp) {
            Destroy(self.gameObject);
            Food.Bore();
            Director.GainInterval();
        }
    }
}