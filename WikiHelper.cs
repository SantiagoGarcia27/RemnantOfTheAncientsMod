using CalamityMod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod
{
    public class WikiHelper
    {
        private static readonly string ExportTimestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        public static void ExportAllContentCSV()
        {


            // =========================
            // ITEMS
            // =========================
            string pathItems = PathGenerator("Items");
            using StreamWriter writerItems = new(pathItems);
            // Encabezados
            writerItems.WriteLine(
                "Category,Name,InternalName,TypeID,Damage,Defense,LifeMax,Rarity,Value"
            );
            foreach (ModItem modItem in ModContent.GetContent<ModItem>())
            {
                if (modItem.Mod != RemnantOfTheAncientsMod.RemnantOfTheAncients) continue;
                Item item = modItem.Item;

                writerItems.WriteLine(
                    $"Item," +
                    $"{Escape(item.Name)}," +
                    $"{Escape(modItem.Name)}," +
                    $"{item.type}," +
                    $"{item.damage}," +
                    $"," + // Defense
                    $"," + // LifeMax
                    $"{item.rare}," +
                    $"{item.value}"
                );
            }


            // =========================
            // NPCS
            // =========================
            string pathNPC = PathGenerator("Npc");
            using StreamWriter writerNpc = new(pathNPC);
            // Encabezados
            writerNpc.WriteLine(
                "Category,Name,InternalName,TypeID,Damage,Defense,LifeMax,Rarity,Value"
            );
            foreach (ModNPC modNpc in ModContent.GetContent<ModNPC>())
            {
                if (modNpc.Mod != RemnantOfTheAncientsMod.RemnantOfTheAncients) continue;
                NPC npc = modNpc.NPC;

                writerNpc.WriteLine(
                    $"NPC," +
                    $"{Escape(modNpc.DisplayName.Value)}," +
                    $"{Escape(modNpc.Name)}," +
                    $"{npc.type}," +
                    $"{npc.damage}," +
                    $"{npc.defense}," +
                    $"{npc.lifeMax}," +
                    $"," + // Rarity
                    $""     // Value
                );
            }

            // =========================
            // PROJECTILES
            // =========================
            string pathProjectile = PathGenerator("Projectiles");
            using StreamWriter writerProjectile = new(pathProjectile);
            foreach (ModProjectile modProj in ModContent.GetContent<ModProjectile>())
            {
                if (modProj.Mod != RemnantOfTheAncientsMod.RemnantOfTheAncients) continue;
                Projectile proj = modProj.Projectile;

                writerProjectile.WriteLine(
                    $"Projectile," +
                    $"{Escape(modProj.DisplayName.Value)}," +
                    $"{Escape(modProj.Name)}," +
                    $"{proj.type}," +
                    $"{proj.damage}," +
                    $"," +
                    $"," +
                    $"," +
                    $""
                );
            }

            // =========================
            // BUFFS
            // =========================
            string pathBuffs = PathGenerator("Buffs");
            using StreamWriter writerBuffs = new(pathBuffs);
            foreach (ModBuff modBuff in ModContent.GetContent<ModBuff>())
            {
                if (modBuff.Mod != RemnantOfTheAncientsMod.RemnantOfTheAncients) continue;
                writerBuffs.WriteLine(
                    $"Buff," +
                    $"{Escape(modBuff.DisplayName.Value)}," +
                    $"{Escape(modBuff.Name)}," +
                    $"{modBuff.Type}," +
                    $"," +
                    $"," +
                    $"," +
                    $"," +
                    $""
                );
            }

            // =========================
            // TILES
            // =========================
            string pathTiles = PathGenerator("Tiles");
            using StreamWriter writerTiles = new(pathTiles);
            foreach (ModTile modTile in ModContent.GetContent<ModTile>())
            {
                if (modTile.Mod != RemnantOfTheAncientsMod.RemnantOfTheAncients) continue;
                writerTiles.WriteLine(
                    $"Tile," +
                    $"{Escape(modTile.Name)}," +
                    $"{Escape(modTile.Name)}," +
                    $"{modTile.Type}," +
                    $"," +
                    $"," +
                    $"," +
                    $"," +
                    $""
                );
            }

            // =========================
            // WALLS
            // =========================
            string pathWalls = PathGenerator("Walls");
            using StreamWriter writerWalls = new(pathWalls);

            foreach (ModWall modWall in ModContent.GetContent<ModWall>())
            {
                if (modWall.Mod != RemnantOfTheAncientsMod.RemnantOfTheAncients) continue;
                writerWalls.WriteLine(
                    $"Wall," +
                    $"{Escape(modWall.Name)}," +
                    $"{Escape(modWall.Name)}," +
                    $"{modWall.Type}," +
                    $"," +
                    $"," +
                    $"," +
                    $"," +
                    $""
                );
            }

            // =========================
            // RECIPES
            // =========================
            string pathRecipes = PathGenerator("Recipes");
            using StreamWriter writerRecipes = new(pathRecipes);
            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe?.createItem?.ModItem?.Mod == RemnantOfTheAncientsMod.RemnantOfTheAncients)
                {
                    writerRecipes.WriteLine(
                        $"Recipe," +
                        $"{Escape(recipe.createItem.Name)}," +
                        $"{Escape(recipe.createItem.ModItem.Name)}," +
                        $"{recipe.createItem.type}," +
                        $"," +
                        $"," +
                        $"," +
                        $"," +
                        $""
                    );
                }
            }
        }
        private static string PathGenerator(string path)
        {
            string result = Path.Combine(
              Main.SavePath,
              "ROTA",
              $"RemantOfTheAncientsContentSheet-{path}-{ExportTimestamp}.csv"
            );

            return result;
        }

        private static string Escape(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            if (text.Contains(",") || text.Contains("\""))
            {
                text = text.Replace("\"", "\"\"");
                return $"\"{text}\"";
            }

            return text;
        }
    }
}
