using Raylib_cs;

public class Enemy {
  // Atributos
  public int Health { get; set; }
  public int Mana { get; set; }
  public int MaxMana { get; set; }
  public int MeeleAttack { get; set; }
  public int MagicAttack { get; set; }
  public int ManaRecovery { get; set; }
  public int ManaCost { get; set; }

  //Constructor
  public Enemy (
    int health,
    int mana,
    int maxMana,
    int meeleAttack,
    int magicAttack,
    int manaRecovery,
    int manaCost
  ) {
    Health = health;
    Mana = mana;
    MaxMana = maxMana;
    MeeleAttack = meeleAttack;
    MagicAttack = magicAttack;
    ManaRecovery = manaRecovery;
    ManaCost = manaCost;
  }

  //Metodos
  public int Attack (AttackType selection) {
    if (selection == AttackType.MEELE_ATTACK && (Mana >= ManaCost)) {
      RecoverMana();
      return MeeleAttack;
    } else if (selection == AttackType.MAGIC_ATTACK) {
      Mana -= ManaCost;
      return MagicAttack;
    } else {
      return 0;
    }
  }

  public void TakeDamage (int attack) {
    Health -= attack;
  }

  public void RecoverMana () {
    Mana += ManaRecovery;
    if (Mana > MaxMana) {
      Mana = MaxMana;
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
    Raylib.DrawTexture(sprite, 900, 236, Color.White);
  }
}