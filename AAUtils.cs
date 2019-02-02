
	//ADD TO AAUTILS AT THE TOP
	public class GenericUtils
	{
		//COLORS
		public static Color COLOR_GLOWPULSE //a pulsing white glow
		{
			get
			{
				return new Color(255, 255, 255) * ((float)Main.mouseTextColor / 255f);			
			}
		}

        public static void MoveToPoint(Entity entity, Vector2 point, float moveSpeed)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - entity.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            entity.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            entity.velocity *= moveSpeed;
            entity.velocity *= velMultiplier;
        }
	}
