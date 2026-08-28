using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.ModCompativilitie.Fargos;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.World
{
	public class RemanntWorld : ModSystem
	{
		public static bool OneInMiddleMillion;
		public static bool SpawnTimeWithard;
		public static int TimeWizardTimeAcelerationCouldown;

		public override void OnWorldLoad()
		{
			TimeWizardTimeAcelerationCouldown = 0;
			SpawnTimeWithard = false;
			RemnantOfTheAncientsMod.MaxRarity = RemnantOfTheAncientsMod.GetMaxRarity();

            ModifyAccsesories.UpdateFallSpeedList();

			ModLoader.TryGetMod("SangarUtilities", out Mod Terraria);
            RemnantOfTheAncientsMod.Terraria = Terraria; 
        }
        
        public override void Load()
        {
			if(RemnantOfTheAncientsMod.FargosSoulMod != null) LoadFargos();
            base.Load();
        }
        [JITWhenModsEnabled("FargowiltasSouls")]
        public void LoadFargos()
		{
            if (RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                Type t = typeof(FargosToggles);
                RemnantOfTheAncientsMod.LoadTogglesFromType(t);
            }
        }
        public override void OnWorldUnload()
		{
			TimeWizardTimeAcelerationCouldown = 0;
			SpawnTimeWithard = false;
		}

		public static bool ExistTileInWorld(int TileId)
		{

			int worldWidth = Main.maxTilesX;
			int worldHeight = Main.maxTilesY;

			for (int x = 0; x < worldWidth; x++)
			{
				for (int y = 0; y < worldHeight; y++)
				{
					Tile tile = Main.tile[x, y];
					if (tile.HasTile && tile.TileType == TileId) return true;
				}
			}
			
			return false;
		}

        public override void PostUpdateWorld()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && Main.dayTime && Main.time == 0)
            {
				foreach (Player player in Main.player)
				{
					if(!player.active) continue;
                    RemnantPlayer modPlayer = player.GetModPlayer<RemnantPlayer>();
					foreach(int npc in modPlayer.PlayerTalkToday.Keys) modPlayer.PlayerTalkToday[npc] = false;            
                }
            
            }
        }

        private static readonly HashSet<int> tombsID = [43, 201, 202, 203, 204, 205, 527, 528, 529, 530, 531];
        public static void KillTombstom()
		{
            foreach (var projectile in Main.projectile)
            {
                if (!projectile.active || !tombsID.Contains(projectile.type)) continue;

                projectile.timeLeft = 1;
                projectile.Kill();
            }        
		}      
    }
}

