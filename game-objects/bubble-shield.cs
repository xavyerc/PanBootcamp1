using System.Numerics;
using Raylib_cs;

public class BubbleShield : Fireball
{
    public BubbleShield(int x, int y, AttackType attackType) : base(x, y, attackType)
    {
      Console.WriteLine("Bubble constructor");
      PositionX = x;
      PositionY = y;
      Radius = 20;
      // AttackSprite = Raylib.LoadTexture(sprites[(int)attackType]);
      // SpriteRect = new Rectangle(PositionX, PositionY, 92, 90);
      // PositionVector = new Vector2(PositionX, PositionY);
      Console.WriteLine("Bubble constructor finish");
    }

  public new void Draw() {
    Raylib.DrawCircle(PositionX, PositionY, 38, BubbleColor);
    // Raylib.DrawTextureRec(AttackSprite, SpriteRect, PositionVector, Color.White);

  }
}