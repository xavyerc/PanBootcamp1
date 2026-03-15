using System.Numerics;
using Raylib_cs;

public class Fireball { //TODO @Diablo must refactor names.
  public int PositionX { get; set; }
  public int PositionY { get; set; }
  public int Radius { get; set; }
  protected Texture2D AttackSprite { get; set; }
  public Rectangle SpriteRect { get; set; }
  public Vector2 PositionVector { get; set; }

  protected static string[] sprites = [
    "assets/bone.png",
    "assets/fire.png",
    "assets/bubble.png"
  ];

  protected static Color BubbleColor = new(220, 0, 255, 105);

  public Fireball (int x, int y, AttackType attackType) {
    PositionX = x;
    PositionY = y;
    Radius = 20;
    AttackSprite = Raylib.LoadTexture(sprites[(int)attackType]);
    SpriteRect = new Rectangle(PositionX, PositionY, 64, 64);
    PositionVector = new Vector2(PositionX, PositionY);
    
  }

  public void Draw() {
    PositionVector = new Vector2(PositionX, PositionY);
    Raylib.DrawTextureRec(AttackSprite, SpriteRect, PositionVector, Color.White);
    PositionX += 10;
  }
}