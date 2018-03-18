using UnityEngine;

namespace Game.Solts {
    using Core;
    using Entitys;
    using Systems;

    [CreateAssetMenuAttribute(menuName="Game/Slot/Eating")]
    public class Eating : Collision {
        public override void Run(Entity self, Entity opp) {
            if (opp.GetType() == typeof(Entitys.Food)) {
                Systems.Body.WillBore = true;
            }
            else if (opp.GetType() == typeof(Entitys.Body)) {
                Director.IsOver = true;
            }
        }
    }
}