using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemnantOfTheAncientsMod.Common.Enums
{
    public class GlobalEnum
    {
        public enum PacketType : byte
        {
            placePltaform
        };

        public enum GameStage : byte
        {
            None,
            PreBoss,
            Hardmode,
            PreHardmode,
            PostMech,
            PostKingSlime,
            PostEyeOfCthulhu,
            PostCorruptBoss,
            PostQueenBee,
            PostSkeletron,
            PostDeerclops,
            PostDeset,
            PostWallOfFlesh,
            PostFrozen,
            PostQueenSlime,
            PostDestoryer,
            PostTwins,
            PostSkeletronPrime,     
            PostPlantera,
            PostGolem,
            PostEmpressOfLight,
            PostDukeFishron,
            PostCultist,
            PostMoonLord
        };

        public enum GemType : byte
        {
            none,
            Amethyst,
            Topaz,
            Emerald,
            Sapphire,
            Ruby,
            Amber,
            Diamond
        };
    }
}
