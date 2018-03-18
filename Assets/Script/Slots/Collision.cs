using UnityEngine;

namespace Game.Solts {
    using Core;

    public class Collision : Slot {
        public virtual void Run(Entity self, Entity opp) {}
    }
}