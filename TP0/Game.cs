﻿using TP0.Entity;
using TP0.Gameplay;
using TP0.Loader;
using System.Collections.Generic;
using System;

namespace TP0
{
    class Game
    {
        public Game() {}

        public void run()
        {
            // 1. Load players
            LoadPlayer loadPlayer = new LoadPlayer();
            List<Player> players = loadPlayer.loadAll();

            // 2. Run tournament
            Tournament tournament = new Tournament(players);
            tournament.selectPools();
            tournament.match();
        }
    }
}
