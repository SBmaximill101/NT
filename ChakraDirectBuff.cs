using Terraria;
using Terraria.ModLoader;
using NT.Common;

namespace NT.Buffs.FirstJutsu
{
    internal class ChakraDirectBuff : ModBuff
    {


		public const int DashCooldown = 50; // Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
		public const int DashDuration = 35; // Duration of the dash afterimage effect in frames

		// The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
		public const float DashVelocity = 10f;

		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ChakraPlayer>().DashVelocity = 10f;
			player.GetModPlayer<ChakraPlayer>().ChakraDirectionOn = true;
		}
	}
}
