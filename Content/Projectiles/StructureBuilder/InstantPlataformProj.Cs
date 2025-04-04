using FargowiltasSouls.Content.Tiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Content.Projectiles.StructureBuilder
{
    public class InstantPlataformProj : ModProjectile
    {
        public override string Texture => RemnantOfTheAncientsMod.PlaceHolderPath;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Instabridge");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 36;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Vector2 position = Projectile.Center;
            SoundEngine.PlaySound(SoundID.Item14, position);

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            float plataformType = Projectile.ai[0];
            float torchType = Projectile.ai[1];
            int range = (int)Projectile.ai[2];
            Player player = Main.player[Projectile.owner];



            // All the way across
            int direction = player.direction;
            bool goLeft = Projectile.Center.X < Main.player[Projectile.owner].Center.X;
            int min = goLeft ? -range : 0;
            int max = goLeft ? 0 : range;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                for (int x = (int)(Projectile.position.X / 16f); direction == 1 ? x <= Projectile.position.X / 16f + range : x >= Projectile.position.X / 16f - range; x += direction)
                {
                    place(x);
                }
            }
            else
            {

                for (int x = min; x < max; x++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(position.Y / 16.0f);

                    if (xPosition < 0 || xPosition >= Main.maxTilesX || yPosition < 0 || yPosition >= Main.maxTilesY)
                        continue;

                    Tile tile = Main.tile[xPosition, yPosition];

                    if (tile == null)
                    {
                        continue;
                    }

                    // Spawn platforms
                    WorldGen.PlaceTile(xPosition, yPosition, TileID.Platforms, mute: true, forced: false, -1, (int)plataformType);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendTileSquare(-1, xPosition, yPosition, 1);

                    if (x % 10 == 0)
                    {
                        WorldGen.PlaceTile(xPosition, yPosition - 1, TileID.Torches, mute: true, forced: false, -1, (int)torchType);
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendTileSquare(-1, xPosition, yPosition - 1, 1);
                    }
                }
            }
        

            
            void place(int x)
            {

                int xPosition = x;
                int yPosition = (int)(position.Y / 16.0f);

                if (xPosition < 0 || xPosition >= Main.maxTilesX || yPosition < 0 || yPosition >= Main.maxTilesY)
                    return;

                Tile tile = Main.tile[xPosition, yPosition];


                if (tile.HasTile || tile == null)
                    return;


                WorldGen.PlaceTile(xPosition, yPosition, TileID.Platforms, mute: true, forced: false, -1, (int)plataformType);
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendTileSquare(-1, xPosition, yPosition, 1);

                if (x % 10 == 0)
                {
                    WorldGen.PlaceTile(xPosition, yPosition - 1, TileID.Torches, mute: true, forced: false, -1, (int)torchType);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendTileSquare(-1, xPosition, yPosition - 1, 1);
                }
            }
        }
    }
}
