using Raylib_cs;

public class Enemy : Character {

  //Constructor
  public Enemy (
    int health,
    int maxHealth,
    int mana,
    int maxMana,
    int meeleAttack,
    int magicAttack,
    int manaRecovery,
    int manaCost
  ) {
    Random rnd = new Random();
    var x = rnd.Next(1800 - 64);
    var y = rnd.Next(1200 - 64);

    Health = health;
    Mana = mana;
    MaxMana = maxMana;
    MeeleAttack = meeleAttack;
    MagicAttack = magicAttack;
    ManaRecovery = manaRecovery;
    ManaCost = manaCost;
    Fireballs = [];
    PositionX = x;
    PositionY = y;
    FightPositionX = 900;
    FightPositionY = 236;
    characterType = CharacterType.ENEMY;
    IsDead = false;
  }

  public void TakeDamage (int attack) {
    Health -= attack;
  }
}