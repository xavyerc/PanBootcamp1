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

  public Player () {
    Health = 100;
  }

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
  }

  //Methods
  public int Attack (AttackType selection) {
    if (selection == AttackType.MEELE_ATTACK) {
      RecoverMana();
      return MeeleAttack;
    } else if (selection == AttackType.MAGIC_ATTACK && (Mana >= ManaCost)) {
      Mana -= ManaCost;
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
}

