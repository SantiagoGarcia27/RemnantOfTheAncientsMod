using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Tools;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;


namespace RemnantOfTheAncientsMod.Content.Projectiles.Multiclass
{
    public class SolverCursor : ModProjectile
    {
        public override string Texture => "RemnantOfTheAncientsMod/Assets/PlaceHolder";
        public override void SetStaticDefaults(){}
        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200000;
            Projectile.light = 1.0f;
            Projectile.extraUpdates = 5;
            Projectile.ignoreWater = true;
            Projectile.scale = 1f;
            AIType = -1;
        }
        public string GetTexture()
        {
            Player player = Main.player[Projectile.owner];
            switch (player.GetModPlayer<RemnantPlayer>().SolverMode)
            {
                case 0:
                    return "RemnantOfTheAncientsMod/Content/Projectiles/Multiclass/SolverMove";
                case 1:
                    return "RemnantOfTheAncientsMod/Content/Projectiles/Multiclass/SolverBreak";
                default:
                    return "RemnantOfTheAncientsMod/Assets/PlaceHolder";
            }
        }
        public static NPC target = null;
        public Tile tile_;

        public override void AI()
        {
            CheckActive();
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.position = FakeMain.MouseWorld(player);
                Projectile.netUpdate = true;

                // Projectile.position = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                if (player.GetModPlayer<RemnantPlayer>().SolverMode == 0)
                {

                    Rectangle hitbox = Projectile.Hitbox;
                    if (Main.mouseLeft)
                    {
                        // Todavía no agarramos nada: buscar NPC bajo el cursor.
                        if (target == null)
                        {
                            foreach (NPC n in Main.ActiveNPCs)
                            {
                                if (n.boss || n.lifeMax > 1000)
                                    continue;

                                if (hitbox.Intersects(n.Hitbox))
                                {
                                    target = n;
                                    break;
                                }
                            }
                        }

                        // Si tenemos uno agarrado, moverlo.
                        if (target != null)
                        {
                            if (!target.active || target.life <= 0)
                            {
                                target = null;
                            }
                            else
                            {
                                target.Center = Projectile.Center;
                                target.velocity = Vector2.Zero;
                                target.netUpdate = true;
                            }
                        }
                    }
                    else
                    {
                        // Soltamos el click -> soltar NPC.
                        target = null;
                    }
                }
                else if (player.GetModPlayer<RemnantPlayer>().SolverMode == 1)
                {
                    if (Projectile.owner == Main.myPlayer)
                    {
                        if (Main.mouseLeft)
                        {
                            Vector2 centerTile = Projectile.Center.ToTilePosition();
                            int centerTileX = (int)centerTile.X;
                            int centerTileY = (int)centerTile.Y;

                            if (Utils1.GetBlockPickPower(Main.tile[centerTileX, centerTileY], centerTileY) <= Utils1.GetBestPickPower(player))
                            {
                                WorldGen.KillTile(centerTileX, centerTileY);
                                if (!Main.tile[centerTileX, centerTileY].HasTile && Main.netMode != NetmodeID.SinglePlayer)
                                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, centerTileX, centerTileY);
                            }
                            else
                            {
                                NetworkText text = NetworkText.FromLiteral($"{player.name} was consumed by Solver");
                                PlayerDeathReason deathReason = PlayerDeathReason.ByCustomReason(text);

                                player.Hurt(deathReason, player.statLifeMax / 4, player.direction, false, false, -1, false, 10000, 1, 2);

                                Dust.NewDust(player.position, 10, 10, DustID.Electric, 0, 0, 0, Color.Red, 1.0f);
                            }
                        }
                    }
                }
            }
        }
        public void CheckActive()
        {
            if (Main.player[Projectile.owner].HeldItem.type != ModContent.ItemType<SolverHand>())
            {
                Projectile.Kill();
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return Main.myPlayer == Projectile.owner && Main.mouseRight;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnKill(int timeLeft){}

        public override bool PreDraw(ref Color lightColor)
        {
            var texture = ModContent.Request<Texture2D>(GetTexture());

            Vector2 position = Projectile.Center - Main.screenPosition - new Vector2(10, 10);
            Vector2 origin = new(texture.Width() * 0.5f, texture.Height() * 0.5f);
            //Main.spriteBatch.Draw((Texture2D)texture, position, null, Color.Purple, Projectile.rotation, origin,Projectile.scale * 0.5f, SpriteEffects.None, 0f);
            return false;
        }
    }
    [Autoload(Side = ModSide.Client)]
    public class SolverOverrideCursor : ModSystem
    {
        private static float cursorScale = 1f;
        private static Vector2 cursorPosition = Vector2.Zero;
        private static LegacyGameInterfaceLayer cursorLayer;

        private Asset<Texture2D>[] cursorTextures =
        [
            ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0"),
            ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0"),
            ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0"),
        ];
        public override void Load()
        {
            IncertListOfAssets();
            cursorLayer = new LegacyGameInterfaceLayer($"{nameof(Mod)}: My Cursor", () =>
            {
                Player player = Main.LocalPlayer;

                Asset<Texture2D> texture = ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0");

                texture = player.GetModPlayer<RemnantPlayer>().SolverMode switch
                {
                    0 => cursorTextures[1],
                    1 => cursorTextures[2],
                    _ => cursorTextures[0],
                };
                if (!texture.IsLoaded)
                    return true;

                Vector2 hotspot = new Vector2(20f, 20f);

                Main.spriteBatch.Draw(texture.Value, Main.MouseScreen - hotspot, null, Color.Purple, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                return true;
            }, InterfaceScaleType.UI);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            List<string> vanillaCursors = [
                "Cursor",
                "Cursor Info",
                "Mouse Item"
            ];
            if (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<SolverHand>())
            {
                foreach (string cursorName in vanillaCursors)
                {
                    int cursorIndex = layers.FindIndex(layer => layer.Name == $"Vanilla: {cursorName}");
                    if (cursorIndex != -1)
                    {
                        layers.RemoveAt(cursorIndex);
                    }
                }

                int defaultCursorIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Cursor");
                if (defaultCursorIndex != -1)
                {
                    layers.Insert(defaultCursorIndex, cursorLayer);
                }
                else
                {
                    layers.Add(cursorLayer);
                }
            }
        }
        public void IncertListOfAssets()
        {
            cursorTextures[0] = ModContent.Request<Texture2D>("Terraria/Images/UI/Cursor_0");
            cursorTextures[1] = Mod.Assets.Request<Texture2D>("Content/Projectiles/Multiclass/SolverMove");
            cursorTextures[2] = Mod.Assets.Request<Texture2D>("Content/Projectiles/Multiclass/SolverBreak");
        }
    }
}
