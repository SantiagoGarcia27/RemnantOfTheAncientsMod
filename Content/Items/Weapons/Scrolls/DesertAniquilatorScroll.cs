using RemnantOfTheAncientsMod.Common.RemPlayer;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Buffs.Buffs.Scrolls;
using RemnantOfTheAncientsMod.Content.Items.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Scrolls
{
    public class DesertAniquilatorScroll : ModItem
    {
        int MovmentSpeedBonus = 50;
        int MinionDamage = 5;
        int ExtraMinions = 1;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MovmentSpeedBonus,MinionDamage,ExtraMinions);
        public override void SetStaticDefaults()
        { 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.DamageType = DamageClass.Generic;
            Item.noMelee = true;
            Item.mana = 50;
            Item.rare = ItemRarityID.Orange;
            Item.width = 46;
            Item.height = 54;
            Item.useTime = 299;
            Item.UseSound = SoundID.Item34;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 199;
            Item.value = Item.sellPrice(gold: 1);
            Item.autoReuse = false;
            Item.buffType = BuffType<MasterD>();
            Item.buffTime = 9999999;
            Item.expert = true;

        }
        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<RemnantPlayer>().PlayerHaveScroll())
            {
                int currentScrollId = player.GetModPlayer<RemnantPlayer>().SearchCurrenScrollEffect();

                if (currentScrollId != -1)
                {
                    player.ClearBuff(currentScrollId);
                    player.AddBuff(Item.buffType, Utils1.FormatTimeToTick(14, 0, 0, 0));
                    return false;
                }
            }
            else
            {
                player.AddBuff(Item.buffType, Utils1.FormatTimeToTick(14, 0, 0, 0));
                return false;
            }
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<KingSlimeScroll>(), 1)
            .AddIngredient(ItemType<Sand_escense>(), 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}