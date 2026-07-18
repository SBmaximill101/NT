using Terraria;
using Terraria.ModLoader;
using NT.ClassContent;
using NT.Common;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Renderers;

namespace NT.Buffs.FirstJutsu
{
    internal class CloneBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
			
		}


		public override void Update(Player player, ref int buffIndex)
		{
			ChackraDamageClass.ChakraCrit = 10;

			if (player.GetModPlayer<ChakraPlayer>().ChakraCurrent >= 3)
			{
				player.GetModPlayer<ChakraPlayer>().CloneJutsu = true;
				
				player.buffTime[buffIndex] = 2;

			}
			else {
				player.GetModPlayer<ChakraPlayer>().CloneJutsu = false;
			}
		}


	


	}
}
