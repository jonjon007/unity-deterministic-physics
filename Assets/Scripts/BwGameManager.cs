using SharedGame;
using System.Collections.Generic;
using UnityGGPO;

namespace BallWar {

    public class BwGameManager : GameManager {

        public override void StartLocalGame() {
            StartGame(new LocalRunner(new BwGame(2)));
        }

        public override void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex) {
            var game = new GGPORunner("ballwar", new BwGame(connections.Count), perfPanel);
            game.Init(connections, playerIndex);
            StartGame(game);
        }
    }
}