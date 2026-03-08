int health = 100;
int mana = 75;
int maxMana = 75;
int meeleAttack = 5;
int magicAttack = 10;
int manaRecovery = 10;
int manaCost = 25;

int enemyHealth = 120;
int enemyMana = 100;
int enemyMaxMana = 100;
int enemyMeeleAttack = 3;
int enemyMagicAttack = 12;
int enemyManaRecovery = 15;


void doDamage(int attack) {
 enemyHealth -= attack; // enemyHealth = enemyHealth - attack;
}

void takeDamage (int attack) {
  health -= attack; // health = health - attack;
}

void recoverMana () {
  mana += manaRecovery;
  if (mana > maxMana) {
    mana = maxMana;
  }
}

void enemyRecoverMana () {
  enemyMana += enemyManaRecovery;
  if (enemyMana > enemyMaxMana) {
    enemyMana = enemyMaxMana;
  }

}

Random rnd = new Random();

while (health > 0 && enemyHealth > 0) {
  Console.Clear();
  Console.WriteLine("Health: " + health + "                 Enemy health: " + enemyHealth);
  Console.WriteLine("Mana: " + mana + "                 Enemy mana: " + enemyMana);
  Console.WriteLine("Elige un ataque (meele = 1, magic = 2): ");
  string selection = Console.ReadLine();
  if (selection == "2") {
    if (mana >= manaCost) {
      doDamage(magicAttack);
      mana -= manaCost;
    } else {
      Console.WriteLine("Not enough mana");
    }
  } else if (selection == "1") {
    recoverMana();
    doDamage(meeleAttack);
  } else {
    Console.WriteLine("Invalid option, press enter to continue...");
    Console.ReadLine();
    continue;
  }

  var enemySelection = (rnd.Next(99) % 2) + 1;

  if (enemySelection == 2) {
    if (enemyMana >= manaCost) {
      takeDamage(enemyMagicAttack);
      enemyMana -= manaCost;
    } else {
      Console.WriteLine("Not enough mana");
    }
  } else if (enemySelection == 1) {
    enemyRecoverMana();
    takeDamage(enemyMeeleAttack);
  }
}

if (health <= 0) {
  Console.WriteLine("You died!");
} else if (enemyHealth <= 0) {
  Console.WriteLine("You win!");
}

