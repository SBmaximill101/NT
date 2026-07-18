using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using NT.Buffs.FirstJutsu;
using Terraria.DataStructures;
using Terraria.Graphics.Renderers;

namespace NT.Common
{
	public class ChakraPlayer : ModPlayer
	{
		
		

		
		public int ChakraCurrent; 
		public const int DefaultChakraMax = 100; 
		public int ChakraMax; 
		public int ChakraMax2;
		public float ChakraRegenRate;
		internal int ChakraRegenTimer = 0; 
		public static readonly Color HealChakra = new(151, 165, 172);

		public bool CloneJutsu = false;

		public bool BodyReplacementActive = false;

		public bool TransformationJutsu = false;


		public override void Initialize()
		{
			ChakraMax = DefaultChakraMax;
		}


		public override void UpdateDead()
		{
			ResetVariables();
		}

		// We need this to ensure that regeneration rate and maximum amount are reset to default values after increasing when conditions are no longer satisfied (e.g. we unequip an accessory that increaces our recource)
		private void ResetVariables()
		{
			ChakraRegenRate = 40f;
			ChakraMax2 = ChakraMax;
		}

		public override void PostUpdateMiscEffects()
		{
			UpdateResource();
		}

		// Lets do all our logic for the custom resource here, such as limiting it, increasing it and so on.
		private void UpdateResource()
		{
			if(ChakraCurrent < 0)
            {
				ChakraCurrent = 0;

			}
			// For our resource lets make it regen slowly over time to keep it simple, let's use ChakraRegenTimer to count up to whatever value we want, then increase currentResource.
			ChakraRegenTimer++; // Increase it by 60 per second, or 1 per tick.

			// A simple timer that goes up to 3 seconds, increases the ChakraCurrent by 1 and then resets back to 0.
			if (ChakraRegenTimer > 180 / ChakraRegenRate)
			{
				ChakraCurrent += 1;
				ChakraRegenTimer = 0;
				if ( ChakraDirectionOn && StickRight || ChakraDirectionOn && StickLeft)
				{
					ChakraCurrent -= 2;
				} // chakradirect
                if (CloneJutsu)
                {
					ChakraCurrent -= 2;
					
                } //clone jutsu
				if (TransformationJutsu)
                {
					ChakraCurrent -= 2;
					
                } //Transformation jutsu
			}
			// Limit ChakraCurrent from going over the limit imposed by ChakraMax.
			ChakraCurrent = Utils.Clamp(ChakraCurrent, 0, ChakraMax2);
		}

		// These indicate what direction is what in the timer arrays used
		public const int DashDown = 0;
		public const int DashRight = 2;
		public const int DashLeft = 3;

		public const int DashCooldown = 40; // Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
		public const int DashDuration = 35; // Duration of the dash afterimage effect in frames

		// The initial velocity.  10 velocity is about 37.5 tiles/second or 50 mph
		public float DashVelocity = 0f;

		// The direction the player has double tapped.  Defaults to -1 for no dash double tap
		public int DashDir = -1;

		
		public bool ChakraDirectionOn;
		public int DashDelay = 0; // frames remaining till we can dash again
		public int DashTimer = 0; // frames remaining in the dash
		public bool StickLeft = false;
		public bool StickRight = false;
		public Vector2 DefaoultRotationOrigin = new Vector2(100,0);


		public IPlayerRenderer PlayerRenderer = new LegacyPlayerRenderer();
		public static bool DrawClone = true;
		
		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			
			if (CloneJutsu)
			{
				if (DrawClone)
				{
					DrawClone = false;
					PlayerRenderer.DrawPlayer(Main.Camera, Player, new Vector2(Player.position.X + 15, Player.position.Y), Player.fullRotation, Player.fullRotationOrigin);
					PlayerRenderer.DrawPlayer(Main.Camera, Player, new Vector2(Player.position.X - 15, Player.position.Y), Player.fullRotation, Player.fullRotationOrigin);
					DrawClone = true;
					
				}
			}

			if(TransformationJutsu)
            {
				(r, g, b, a) = (0, 0, 0, 0);
			}


			base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
		}

        public override void Load()
        {
			if(TransformationJutsu)
            {
				On_PlayerDrawLayers.DrawPlayer_RenderAllLayers += PlayerDrawLayers_DrawPlayer_RenderAllLayers;
			}
		}

        private void PlayerDrawLayers_DrawPlayer_RenderAllLayers(On_PlayerDrawLayers.orig_DrawPlayer_RenderAllLayers orig, ref PlayerDrawSet drawinfo)
		{
			
			drawinfo.DrawDataCache.Clear();
			ModContent.GetInstance<Items.Jutsu.First.TransformationLayer>().DrawWithTransformationAndChildren(ref drawinfo);
			orig(ref drawinfo);

		}

		public override void OnHurt(Player.HurtInfo info)
        {
			int BodyReplacmentGoreType = Mod.Find<ModGore>("BodyReplacementGore1").Type;
			var entitySource = Player.GetSource_None();
			if (BodyReplacementActive)
            {
				Gore.NewGore(entitySource, Player.position, Player.velocity, BodyReplacmentGoreType);
				Player.position = ToReturnForBodyReplacement;
				Player.ClearBuff(ModContent.BuffType<BodyReplacementBuff>());
			}
            base.OnHurt(info);
        }

