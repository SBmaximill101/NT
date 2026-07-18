using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace NT.Dusts
{
    internal class ChakraDirectDust : ModDust
    {
		/*
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 1 * 30, 30, 30);
			
		}

		
		public int curframe = 0;
		public int Worldframe = 0;
		
		public override bool Update(Dust dust)
		{
			Worldframe++;
			if (curframe < 3 && Worldframe > 20)
			{
				curframe++;
				Worldframe = 0;
				dust.frame = new Rectangle(0, curframe * 30, 30, 30);
			}
            if(curframe > 3)
			{
				Worldframe = 0;
				curframe = 0;
				dust.active = false;
			}


			return false;
		}
		DEPRICATED
		*/
	}
}
