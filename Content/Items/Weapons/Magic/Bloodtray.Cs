using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RemnantOfTheAncientsMod.Content.Projectiles.Mage;
using Microsoft.Xna.Framework.Graphics;
using RemnantOfTheAncientsMod.Content.Projectiles.HeldItem;
using RemnantOfTheAncientsMod.Common.Global;
using System.Collections.Generic;

namespace RemnantOfTheAncientsMod.Content.Items.Weapons.Magic
{
    public class Bloodtray : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }


        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.mana = 9;
            Item.rare = ItemRarityID.Blue;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 25;
            Item.UseSound = SoundID.Item34;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 14f;
            Item.useAnimation = 49;
            Item.shoot = ProjectileID.RubyBolt;
            Item.value = Item.sellPrice(silver: 30);
            Item.autoReuse = true;
            Item.GetGlobalItem<CustomTooltip>().SecondHabilitie = true;
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                Item.damage = 30;
                Item.useTime = 23;
            }
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            if (player.altFunctionUse == 2)
            {
                int chargeManaCost = 30;
                if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<BloodRayHeldProj>()] <= 0 && player.statMana >= chargeManaCost )
                {
                    float attackSpeedBonus = player.GetAttackSpeed(DamageClass.Generic);
                    Item.useTime = (int)(65 / attackSpeedBonus);
                    Item.useAnimation = (int)(65 / attackSpeedBonus);
                    player.statMana -= chargeManaCost;
                    
                    foreach(Projectile proj in Main.projectile)
                    {
                        if (proj.type == ModContent.ProjectileType<BloodRayHeldProj>() && proj.owner == player.whoAmI)
                        {
                            proj.Kill();
                        }
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<BloodRayHeldProj>()] <= 0)
                    {
                        Projectile.NewProjectile(source, player.position, Vector2.Zero, ModContent.ProjectileType<BloodRayHeldProj>(), 0, 0, Main.myPlayer, velocity.X, velocity.Y, attackSpeedBonus);
                    }
                }
                return false;
            }
            else
            {
                Item.useAnimation = 49;
                Item.useTime = RemnantOfTheAncientsMod.CalamityMod != null ? 23 : 25;
              
                return true;
            }

        }
        float RangeMax = 10;
        public float fade = 2.6f;

        
        public void SpawnParticles(Player player,Vector2 position)
        {
            const int NUM_DUSTS = 150;
            for (int j = 0; j < RemnantOfTheAncientsMod.ParticleMeter(NUM_DUSTS); j++)
            {
                Vector2 Position = Main.rand.NextVector2CircularEdge(player.width + 100, player.height + 100) + player.position;
                Vector2 Velocity = Position.DirectionTo(position) * 1f;

                var a = Dust.NewDustPerfect(Position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                a.noGravity = true;
                var m = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(new Rectangle(0, -150, 0, player.height + 230)) + player.position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                m.noGravity = true;
                var k = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(new Rectangle(-110, 0, player.width + 200, 0)) + player.position, DustID.CrimsonTorch, Velocity, 100, Color.Red, 3f);
                k.noGravity = true;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 10)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}
