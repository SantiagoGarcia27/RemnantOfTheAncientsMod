using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemnantOfTheAncientsMod.Common.Enums
{
    public class GlobalEnum
    {
        public enum packetType : byte
        {
            placePltaform
        };

        public enum gameStage : byte
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
    }
}
