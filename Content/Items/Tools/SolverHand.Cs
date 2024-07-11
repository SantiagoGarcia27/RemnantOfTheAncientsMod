using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using RemnantOfTheAncientsMod.Common.Global;
using RemnantOfTheAncientsMod.Content.Projectiles.Multiclass;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Items;

namespace RemnantOfTheAncientsMod.Content.Items.Tools
{
    public class SolverHand : ModItem
    {
        public override void SetStaticDefaults()
        {         
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            //Item.damage = 60;
            Item.noMelee = true;
            //Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            //Item.mana = 6;
            Item.rare = ItemRarityID.Red;
            Item.width = 72;
            Item.height = 74;
            Item.useTime = 5;
            Item.useAnimation = 5;
            //Item.UseSound = SoundID.Item34;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 14f;
            //Item.shoot = ProjectileID.Flames;
            Item.value = Item.sellPrice(gold: 8);
            Item.autoReuse = true;
            Item.scale = 1f;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
        }
        public override bool CanUseItem(Player player)
        {
            int maxMode = 1;
            if (player.altFunctionUse == 2)
            {
                if (RemnantPlayer.SolverMode < maxMode /*|| RemnantPlayer.SolverMode == 1*/)
                {
                    RemnantPlayer.SolverMode++;
                }
                else
                {
                    RemnantPlayer.SolverMode = 0;
                }
                switch (RemnantPlayer.SolverMode)
                {
                    case 0:
                        CombatText.NewText(player.Hitbox, Color.Purple, this.GetLocalization("Move").Value, dramatic: true);
                        break;
                    case 2:
                        CombatText.NewText(player.Hitbox, Color.Purple, this.GetLocalization("BlackHole").Value, dramatic: true);
                        break;
                    case 1:
                        CombatText.NewText(player.Hitbox, Color.Purple, this.GetLocalization("Break").Value, dramatic: true);
                        break;
                }
            }
            return base.CanUseItem(player);
        }
        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SolverCursor>()] <= 0)
                Projectile.NewProjectile(Terraria.Entity.GetSource_None(), new(Main.mouseX, Main.mouseY), Vector2.Zero, ModContent.ProjectileType<SolverCursor>(), 1, 0);
            base.HoldItem(player);
        }
        public override bool AltFunctionUse(Player player) => true;
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<MechanicalProsthesis>(1)
            .AddIngredient(ItemID.LunarBar, 5)
            .AddIngredient(ItemID.MartianConduitPlating, 10)
            .AddIngredient(ItemID.SoulofFright, 4)
            .AddIngredient<CelestialAmalgamate>(5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }
    }
}