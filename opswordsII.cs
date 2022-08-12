using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace opswordsII
{
    class opswordsII : Mod
    {
        public static Mod CalamityMod;
        public opswordsII()
        {
           
        }

        public override void AddRecipes()
        {
            RecipeMaker.AddRecipes(this);
        }

        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod")) CalamityMod = ModLoader.GetMod("CalamityMod");
            else CalamityMod = null;
        }
        public override void Unload()
        {
        }
        public static Color GetLightColor(Vector2 position)
        {
            return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
        }
        public override void PostSetupContent()
        {
            /*    Mod bossChecklist = ModLoader.GetMod("BossChecklist");
               if (bossChecklist != null){
                   bossChecklist.Call(
                       "AddBoss",
                       5.2f,
                       ModContent.NPCType<DesertAniquilator>(),
                       this, // Mod
                       "Desert Aniquilator",
                       (Func<bool>)(() => world1.downedDesert),
                       ModContent.ItemType<Items.BossSummon.DesertChest>(),
                       new List<int> { ModContent.ItemType<Items.Armor.Masks.DesertAMask>()},
                       new List<int> { ModContent.ItemType<Items.Ranger.desertbow>(), ModContent.ItemType<Items.Mele.DesertEdge>(), ModContent.ItemType<Items.Summon.DesertStaff>(), ModContent.ItemType<Items.Magic.DesertTome>()  },
                       "$Use a [i:DesertChest in the desert]"
                       );

                       bossChecklist.Call(
                       "AddBoss",
                       6.1f,
                       ModContent.NPCType<frozen_assaulter>(),
                       this, // Mod
                       "Frozen Assaulter",
                       (Func<bool>)(() => world1.downedFrozen),
                       ModContent.ItemType<Items.BossSummon.FrozenArtifact>(),
                       new List<int> { ModContent.ItemType<Items.Armor.Masks.FrozenMask>()},
                       new List<int> { ModContent.ItemType<Items.Ranger.FrostShark>(), ModContent.ItemType<Items.Mele.Permafrost>(), ModContent.ItemType<Items.Summon.FrozenStafff>(), ModContent.ItemType<Items.Magic.frozen_staff>() },
                       "$Use a FrozenArtifact in the snow at the nigth"
                       );

                       bossChecklist.Call(
                       "AddBoss",
                       10.1f,
                       ModContent.NPCType<InfernalTyrantHead>(),
                       this, // Mod
                       "Infernal Tyrant",
                       (Func<bool>)(() => world1.downedTyrant),
                       ModContent.ItemType<Items.BossSummon.InfernalCalis>(),
                       new List<int> { ModContent.ItemType<Items.Armor.Masks.FrozenMask>()},
                       new List<int> { ModContent.ItemType<Items.Ranger.FrostShark>(), ModContent.ItemType<Items.Mele.Permafrost>(), ModContent.ItemType<Items.Summon.FrozenStafff>(), ModContent.ItemType<Items.Magic.frozen_staff>() },
                       "$Use a Infernal Calis in the Underworld"
                       );       
               }*/
        }


        public int ParticlleMetter(int i)
        {
            float LagLevel = ModContent.GetInstance<ConfigClient1>().LagReducer;
            int n = 0;

            if (LagLevel == 0) n = i;
            else if (LagLevel == 1) n = i / 2;
            else if (LagLevel == 2) n = i / 4;
            else if (LagLevel == 3) n = 0;
            return n;
        }
    }
  
}

