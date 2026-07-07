using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.PlayerProxy
{
    public static class FakePlayers
    {
        private static readonly Dictionary<int, int> _players = [];

        public static Player Get(NPC npc, bool visible = false)
        {
            Player player = null;
            if (!_players.TryGetValue(npc.whoAmI, out int playerwhoAmI))
            {
                int index = GetAvailableWhoAmI();
                if (index <= -1) return null;
                _players[npc.whoAmI] = index;
                Main.player[index] = new Player()
                {
                    whoAmI = index,
                    active = true
                };
                playerwhoAmI = index;
            }

            player = Main.player[playerwhoAmI];
            Sync(player, npc,visible);

            return player;
        }
        public static Player Update(NPC npc, bool visible = false) => Get(npc, visible);
        public static void Remove(int npcWhoAmI)
        {
            if (!_players.TryGetValue(npcWhoAmI, out int playerWhoAmI)) return;
            Main.player[playerWhoAmI].Reset();
            _players.Remove(npcWhoAmI);  
        }

        private static void Sync(Player player, NPC npc, bool visible)
        {
            player.position = npc.position;
            player.Center = npc.Center;
            player.velocity = npc.velocity;
            player.direction = npc.direction;
            player.active = npc.active;
            player.dead = npc.life <= 0;
            player.GetModPlayer<FakePlayer>().isFakePlayer = true;
            if (!visible)
            {
                player.GetModPlayer<FakePlayer>().shouldBeInvisible = true;

            }
        }
        private static void Reset(this Player player)
        {
            int whoAmI = player.whoAmI;
            Main.player[whoAmI] = new Player()
            {
                whoAmI = whoAmI,
                active = false
            };
        }

        private static int GetAvailableWhoAmI()
        {
            for (int i = Main.player.Length - 2; i >= 0; i--)
            {
                if (!Main.player[i].active) return i;   
            }
            return -1;
        }


    }

    public class FakePlayer : ModPlayer
    {
        public bool shouldBeInvisible;
        public bool isFakePlayer;
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if(!shouldBeInvisible) return;
            PlayerDrawLayers.Head.Hide();
            PlayerDrawLayers.Torso.Hide();
            PlayerDrawLayers.Leggings.Hide();
            PlayerDrawLayers.ArmOverItem.Hide();
            PlayerDrawLayers.Skin.Hide();
        }
    }
}

