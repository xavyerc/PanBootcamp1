using Raylib_cs;
public class Character
{
  public int Health { get; set; }
  public int MaxHealth { get; set; }
  public int Mana { get; set; }
  public int MaxMana { get; set; }
  public int MeeleAttack { get; set; }
  public int MagicAttack { get; set; }
  public int ManaRecovery { get; set; }
  public int ManaCost { get; set; }
  public int PositionX { get; set; }
  public int PositionY { get; set; }
  public int FightPositionX { get; set; }
  public int FightPositionY { get; set; }
  public bool IsDead { get; set; }
  public List<Fireball> Fireballs { get; set; }
  public CharacterType characterType { get; set; }

  public virtual int Attack (AttackType selection) {
    Fireball fb;
    var posX = characterType == CharacterType.PLAYER ? 104 : 0;
    if (selection == AttackType.MEELE_ATTACK) {
      RecoverMana();
      fb = new Fireball(FightPositionX + posX, FightPositionY + 25, AttackType.MEELE_ATTACK);
      Fireballs.Add(fb);
      return MeeleAttack;
    } else if (selection == AttackType.MAGIC_ATTACK && (Mana >= ManaCost)) {
      Mana -= ManaCost;
      fb = new Fireball(FightPositionX + posX, FightPositionY + 25, AttackType.MAGIC_ATTACK);
      Fireballs.Add(fb);
      return MagicAttack;
    } else {
      return 0;
    }
  }
  public void RecoverMana () {
    Mana += ManaRecovery;
    if (Mana > MaxMana) {
      Mana = MaxMana;
    }
  }

  public bool GetIsDead () {
    if (Health <= 0) {
      return true;
    } else {
      return false;
    }
  }

  public virtual void Draw(Texture2D sprite, int gameState) {
    if (gameState == 0)
    {
      Raylib.DrawTexture(sprite, FightPositionX, FightPositionY, Color.White);
    } else
    {
      Raylib.DrawTexture(sprite, PositionX, PositionY, Color.White);
    }
    if (Fireballs.Count > 0) {
      List<int> fbToDeletes = [];
      int i = 0;
      foreach (var fb in Fireballs)
      {
        fb.Draw(characterType);
        if (
          (characterType == CharacterType.PLAYER && fb.PositionX > 1050) ||
          (characterType == CharacterType.ENEMY && fb.PositionX < 0)
        ) {
          fbToDeletes.Add(i);
        }
        i++;
      }
      foreach(var fbsToDelete in fbToDeletes) {
        Fireballs.RemoveAt(fbsToDelete);
      }
    }
  }
  
}