using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace RemnantOfTheAncientsMod.Common.PlayerProxy
{
    public static class FakePlayers
    {
        private static readonly Dictionary<int, Player> _players = [];

        public static Player Get(NPC npc)
        {
            if (!_players.TryGetValue(npc.whoAmI, out Player player))
            {
                player = new Player();
                _players[npc.whoAmI] = player;
                Main.player[2] = player;
            }

            Sync(player, npc);

            return player;
        }
        public static void Remove(NPC npc)
        {
            _players.Remove(npc.whoAmI);
            Main.player[2].active = false;
        }

        private static void Sync(Player player, NPC npc)
        {
            player.position = npc.position;
            player.Center = npc.Center;
            player.velocity = npc.velocity;
            player.direction = npc.direction;
            player.active = npc.active;
            player.dead = npc.life <= 0;
            player.whoAmI = 0;                      
        }

        
    }
}

