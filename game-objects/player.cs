using Raylib_cs;
public class Player : Character {
  // Atributos
  public int BoubbleShieldInv { get; set; }
  public int BoubbleShieldMax { get; set; }
  public int BoubbleShieldCD { get; set; }
  public bool IsBubbleShieldActive { get; set; }
  
  public BubbleShield Shield { get; set; }

  public Player(
    int health,
    int maxHealth,
    int mana,
    int maxMana,
    int meeleAttack,
    int magicAttack,
    int manaRecovery,
    int manaCost,
    int boubbleShieldInv,
    int boubbleShieldMax,
    int boubbleShieldCD,
    bool isBubbleShieldActive
  ) {
    Health = health;
    MaxHealth = maxHealth;
    Mana = mana;
    MaxMana = maxMana;
    MeeleAttack = meeleAttack;
    MagicAttack = magicAttack;
    ManaRecovery = manaRecovery;
    ManaCost = manaCost;
    BoubbleShieldInv = boubbleShieldInv;
    BoubbleShieldMax = boubbleShieldMax;
    BoubbleShieldCD = boubbleShieldCD;
    IsBubbleShieldActive = isBubbleShieldActive;
    PositionX = 550;
    PositionY = 350;
    FightPositionX = 30;
    FightPositionY = 236;
    Fireballs = [];
    Shield = new BubbleShield(FightPositionX + 35, FightPositionY + 35, AttackType.BUBBLE_SHIELD);
    characterType = CharacterType.PLAYER;
  }

  //Methods
  public override int Attack(AttackType selection) {
    IsBubbleShieldActive = false;
    return base.Attack(selection);
  } 

  public void TakeDamage (int attack) {
    if (!IsBubbleShieldActive){
      Health -= attack;
    }
  }

    public void UseBoubbleShield () {
    if (BoubbleShieldInv >= 1) {
      IsBubbleShieldActive = true;
      BoubbleShieldInv -= 1;
    }
    RecoverMana();
  }

  public void RegenerateBS (int currentTurn) {
    if ((currentTurn % BoubbleShieldCD == 0) && (BoubbleShieldInv < BoubbleShieldMax) && (currentTurn != 0)) {
      BoubbleShieldInv += 1;
    }
  }

  public void Move ()
  {
    if (Raylib.IsKeyDown(KeyboardKey.W) && PositionY > 0) {
      PositionY -= 3;
    }
    if (Raylib.IsKeyDown(KeyboardKey.A) && PositionX > 0) {
      PositionX -= 3;
    }
    if (Raylib.IsKeyDown(KeyboardKey.S) && PositionY < 1200 - 64) {
      PositionY += 3;
    }
    if (Raylib.IsKeyDown(KeyboardKey.D) && PositionX < 1800 -64) {
      PositionX += 3;
    }
  }

  public override void ResetStats()
  {
    base.ResetStats();
    BoubbleShieldInv = 2;
  }

  public override void Draw(Texture2D sprite, int gameState) {
    Move();
    base.Draw(sprite, gameState);
    if (IsBubbleShieldActive && Shield != null) {
      Shield.Draw();
    }
  }
}

