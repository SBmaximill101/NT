using Terraria;
using Terraria.ModLoader;
using NT.Common;

namespace NT.Buffs.FirstJutsu
{
    internal class BodyReplacementBuff : ModBuff
    {


		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			
			player.statDefense += 70;
			player.GetModPlayer<ChakraPlayer>().BodyReplacementActive = true;
			

		}
	}
}
