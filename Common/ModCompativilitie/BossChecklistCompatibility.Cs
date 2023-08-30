using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DAniquilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.frozen_assaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie
{
    public class BossChecklistCompatibility : ModSystem
    {
        public override void PostSetupContent()
        {
            DoBossChecklistIntegration();
        }

        private void DoBossChecklistIntegration()
        {
            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }
            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }
            AddBoses(bossChecklistMod);
            

        }
        public void AddBoses(Mod bossChecklistMod)
        {
            AddDesert(bossChecklistMod);
            AddFrozen(bossChecklistMod);
            AddTyrant(bossChecklistMod);
        }

        List<int> collection;
        Action customPortrait;
        public void AddDesert(Mod bossChecklistMod)
        {
            collection = new List<int>()
                {
                ModContent.ItemType<DesertTrophy>(),
                ModContent.ItemType<DesertAMask>(),
                ModContent.ItemType<DesertMusicBox>()
                };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                "DesertAniquilator",    
                6.8f,
                () => RemnantDownedBossSystem.downedDesert,
                ModContent.NPCType<DesertAniquilator>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<DesertChest>(),
                    ["collectibles"] = collection
                }
            );
        }
        public void AddFrozen(Mod bossChecklistMod)
        {
            collection = new List<int>()
            {
                ModContent.ItemType<FrostTrophy>(),
                ModContent.ItemType<FrozenMask>(),
                ModContent.ItemType<FrozenMusicBox>(),
                ModContent.ItemType<Frozenp2MusicBox>()
            };

             bossChecklistMod.Call(
                "LogBoss",
                Mod,
                "FrozenAssaulter",    
                7.2f, 
                () => RemnantDownedBossSystem.downedFrozen,
                ModContent.NPCType<FrozenAssaulter>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<FrozenArtifact>(),
                    ["collectibles"] = collection,
                }
            );


        }
        public void AddTyrant(Mod bossChecklistMod)
        {

            collection = new List<int>()
            {
                ModContent.ItemType<InfernalTrophy>(),
                ModContent.ItemType<InfernalMask>(),
                ModContent.ItemType<InfernalMusicBox>()
            };

            var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) => {
                Texture2D texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/InfernalTyrant_BossChecklist").Value;
                Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                "InfernalTyrantHead",       
                13f,
                () => RemnantDownedBossSystem.downedTyrant,
                ModContent.NPCType<InfernalTyrantHead>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<InfernalCalis>(),
                    ["collectibles"] = collection,
                    ["customPortrait"] = customPortrait
                }
            );
        }
    }
}
