using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using System;
using System.Linq;
using System.Collections.Generic;


namespace AAMod.Items.Dev.Invoker
{
	public class InvokerStaff : BanishDamageItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invoker Staff");
			Tooltip.SetDefault("");

            Item.staff[item.type] = true;

        }

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			String text = "";
			Player player = Main.player[Main.myPlayer];
			if(!player.GetModPlayer<InvokerPlayer>(mod).Thebookoflaw)
			{
				text += @"Strange. All is strange. 
Swinging a weapon causes summon damage?
Really?
Legendry Weapon.";
			}
			else
			{
				text += @"Left click to shoot a invoker bolt.
Right click to banish all of the enemies according to your basic banish damage.
Banishing enemies can heal you according to their maxlife.
But at what cost?
Legendry Weapon.";
			}
			foreach (TooltipLine tooltipLine in tooltips)
			{
				if (tooltipLine != null && tooltipLine.Name == "Damage")
				{
					string[] splitText = tooltipLine.text.Split(' ');
					string damageValue = splitText.First();
					string damageWord = splitText.Last();
					if(Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).Thebookoflaw) 
					{
						tooltipLine.text = damageValue + " banish " + damageWord;
					}
					else 
					{
						tooltipLine.text = damageValue + " summon " + damageWord;
					}
				}
				if (tooltipLine != null && tooltipLine.Name == "Tooltip0")
				{
					tooltipLine.text = text;
				}
			}
		}

        public override void SafeSetDefaults()
        {
			item.scale = 0.65f;
			item.width = 41;
			item.height = 41;
			item.rare = 11;
			item.damage = 200;
			item.noMelee = true;
			item.autoReuse = true;
			item.reuseDelay = 20;
			item.useStyle = 5;
			item.useTime = 16;
			item.useAnimation = 16;
			item.shoot = mod.ProjectileType("InvokerStaffproj"); 
			item.shootSpeed = 40f;
			item.value = Item.buyPrice(10, 36, 0, 0);
        }
		public override bool CanUseItem(Player player)
		{
			if(!player.GetModPlayer<InvokerPlayer>(mod).Thebookoflaw)
			{
				item.noMelee = false;
				Item.staff[item.type] = false;
				item.useStyle = 1;
				item.summon = true;
				item.damage = (int)(200 * player.minionDamage);
				return true;
			}
			if(player.GetModPlayer<InvokerPlayer>(mod).Thebookoflaw)
			{
				item.noMelee = true;
				Item.staff[item.type] = true;
				item.useStyle = 5;
				item.summon = false;
				return true;
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse != 2 && player.GetModPlayer<InvokerPlayer>(mod).Thebookoflaw)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("InvokerStaffproj"), (int)((double)damage), knockBack, player.whoAmI, 0f, 0f);
			}
			if (player.altFunctionUse == 2 && player.GetModPlayer<InvokerPlayer>(mod).Thebookoflaw)
			{
				if(!player.GetModPlayer<InvokerPlayer>(mod).InvokerMadness)
				{
					player.AddBuff(mod.BuffType("InvokerofMadness"), player.GetModPlayer<InvokerPlayer>(mod).Thebookoflaw? 30 : 300);
					player.GetModPlayer<InvokerPlayer>(mod).BanishDamage = item.damage * 5;
					player.GetModPlayer<InvokerPlayer>(mod).banishing = true;
				}
			}
			return false;
		}

        public override Vector2? HoldoutOrigin()
		{
			return new Vector2(38, 42);
		}

		public override bool AltFunctionUse(Player player)
		{
			return (!(!player.GetModPlayer<InvokerPlayer>(mod).DarkCaligula && player.GetModPlayer<InvokerPlayer>(mod).InvokedCaligula) && player.GetModPlayer<InvokerPlayer>(mod).Thebookoflaw);
		}

    }

	public abstract class BanishDamageItem : BaseAAItem
	{
		public virtual void SafeSetDefaults()
		{
		}
		public sealed override void SetDefaults()
		{
			SafeSetDefaults();
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
		}
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult) 
		{
			mult *= InvokerPlayer.ModPlayer(player).BanishDamageMult;
		}
		public override void GetWeaponKnockback(Player player, ref float knockback)
		{
			knockback = 0;
		}

		public override void GetWeaponCrit(Player player, ref int crit)
		{
			crit = 0;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				string[] splitText = tt.text.Split(' ');
				string damageValue = splitText.First();
				string damageWord = splitText.Last();
				tt.text = damageValue + " banish " + damageWord;
			}
		}
	}
    public class InvokerStaffproj : ModProjectile
	{
        public override void SetDefaults()
        {
           	projectile.width = 41;
           	projectile.height = 41;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.timeLeft = 6000;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.damage = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightBlue;
        }
        
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, (Main.DiscoR -projectile.alpha) * 0.8f / 255f, (Main.DiscoG -projectile.alpha) * 0.4f / 255f, (Main.DiscoB -projectile.alpha) * 0f / 255f);
            Player projOwner = Main.player[projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
           	projectile.direction = projOwner.direction;
           	projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(225f);

            if (projectile.spriteDirection == -1)
            {
               projectile.rotation -= MathHelper.ToRadians(90f);
            }

			if (projectile.ai[0] == 0f)
			{
				float[] ai = projectile.ai;
				int num2 = 1;
				float num3 = ai[num2];
				ai[num2] = num3 + 1f;
				if (projectile.ai[1] >= 45f)
				{
					projectile.ai[1] = 45f;
					if (projectile.velocity.X < 0f)
					{
						projectile.spriteDirection = -1;
						projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(225f);
					}
					else
					{
						projectile.spriteDirection = 1;
						projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
					}
				}
			}
			if (projectile.ai[0] == 1f)
			{
				projectile.tileCollide = false;
				int num6 = 15;
				bool flag = false;
				bool flag2 = false;
				float[] localAI = projectile.localAI;
				int num7 = 0;
				float num8 = localAI[num7];
				localAI[num7] = num8 + 1f;
				if (projectile.localAI[0] % 30f == 0f)
				{
					flag2 = true;
				}
				int num9 = (int)projectile.ai[1];
				if (projectile.localAI[0] >= (float)(60 * num6))
				{
					flag = true;
				}
				else if (num9 < 0 || num9 >= 200)
				{
					flag = true;
				}
				else if (Main.npc[num9].active && !Main.npc[num9].dontTakeDamage)
				{
					projectile.Center = Main.npc[num9].Center - projectile.velocity * 2f;
					projectile.gfxOffY = Main.npc[num9].gfxOffY;
					projectile.alpha = Main.npc[num9].alpha;
					if (flag2)
					{
						Main.npc[num9].HitEffect(0, 1.0);
					}
					if(Main.npc[num9].GetGlobalNPC<InvokedGlobalNPC>(mod).IsBeingBanished)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					projectile.Kill();
				}
			}
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Rectangle rectangle = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);

			double Realdamage = Main.CalculateDamage((int)projectile.damage, 0);
			if(target.life <= Realdamage) target.life -= target.life - 1;
			else target.life -= (int)Realdamage;

			if(target.realLife >= 0)
			{
				if(Main.npc[target.realLife].life <= Realdamage) Main.npc[target.realLife].life -= Main.npc[target.realLife].life - 1;
				else Main.npc[target.realLife].life -= (int)Realdamage;
			}

			Main.player[Main.myPlayer].dpsDamage += (int)Realdamage;

			damage = 1;

			Color damagecolor = crit ? CombatText.DamagedHostileCrit : CombatText.DamagedHostile;
			CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), damagecolor, (int)Realdamage, false, false);


			if (projectile.owner == Main.myPlayer)
			{
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && ((projectile.friendly && (!Main.npc[i].friendly || projectile.type == 318 || (Main.npc[i].type == 22 && projectile.owner < 255 && Main.player[projectile.owner].killGuide) || (Main.npc[i].type == 54 && projectile.owner < 255 && Main.player[projectile.owner].killClothier))) || (projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (projectile.owner < 0 || Main.npc[i].immune[projectile.owner] == 0 || projectile.maxPenetrate == 1) && (Main.npc[i].noTileCollide || !projectile.ownerHitCheck || projectile.CanHit(Main.npc[i])))
					{
						bool flag;
						if (Main.npc[i].type == 414)
						{
							Rectangle rect = Main.npc[i].getRect();
							int num = 8;
							rect.X -= num;
							rect.Y -= num;
							rect.Width += num * 2;
							rect.Height += num * 2;
							flag = projectile.Colliding(rectangle, rect);
						}
						else
						{
							flag = projectile.Colliding(rectangle, Main.npc[i].getRect());
						}
						if (flag)
						{
							if (Main.npc[i].reflectingProjectiles && projectile.CanReflect())
							{
								Main.npc[i].ReflectProjectile(projectile.whoAmI);
								return;
							}
							projectile.ai[0] = 1f;
							projectile.ai[1] = (float)i;
							projectile.velocity = (Main.npc[i].Center - projectile.Center) * 0.75f;
							projectile.netUpdate = true;
							projectile.StatusNPC(i);
							projectile.damage = 0;
							Point[] array = new Point[10];
							int num2 = 0;
							for (int j = 0; j < 1000; j++)
							{
								if (j != projectile.whoAmI && Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && Main.projectile[j].type == projectile.type && Main.projectile[j].ai[0] == 1f && Main.projectile[j].ai[1] == (float)i)
								{
									array[num2++] = new Point(j, Main.projectile[j].timeLeft);
									if (num2 >= array.Length)
									{
										break;
									}
								}
							}
							if (num2 >= array.Length)
							{
								int num3 = 0;
								for (int k = 1; k < array.Length; k++)
								{
									if (array[k].Y < array[num3].Y)
									{
										num3 = k;
									}
								}
								Main.projectile[array[num3].X].Kill();
							}
						}
					}
				}
			}
		}
		
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return null;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType("Invokedproj"), 3600);
		}
    }

	public class Invokedproj : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Be Banished");
			Description.SetDefault("You are marked by Invoked Magic");
			Main.debuff[Type] = false;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<InvokedGlobalNPC>(mod).Banished = true;

			InvokerPlayer InvokerPlayer = Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod);
			if((InvokerPlayer.banishing && npc.active && (InvokerPlayer.BanishDamage * InvokerPlayer.BanishDamageMult * InvokerPlayer.BanishLimit > npc.life)) || npc.GetGlobalNPC<InvokedGlobalNPC>(mod).IsBeingBanished)
			{
				npc.GetGlobalNPC<InvokedGlobalNPC>(mod).IsBeingBanished = true;
			}
        }
	}

	public class InvokedGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public bool Banished;
		public bool IsBeingBanished = false;
		public int BanishCount = 0;
		public bool CaligulaSoulFight = false;

		public override void ResetEffects(NPC npc)
		{
			this.Banished = false;
		}

		public void BanishAction(NPC npc)
		{
			npc.velocity.X = 0;
			npc.velocity.Y = 0;
			npc.scale -= 0.01f;
			npc.alpha += 4;

			if(BanishCount > 70 || npc.alpha >= 250 || npc.scale < 0.05f)
			{
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("InvokedHeal"), 0, 0f, Main.player[Main.myPlayer].whoAmI, Main.player[Main.myPlayer].whoAmI, npc.lifeMax * 0.01f);
				
				if(npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordHand)
				{
					for(int i = 0; i < 200 ; i++)
					{
						if(Main.npc[i].type == NPCID.MoonLordCore || Main.npc[i].type == NPCID.MoonLordHead || Main.npc[i].type == NPCID.MoonLordHand)
						{
							Main.npc[i].active = false;
							Main.npc[i].NPCLoot();
						}
					}
				}

				
				if(npc.realLife >= 0) 
				{
					Main.npc[npc.realLife].NPCLoot();//This need change in AAMod
					for(int i = 0; i < 200 ; i++)
					{
						if(Main.npc[i].realLife == npc.realLife)
						{
							Main.npc[i].NPCLoot();
							Main.npc[i].active = false;
						}
					}
					NPCLoader.CheckDead(Main.npc[npc.realLife]);
					Main.npc[npc.realLife].checkDead();
					Main.npc[npc.realLife].netUpdate = true;
				}
				npc.NPCLoot();//This need change in AAMod
				NPCLoader.CheckDead(npc);
				npc.checkDead();
				npc.active = false;
				npc.life = 0;
				npc.netUpdate = true;
				BanishCount = 0;
			}
		}
		public override void UpdateLifeRegen(NPC npc, ref int damage) 
		{
			int InvokedCount = 0;
			for (int i = 0; i < 1000; i++) 
			{
				Projectile p = Main.projectile[i];
				int num9 = (int)p.ai[1];
				if (p.active && p.type == mod.ProjectileType<InvokerStaffproj>() && p.ai[0] == 1f && npc == Main.npc[num9]) 
				{
					InvokedCount++;
					npc.lifeRegen -= 10;
				}
			}

			InvokerPlayer InvokerPlayer = Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod);

			if(npc.boss)
			{
				CaligulaSoulFight = true;

				if(InvokerPlayer.CaligulaSoul.Contains(npc.type))
				{
					CaligulaSoulFight = false;
				}
				
				bool flag = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == mod.ItemType("InvokerStaff") && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).Thebookoflaw && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).Thebookoflaw;
				bool flag2 = npc.life < 50000 && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).InvokedCaligula;
				
				if(!flag || !flag2)
				{
					CaligulaSoulFight = false;
				}
			}


			if(!npc.townNPC && (npc.life < InvokerPlayer.BanishDamage * InvokerPlayer.BanishDamageMult * InvokedCount) && InvokerPlayer.banishing && (npc.active || npc.life > 0))
			{
				npc.GetGlobalNPC<InvokedGlobalNPC>(mod).IsBeingBanished = true;
			}
			if((IsBeingBanished && !npc.townNPC && (npc.active || npc.life > 0)) || (!npc.townNPC && (npc.life < InvokerPlayer.BanishDamage) && InvokerPlayer.banishing && (npc.active || npc.life > 0)))
			{
				IsBeingBanished = true;
				BanishCount ++;
				if(BanishCount == 1)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("InvokedRune"), 0, 0f, Main.player[Main.myPlayer].whoAmI, 1f, npc.whoAmI);
					
					if(npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordHand)
					{
						for(int i = 0; i < 200 ; i++)
						{
							if(Main.npc[i].type == NPCID.MoonLordCore || Main.npc[i].type == NPCID.MoonLordHead || Main.npc[i].type == NPCID.MoonLordHand)
							{
								Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("InvokedRune"), 0, 0f, Main.player[Main.myPlayer].whoAmI, 1f, npc.whoAmI);
							}
						}
					}
				}
				BanishAction(npc);
			}
			return;
		}
		
		public override bool PreNPCLoot(NPC npc)
		{
			if(Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == mod.ItemType("InvokerStaff") && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).Thebookoflaw && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).Thebookoflaw)
			{
            	//Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).BanishProjClear = true; // Just for test.
				float nump7 = 4f;
				float nump8 = (float)Main.rand.Next(-100, 101);
				float nump9 = (float)Main.rand.Next(-100, 101);
				float nump10 = (float)Math.Sqrt((double)(nump8 * nump8 + nump9 * nump9));
				nump10 = nump7 / nump10;
				nump8 *= nump10;
				nump9 *= nump10;
				int[] array = new int[200];
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].CanBeChasedBy(this, false))
					{
						float num5 = Math.Abs(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - npc.position.X + (float)(npc.width / 2)) + Math.Abs(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2) - npc.position.Y + (float)(npc.height / 2));
						if (num5 < 800f)
						{
							if (Collision.CanHit(npc.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
							{
								array[num4] = i;
								num4++;
							}
							else if (num4 == 0)
							{
								array[num3] = i;
								num3++;
							}
						}
					}
				}
				if (num3 == 0 && num4 == 0)
				{
					return true;
				}
				int num6;
				if (num4 > 0)
				{
					num6 = array[Main.rand.Next(num4)];
				}
				else
				{
					num6 = array[Main.rand.Next(num3)];
				}
				if(npc.lifeMax >= 1000) Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("InvokedHeal"), 0, 0f, Main.player[Main.myPlayer].whoAmI, Main.player[Main.myPlayer].whoAmI, (npc.life > npc.lifeMax? npc.life : npc.lifeMax) * 0.001f);
				if(npc.damage != 0) 
				{
					if((npc.realLife >= 0 && npc.realLife == npc.whoAmI) || npc.realLife < 0) Projectile.NewProjectile(npc.Center.X, npc.Center.Y, nump8, nump9, mod.ProjectileType("InvokedDamage"), npc.damage * 20, 0f, Main.player[Main.myPlayer].whoAmI, (float)num6, 0f);
				}
				if(npc.GetGlobalNPC<InvokedGlobalNPC>(mod).CaligulaSoulFight && !Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).DarkCaligula && Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).InvokedCaligula && (npc.type == mod.NPCType("ZeroProtocol") || npc.type == mod.NPCType("YamataA") || npc.type == mod.NPCType("AkumaA") || npc.type == mod.NPCType("ShenA") || npc.type == mod.NPCType("SupremeRajah")))
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, nump8, nump9, mod.ProjectileType("InvokedDamage"), 0, 0f, Main.player[Main.myPlayer].whoAmI, Main.player[Main.myPlayer].whoAmI, npc.type);
				}
			}
			return true;
		}
		
	}
	
	public class InvokedProjClear : GlobalProjectile
	{
		public override bool Autoload(ref string name) => true;
		public override bool PreAI(Projectile projectile)
		{
			if (Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>(mod).BanishProjClear)
			{
				for (int q = 0; q < 1000; q++)
				{
					Projectile p = Main.projectile[q];
					bool KILL = false;
					if (p.active && p.type != mod.ProjectileType<InvokedHeal>() && p.type != mod.ProjectileType<InvokedDamage>() && p.type != mod.ProjectileType<InvokerStaffproj>() && p.type != mod.ProjectileType<InvokedRune>())
					{
						//if(Main.netMode != 0 && p.owner != Main.clientPlayer.whoAmI) KILL = true;
						KILL = true;
					}
					if (KILL) 
					{
						if(p.damage != 0 && !p.friendly) 
						{
							Projectile.NewProjectile(p.Center.X, p.Center.Y, 0, 0, mod.ProjectileType("InvokedHeal"), 0, 0f, Main.player[Main.myPlayer].whoAmI, Main.player[Main.myPlayer].whoAmI, (int)(p.damage * 0.1));
							/* 
							float reflect1 = 0f;
							float reflect2 = 0f;
							if(p.ai[0] == Main.myPlayer) reflect1 = p.owner;
							if(p.ai[1] == Main.myPlayer) reflect2 = p.owner;
							if(p.ai[0] != Main.myPlayer && p.ai[1] != Main.myPlayer) {reflect1 = p.ai[0]; reflect2 = p.ai[1];}
							int R = Projectile.NewProjectile(Main.player[Main.myPlayer].Center.X, Main.player[Main.myPlayer].Center.Y, -p.velocity.X, -p.velocity.Y, p.type, p.damage * 10000, p.knockBack, Main.myPlayer, reflect1, reflect2);
							Main.projectile[R].friendly = true;
							Main.projectile[R].playerImmune[R] = 60;
							*/
						}
						p.active = false;
						p.Kill();
					}
				}
			}
			return base.PreAI(projectile);
		}
	}
	

	public class InvokedDamage : ModProjectile
	{
		public override void SetDefaults()
        {
           	projectile.width = 6;
			projectile.height = 6;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.extraUpdates = 3;
        }

		public override Color? GetAlpha(Color lightColor)
        {
            return Color.DarkBlue;
        }
		private int time = 0;
		public override void AI()
        {
			time += 1;
			int num0;
			if (time >= 60)
			{
				if(projectile.ai[1] == 0f)
				{
					projectile.friendly = true;
					int num568 = (int)projectile.ai[0];
					if (!Main.npc[num568].active)
					{
						int[] array2 = new int[200];
						int num569 = 0;
						for (int num570 = 0; num570 < 200; num570 = num0 + 1)
						{
							if (Main.npc[num570].CanBeChasedBy(this, true))
							{
								float num571 = Math.Abs(Main.npc[num570].position.X + (float)(Main.npc[num570].width / 2) - projectile.position.X + (float)(projectile.width / 2)) + Math.Abs(Main.npc[num570].position.Y + (float)(Main.npc[num570].height / 2) - projectile.position.Y + (float)(projectile.height / 2));
								if (num571 < 800f)
								{
									array2[num569] = num570;
									num0 = num569;
									num569 = num0 + 1;
								}
							}
							num0 = num570;
						}
						if (num569 == 0)
						{
							projectile.Kill();
							return;
						}
						num568 = array2[Main.rand.Next(num569)];
						projectile.ai[0] = (float)num568;
					}
					float num572 = 4f;
					Vector2 vector44 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num573 = Main.npc[num568].Center.X - vector44.X;
					float num574 = Main.npc[num568].Center.Y - vector44.Y;
					float num575 = (float)Math.Sqrt((double)(num573 * num573 + num574 * num574));
					num575 = num572 / num575;
					num573 *= num575;
					num574 *= num575;
					int num576 = 30;
					projectile.velocity.X = (projectile.velocity.X * (float)(num576 - 1) + num573) / (float)num576;
					projectile.velocity.Y = (projectile.velocity.Y * (float)(num576 - 1) + num574) / (float)num576;
				}
				else
				{

					int num492 = (int)projectile.ai[0];
					float num493 = 4f;
					Vector2 vector39 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num494 = Main.player[num492].Center.X - vector39.X;
					float num495 = Main.player[num492].Center.Y - vector39.Y;
					float num496 = (float)Math.Sqrt((double)(num494 * num494 + num495 * num495));
					if (num496 < 50f && projectile.position.X < Main.player[num492].position.X + (float)Main.player[num492].width && projectile.position.X + (float)projectile.width > Main.player[num492].position.X && projectile.position.Y < Main.player[num492].position.Y + (float)Main.player[num492].height && projectile.position.Y + (float)projectile.height > Main.player[num492].position.Y)
					{
						if (projectile.owner == Main.myPlayer)
						{
							Player player = Main.player[num492];
							player.GetModPlayer<InvokerPlayer>(mod).CaligulaSoul.Add((int)projectile.ai[1]) ;
							CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.DarkGray, "Powerful Soul Steal!", false, false);
						}
						projectile.Kill();
					}
					num496 = num493 / num496;
					num494 *= num496;
					num495 *= num496;
					projectile.velocity.X = (projectile.velocity.X * 15f + num494) / 16f;
					projectile.velocity.Y = (projectile.velocity.Y * 15f + num495) / 16f;
				}
			}
			for (int num577 = 0; num577 < 5; num577 = num0 + 1)
			{
				float num578 = projectile.velocity.X * 0.2f * (float)num577;
				float num579 = -(projectile.velocity.Y * 0.2f) * (float)num577;
				int num580 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 175, 0f, 0f, 20, Color.DarkBlue, 2f);
				Main.dust[num580].noGravity = true;
				Dust dust3 = Main.dust[num580];
				dust3.velocity *= 0f;
				Dust dust74 = Main.dust[num580];
				dust74.position.X = dust74.position.X - num578;
				Dust dust75 = Main.dust[num580];
				dust75.position.Y = dust75.position.Y - num579;
				num0 = num577;
			}
			return;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			double Realdamage = Main.CalculateDamage((int)projectile.damage, 0);
			if(target.life <= Realdamage) target.life -= target.life - 1;
			else target.life -= (int)Realdamage;

			if(target.realLife >= 0)
			{
				if(Main.npc[target.realLife].life <= Realdamage) Main.npc[target.realLife].life -= Main.npc[target.realLife].life - 1;
				else Main.npc[target.realLife].life -= (int)Realdamage;
			}

			Main.player[Main.myPlayer].dpsDamage += (int)Realdamage;

			damage = 1;

			Color damagecolor = crit ? CombatText.DamagedHostileCrit : CombatText.DamagedHostile;
			CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), damagecolor, (int)Realdamage, false, false);
		}
	}
	public class InvokedHeal : ModProjectile
	{
		public override void SetDefaults()
        {
           	projectile.width = 6;
			projectile.height = 6;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.extraUpdates = 3;
        }
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.DarkRed;
        }
		public override void AI()
        {
			int num492 = (int)projectile.ai[0];
			float num493 = 4f;
			Vector2 vector39 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
			float num494 = Main.player[num492].Center.X - vector39.X;
			float num495 = Main.player[num492].Center.Y - vector39.Y;
			float num496 = (float)Math.Sqrt((double)(num494 * num494 + num495 * num495));
			if (num496 < 50f && projectile.position.X < Main.player[num492].position.X + (float)Main.player[num492].width && projectile.position.X + (float)projectile.width > Main.player[num492].position.X && projectile.position.Y < Main.player[num492].position.Y + (float)Main.player[num492].height && projectile.position.Y + (float)projectile.height > Main.player[num492].position.Y)
			{
				if (projectile.owner == Main.myPlayer)
				{
					int num497 = (int)projectile.ai[1];
					Main.player[num492].HealEffect(num497, true);
					Player player = Main.player[num492];
					player.statLife += num497;
					if (Main.player[num492].statLife > Main.player[num492].statLifeMax2)
					{
						Main.player[num492].statLife = Main.player[num492].statLifeMax2;
					}
					NetMessage.SendData(66, -1, -1, null, num492, (float)num497, 0f, 0f, 0, 0, 0);
				}
				projectile.Kill();
			}
			num496 = num493 / num496;
			num494 *= num496;
			num495 *= num496;
			projectile.velocity.X = (projectile.velocity.X * 15f + num494) / 16f;
			projectile.velocity.Y = (projectile.velocity.Y * 15f + num495) / 16f;
			int num3;
			for (int num502 = 0; num502 < 5; num502 = num3 + 1)
			{
				float num503 = projectile.velocity.X * 0.2f * (float)num502;
				float num504 = -(projectile.velocity.Y * 0.2f) * (float)num502;
				int num505 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 175, 0f, 0f, 20, Color.OrangeRed, 1.3f);
				Main.dust[num505].noGravity = true;
				Dust dust3 = Main.dust[num505];
				dust3.velocity *= 0f;
				Dust dust72 = Main.dust[num505];
				dust72.position.X = dust72.position.X - num503;
				Dust dust73 = Main.dust[num505];
				dust73.position.Y = dust73.position.Y - num504;
				num3 = num502;
			}
			return;
		}
	}
	public class InvokedRune : ModProjectile
	{
		public override void SetDefaults()
        {
			projectile.width = 86;
			projectile.height = 86;
			projectile.hostile = false;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 200;
			projectile.damage = 0;
        }

		private int count = 0;

		public override void AI()
        {
			Lighting.AddLight(projectile.Center, (Main.DiscoR -projectile.alpha) * 0.8f / 255f, (Main.DiscoG -projectile.alpha) * 0.4f / 255f, (Main.DiscoB -projectile.alpha) * 0f / 255f);
			
			if(count < 13)
			{
				projectile.alpha -= 20;
			}
			else if(count >= 13)
			{
				projectile.alpha += 3;
				if(projectile.alpha >= 250)
				{
					projectile.Kill();
				}
			}
			
			count ++;

			int numa = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 175, 0f, 0f, 30, Color.OrangeRed, 1.3f);
			Main.dust[numa].noGravity = true;
			Main.dust[numa].alpha ++;
			
			projectile.damage = 0;
			projectile.netUpdate = true;

			if(projectile.ai[0] == 1f)
			{
				int num9 = (int)projectile.ai[1];
				if(Main.npc[num9].active)
				{
					projectile.velocity = (Main.npc[num9].Center - projectile.Center) * 0.75f;
					projectile.StatusNPC(num9);
					projectile.Center = Main.npc[num9].Center - projectile.velocity * 2f;
					projectile.gfxOffY = Main.npc[num9].gfxOffY;
				}
				else if (num9 < 0 || num9 >= 200)
				{
					projectile.Kill();
				}
				else
				{
					projectile.Kill();
				}
				
				if(!Main.npc[num9].active && Main.npc[num9].life <= 0)
				{
					projectile.Kill();
				}
			}
			else
			{
				int num9 = (int)projectile.ai[1];
				if(Main.player[num9].active)
				{
					projectile.velocity = (Main.player[num9].Center - projectile.Center) * 0.75f;
					projectile.Center = Main.player[num9].Center - projectile.velocity * 2f;
					projectile.gfxOffY = Main.player[num9].gfxOffY;
				}
				else if (num9 < 0 || num9 >= 200)
				{
					projectile.Kill();
				}
				else
				{
					projectile.Kill();
				}
				
				if(!Main.player[num9].active)
				{
					projectile.Kill();
				}
			}
			
			
		}
	}
}