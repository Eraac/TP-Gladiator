﻿using System.Collections.Generic;
using TP0.Entity;

namespace TP0.Gameplay
{
    class Tournament
    {
        private List<Player> _players;
        private List<Pool> _pools;

        public Tournament(List<Player> players)
        {
            this._players = players;
            this._pools = new List<Pool>();
        }

        public void selectPools()
        {
            List<Team> teams = new List<Team>();

            foreach(Player player in this._players)
            {
                teams.Add(player.Teams[0]); // TODO : Laisser le joueur choisir la team + ajouter assert
            }

            teams.Sort(compareTeamByVictory);

            int count = teams.Count;

            for (int i = 0; i < count; i += 2)
            {
                // Le cas où il y a un nombre impair d'équipe
                if (i + 1 >= count) {
                    break;
                }

                Pool pool = new Pool(teams[i], teams[i + 1]);
                this._pools.Add(pool);
            }
        }

        private static int compareTeamByVictory(Team a, Team b)
        {
            float aVictory = a.pourcentageVictory();
            float bVictory = b.pourcentageVictory();

            if (aVictory > bVictory) {
                return 1;
            } else if (bVictory > aVictory) {
                return -1;
            } else {
                return 0;
            }
        }
    }
}