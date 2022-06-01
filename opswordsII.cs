using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using opswordsII;

namespace OpswordsII
{
    class OpswordsII : Mod
    {
        internal bool CalamityLoaded;
        internal bool ThoriumLoaded;

        public OpswordsII()
        {
        }
        /*public override void AddRecipes()
		{
           Recipe.AddRecipes();
    
            if (CalamityLoaded)
			{
				CalamityRecipes();
			}

			RecipeHelper.ExampleRecipeEditing(this);
        }*/

        public override void AddRecipes()
        {
            RecipeMaker.AddRecipes(this);
        }
        public override void AddRecipeGroups()
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
        private void CalamityRecipes()
		{

        }
        public override void Load() {
            if (!Main.dedServ) {
              /*  AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Desert Aniquilator"), ItemType("DesertMusicBox"), TileType("DesertMusicBoxT"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Frozen Assaulter"), ItemType("FrozenMusicBox"), TileType("FrozenMusicBoxT"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Frozen Theme p2 "), ItemType("Frozenp2MusicBox"), TileType("Frozenp2MusicBoxT"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Infernal_Tyrant"), ItemType("InfernalMusicBox"), TileType("InfernalMusicBoxT"));*/
            }
        }
    }
}

