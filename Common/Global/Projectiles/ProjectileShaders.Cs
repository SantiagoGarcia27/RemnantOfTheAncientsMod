using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Bangarang.Common.GlobalProjectiles;
public class SimpleTrailGlobalProjectile : GlobalProjectile
{
    public static Dictionary<int, SimpleTrailSettings> ProjectileTrailSettings { get; set; } = new();

    private static readonly VertexStrip vertexStrip = new();

    public override bool PreDraw(Projectile projectile, ref Color lightColor)
    {
        //if (ProjectileTrailSettings.TryGetValue(projectile.type, out SimpleTrailSettings settings))
        //{

   

        Main.pixelShader.CurrentTechnique.Passes[0].Apply();









        Main.instance.LoadProjectile(projectile.type);

            MiscShaderData miscShaderData = GameShaders.Misc["LightDisc"];
            miscShaderData.UseSaturation(-2.8f);
            miscShaderData.UseOpacity(2f);
            miscShaderData.Apply();

            

            //vertexStrip.PrepareStripWithProceduralPadding(projectile.oldPos, projectile.oldRot, VertexStrip.StripColorFunction.Combine, settings.StripHalfWidthFunction, (projectile.Size / 2f) - Main.screenPosition);
            vertexStrip.DrawTrail();

     

            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
      // }

        return base.PreDraw(projectile, ref lightColor);
    }
}

public class SimpleTrailSettings
{
    public float Saturation { get; set; } = -2.8f;
    public float Opacity { get; set; } = 2f;
    public VertexStrip.StripColorFunction StripColorFunction { get; set; }
    public VertexStrip.StripHalfWidthFunction StripHalfWidthFunction { get; set; }
}