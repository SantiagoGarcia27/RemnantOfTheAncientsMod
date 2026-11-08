using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Common.Drops.DropRules;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Core;
using RemnantOfTheAncientsMod.Content.Items.Consumables.ReaperSouls;
using RemnantOfTheAncientsMod.Content.Items.Placeables.MusicBox;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Magic;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.Flail;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Melee.saber;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Bows;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Ranger.Rep;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Scrolls;
using RemnantOfTheAncientsMod.Content.Items.Weapons.Summon;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.DesertAnnihilator;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.FrozenAssaulter;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.GemstoneCrusher;
using RemnantOfTheAncientsMod.Content.NPCs.Bosses.ITyrant;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Consumables.tresure_bag
{
    public class TreasureRock : BossBagItem
    {
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemType<Terra_core>()));
            itemLoot.Add(ItemDropRule.Common(ItemType<InfernalMusicBox>(), 7));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<GemstoneCrusher>()));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<WandOfCaverns>(), ItemType<StoneBlunderbuss>(), ItemType<StoneMortar>(), ItemType<RockAndRoll>()));
            itemLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.CrusherReaperSoulRule(), ItemType<CrusherSoul>()));
        }
    }

    public class DesertBag : BossBagItem
    {
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemType<DesertMusicBox>(), 7));
            itemLoot.Add(ItemDropRule.Common(ItemType<Desert_Core>(), 1, 1, 1));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<DesertAnnihilator>()));
            itemLoot.Add(ItemDropRule.Common(ItemType<DesertAniquilatorScroll>(), 5));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<DesertBow>(), ItemType<DesertEdge>(), ItemType<DesertTome>(), ItemType<DesertStaff>()));
            itemLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.DesertAnhilatorReaperSoulRule(), ItemType<DesertAnnihilatorSoul>()));
        }
    }
    public class FrostBag : BossBagItem
    {
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<FrozenAssaulter>()));
            itemLoot.Add(ItemDropRule.Common(ItemType<FrozenMusicBox>(), 7));
            itemLoot.Add(ItemDropRule.Common(ItemType<Frozenp2MusicBox>(), 7));
            itemLoot.Add(ItemDropRule.Common(ItemType<Frost_core>(), 1, 1, 1));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<FrozenAssaulter>())); ;
            // itemLoot.Add(ItemDropRule.Common(ItemType<DScroll>(), 5));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<FrostShark>(), ItemType<Permafrost>(), ItemType<frozen_staff>(), ItemType<FrozenStafff>()));
            itemLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.FrozenAssaulterReaperSoulRule(), ItemType<FrozenAssaulterSoul>()));
        }
    }

    public class InfernalBag : BossBagItem
    {
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemType<Infernal_core>()));
            itemLoot.Add(ItemDropRule.Common(ItemType<InfernalMusicBox>(), 7));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCType<InfernalTyrantBody>()));
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<TyrantRepeater>(), ItemType<SpikeSaber>(), ItemType<Tyran_Blast>(), ItemType<EvilEyeStaff>()));
            itemLoot.Add(ItemDropRule.ByCondition(new RemnantConditions.InfernalTyrantReaperSoulRule(), ItemType<InfernalTyrantSoul>()));
        }
    }

    public class StarChunk : BossBagItem
    {
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 1, 5, 20));
        }
    }

    public abstract class BossBagItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 38;
            Item.height = 36;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
        }

        public override bool CanRightClick() => true;
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Lerp(lightColor, Color.White, 0.4f);
        }

        public override void PostUpdate()
        {
            // Spawn some light and dust when dropped in the world
            Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                // This creates a randomly rotated vector of length 1, which gets it's components multiplied by the parameters
                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() / 2f;
                Vector2 velocity = new(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
                dust.scale = 0.5f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            // Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
            {
                // In case this item is animated, this picks the correct frame
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            }
            else
            {
                frame = texture.Frame();
            }

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
            {
                time = 2f - time;
            }

            time = (time / 2f) + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;

                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}


