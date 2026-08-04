using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Systems;
using RemnantOfTheAncientsMod.Content.Items.Armor.Masks;
using RemnantOfTheAncientsMod.Content.Items.Consumables.BossSummon;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using RemnantOfTheAncientsMod.Content.Items.Placeables.Trophy;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.ModCompativilitie
{
    public class  BossChecklistCompatibility : ModSystem
    {
        public override void PostSetupContent()
        {
            BossChecklistIntegration();
        }

        private void BossChecklistIntegration()
        {
            if (!ModLoader.TryGetMod("BossChecklist", out Mod BossChecklistMod))
            {
                return;
            }
            if (BossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }
            AddBoses(BossChecklistMod);
            

        }
        public void AddBoses(Mod BossChecklistMod)
        {
            AddDesert(BossChecklistMod);
            AddFrozen(BossChecklistMod);
            AddTyrant(BossChecklistMod);
        }

        List<int> collection;
        Action customPortrait;
        public void AddDesert(Mod BossChecklistMod)
        {
            collection =
                [
                ModContent.ItemType<DesertTrophy>(),
                ModContent.ItemType<DesertAMask>(),
                ModContent.ItemType<DesertMusicBox>()
                ];

                BossChecklistMod.Call(
                "LogBoss",
                Mod,
                "DesertAnnihilator",    
                6.7f,
                () => RemnantDownedBossSystem.downedDesert,
                ModContent.NPCType<DesertAnnihilator>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<DesertGlyph>(),
                    ["collectibles"] = collection
                }
            );
        }
        public void AddFrozen(Mod BossChecklistMod)
        {
            collection =
            [
                ModContent.ItemType<FrostTrophy>(),
                ModContent.ItemType<FrozenMask>(),
                ModContent.ItemType<FrozenMusicBox>(),
                ModContent.ItemType<Frozenp2MusicBox>()
            ];

                BossChecklistMod.Call(
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
        public void AddTyrant(Mod BossChecklistMod)
        {

            collection =
            [
                ModContent.ItemType<InfernalTrophy>(),
                ModContent.ItemType<InfernalMask>(),
                ModContent.ItemType<InfernalMusicBox>()
            ];

            var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) => {
                Texture2D texture = ModContent.Request<Texture2D>("RemnantOfTheAncientsMod/Content/NPCs/Bosses/ITyrant/InfernalTyrant_BossChecklist").Value;
                Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

                BossChecklistMod.Call(
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
