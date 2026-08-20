/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.PlayerProxy
{
    public static class FakePlayers
    {
        private static readonly Dictionary<NPC, int> _players = [];

        public static Player GetPlayerProxy(this NPC npc, bool visible = false)
        {
            Player player = null;
            if (!_players.TryGetValue(npc, out int playerwhoAmI))
            {
                int index = GetAvailableWhoAmI();
                if (index <= -1) return null;

                _players[npc] = index;
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
        public static Player UpdatePlayerProxy(NPC npc, bool visible = false) => GetPlayerProxy(npc, visible);
        public static void DisposePlayerProxy(this NPC npc)
        {
            if (!_players.TryGetValue(npc, out int playerWhoAmI)) return;
            Main.player[playerWhoAmI].Reset();
            _players.Remove(npc);  
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

        private static void GarbageCollector()
        {
            if (_players.Count == 0) return;
            foreach (Player player in Main.player)
            {
                if (player == null || !player.active) continue;
                if (!player.GetModPlayer<FakePlayer>().isFakePlayer) continue;
                NPC npc = _players.FirstOrDefault(x => x.Value == player.whoAmI).Key;
                if (npc == null) continue;
                if (Main.npc.IndexInRange(npc.whoAmI) && ReferenceEquals(npc, Main.npc[npc.whoAmI])) continue;

                player.Reset();
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
            int reservedSpaceForRealPlayers = Main.netMode == NetmodeID.SinglePlayer ? 0 : 5; 

            for (int i = Main.player.Length - 2; i >= reservedSpaceForRealPlayers; i--)
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
*/
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

public class ForceAttackPlayer : ModPlayer
{
    public bool forceLeftClick = false;

    public override void ResetEffects()
    {
        // Se resetea en cada frame para que el NPC deba mantenerlo activo
        forceLeftClick = false;
    }

    public override void SetControls()
    {
        if (forceLeftClick)
        {
            if (Player.HeldItem.type != ItemID.Mace)
            {
                Player.inventory[Player.selectedItem].SetDefaults(ItemID.Mace);
            }

            // 2. Simular clic mantenido y liberación previa para iniciar la animación
            Player.controlUseItem = true;

            if (Player.itemAnimation == 0)
            {
                // Libera la condición para permitir un nuevo uso/ataque
                Player.releaseUseItem = true;
            }

            // 3. Forzar al motor a evaluar el uso del ítem si aún no está atacando
            if (Player.itemAnimation == 0 && Player.itemTime == 0)
            {
                Player.ItemCheck();
            }
        }
    }   
}
public static class KeySystemPublicApi
{
    public static void Click(this Player player)
    {
        int a = player.whoAmI;
        player.GetModPlayer<ForceAttackPlayer>().forceLeftClick = true;
    }
    public static void StopClick(this Player player)
    {
        player.GetModPlayer<ForceAttackPlayer>().forceLeftClick = false;
    }

}