        public override void ResetEffects() {
			ResetVariables();

			BodyReplacementActive = false;
			ChakraDirectionOn = false;


			


			/* ---- Transformation Jutsu ---- */

			if (TransformationJutsu)
			{
				//Player.AddBuff(10, 2);
			}

			/* ----  Chakra Direct ---- */


			//StickLeft = false;
			//StickRight = false;

			// ResetEffects is called not long after player.doubleTapCardinalTimer's values have been set
			// When a directional key is pressed and released, vanilla starts a 15 tick (1/4 second) timer during which a second press activates a dash
			// If the timers are set to 15, then this is the first press just processed by the vanilla logic.  Otherwise, it's a double-tap
			if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15 && StickLeft == false && StickRight == false) 
			{
				DashDir = DashDown;
			}
			else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15 ||
				StickLeft && Player.controlRight) 
			{
				DashDir = DashRight;
			}
			else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15 ||
				StickRight && Player.controlLeft) 
			{
				DashDir = DashLeft;
			}
			else {
				DashDir = -1;
			}


		}


		Vector2 ToReturnForBodyReplacement;
		

		// This is the perfect place to apply dash movement, it's after the vanilla movement code, and before the player's position is modified based on velocity.
		// If they double tapped this frame, they'll move fast this frame
		public override void PreUpdateMovement() {

			if(DefaoultRotationOrigin.X == 100)
            {
				DefaoultRotationOrigin = Player.fullRotationOrigin;
			}

            if (BodyReplacementActive == false)
            {
				ToReturnForBodyReplacement = Player.position;
			}

			if (ChakraDirectionOn)
			{
				// if the player can use our dash, has double tapped in a direction, and our dash isn't currently on cooldown
				if (ChakraDirectionOn && Player.dashType == 0 && !Player.setSolar && !Player.mount.Active && DashDir != -1 && DashDelay == 0)
				{
					Vector2 newVelocity = Player.velocity;

					switch (DashDir)
					{
						// Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
						case DashDown when Player.velocity.Y < DashVelocity:
							{

								newVelocity.Y = DashVelocity;
								break;
							}
						case DashLeft when Player.velocity.X > -DashVelocity:
						case DashRight when Player.velocity.X < DashVelocity:
							{
								// X-velocity is set here
								float dashDirection = DashDir == DashRight ? 1 : -1;
								newVelocity.X = dashDirection * DashVelocity;
								newVelocity.Y = DashVelocity * -.5f;
								break;
							}

					}

					// start our dash
					DashDelay = DashCooldown;
					DashTimer = DashDuration;
					Player.velocity = newVelocity;

					// Here you'd be able to set an effect that happens when the dash first activates
					// Some examples include:  the larger smoke effect from the Master Ninja Gear and Tabi
				}

				if (DashDelay > 0)
					DashDelay--;

				if (DashTimer > 0)
				{ // dash is active
				  // This is where we set the afterimage effect.  You can replace these two lines with whatever you want to happen during the dash
				  // Some examples include:  spawning dust where the player is, adding buffs, making the player immune, etc.
				  // Here we take advantage of "player.eocDash" and "player.armorEffectDrawShadowEOCShield" to get the Shield of Cthulhu's afterimage effect
					Player.eocDash = DashTimer;
					Player.armorEffectDrawShadowEOCShield = true;

					// count down frames remaining
					DashTimer--;
				}


				if (ChakraDirectionOn && ChakraCurrent >= 3)
				{
					if (Main.tile[((int)Player.position.X) / 16 - 1, ((int)Player.position.Y) / 16].TileType != TileID.Platforms &&
						Main.tile[((int)Player.position.X) / 16 - 1, ((int)Player.position.Y) / 16].HasTile)
					{
						StickLeft = true;
					}
					else { Player.fullRotationOrigin = DefaoultRotationOrigin; Player.fullRotation = 0; StickLeft = false; }


					if (Main.tile[((int)Player.position.X) / 16 + 2, ((int)Player.position.Y) / 16].TileType != TileID.Platforms &&
						Main.tile[((int)Player.position.X) / 16 + 2, ((int)Player.position.Y) / 16].HasTile)
					{
						StickRight = true;
					}
					else { Player.fullRotationOrigin = DefaoultRotationOrigin; Player.fullRotation = 0; StickRight = false; }
					Vector2 AntiGravVelocity;

					if (StickLeft)
					{
						AntiGravVelocity.X = Player.velocity.X;
						AntiGravVelocity.Y = 0f;
						Player.fullRotationOrigin = new Vector2(20, 25);
						Player.fullRotation = 1.5f;

						if (Player.controlUp)
						{
							AntiGravVelocity.Y = Player.accRunSpeed * -1.5f;
							Player.direction = -1;
						}
						if (Player.controlDown)
						{
							AntiGravVelocity.Y = Player.accRunSpeed * 1.5f;
							Player.direction = 1;
						}
						Player.velocity = AntiGravVelocity;
					}
					if (StickRight)
					{
						AntiGravVelocity.X = Player.velocity.X;
						AntiGravVelocity.Y = 0f;
						Player.fullRotationOrigin = new Vector2(0, 20);
						Player.fullRotation = -1.5f;

						if (Player.controlUp)
						{
							AntiGravVelocity.Y = Player.accRunSpeed * -1.5f;
							Player.direction = 1;
						}
						if (Player.controlDown)
						{
							AntiGravVelocity.Y = Player.accRunSpeed * 1.5f;
							Player.direction = -1;
						}
						Player.velocity = AntiGravVelocity;
					}



				}
				if (ChakraDirectionOn && ChakraCurrent <= 3)
				{
					Player.fullRotation = 0;
					StickLeft = false;
					StickRight = false;
					Player.ClearBuff(ModContent.BuffType<ChakraDirectBuff>());

				}
			}

		}

		

	}
}