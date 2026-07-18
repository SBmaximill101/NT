using Terraria;
using Terraria.ModLoader;
using NT.ClassContent;
using NT.Common;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Renderers;


namespace NT.Buffs.FirstJutsu
{
    internal class TransformationBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;

		}


		public override void Update(Player player, ref int buffIndex)
		{

			if (player.GetModPlayer<ChakraPlayer>().ChakraCurrent >= 3)
			{
				player.GetModPlayer<ChakraPlayer>().TransformationJutsu = true;
				player.aggro -= 1000;
				player.buffTime[buffIndex] = 2;

			}
			else
			{
				player.GetModPlayer<ChakraPlayer>().TransformationJutsu = false;
			}
		}
	}
}
