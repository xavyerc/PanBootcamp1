using Raylib_cs;
public class Player {
  // Atributos
  public int Health { get; set; }
  public int Mana { get; set; }
  public int MaxMana { get; set; }
  public int MeeleAttack { get; set; }
  public int MagicAttack { get; set; }
  public int ManaRecovery { get; set; }
  public int ManaCost { get; set; }
  public int BoubbleShieldInv { get; set; }
  public int BoubbleShieldMax { get; set; }
  public int BoubbleShieldCD { get; set; }
  public bool IsBubbleShieldActive { get; set; }
  public int PositionX { get; set; }
  public int PositionY { get; set; }
  public List<Fireball> Fireballs { get; set; }
  public BubbleShield Shield { get; set; }

  public Player(
    int health,
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
    PositionX = 30;
    PositionY = 236;
    Fireballs = [];
    Shield = new BubbleShield(PositionX + 35, PositionY + 35, AttackType.BUBBLE_SHIELD);
  }

  //Methods
  public int Attack (AttackType selection) {
    Fireball fb;
    IsBubbleShieldActive = false;
    if (selection == AttackType.MEELE_ATTACK) {
      RecoverMana();
      fb = new Fireball(PositionX + 64 + 40, PositionY + 25, AttackType.MEELE_ATTACK);
      Fireballs.Add(fb);
      return MeeleAttack;
    } else if (selection == AttackType.MAGIC_ATTACK && (Mana >= ManaCost)) {
      Mana -= ManaCost;
      fb = new Fireball(PositionX + 64 + 40, PositionY + 25, AttackType.MAGIC_ATTACK);
      Fireballs.Add(fb);
      return MagicAttack;
    } else {
      return 0;
    }
  }
  public void TakeDamage (int attack) {
    if (!IsBubbleShieldActive){
      Health -= attack;
    }
  }

  public void RecoverMana () {
    Mana += ManaRecovery;
    if (Mana > MaxMana) {
      Mana = MaxMana;
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

  public bool IsDead () {
    if (Health <= 0) {
      return true;
    } else {
      return false;
    }
  }

  public void Draw(Texture2D sprite) {
    Raylib.DrawTexture(sprite, 30, 236, Color.White);
    if (Fireballs.Count > 0) {
      List<int> fbToDeletes = [];
      int i = 0;
      foreach (var fb in Fireballs)
      {
        fb.Draw();
        if (fb.PositionX > 1050) {
          fbToDeletes.Add(i);
        }
        i++;
      }
      foreach(var fbsToDelete in fbToDeletes) {
        Fireballs.RemoveAt(fbsToDelete);
      }
    }
    Raylib.DrawText("Fireballs: " + Fireballs.Count, 10, 400, 20, Color.Red);
    if (IsBubbleShieldActive && Shield != null) {
      Shield.Draw();
    }
  }
}

