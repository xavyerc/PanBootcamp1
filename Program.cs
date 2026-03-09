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

int boubbleShieldInv = 2;
int boubbleShieldMax = 3;
int boubbleShieldCD = 3;
bool isBubbleShieldActive = false;

int currentTurn = 0;

void doDamage(int attack) {
 enemyHealth -= attack; // enemyHealth = enemyHealth - attack;
 recoverMana();
}

void takeDamage (int attack) {
  if (!isBubbleShieldActive){
    health -= attack; // health = health - attack;
  } else {
    Console.WriteLine("You blocked the attack succesfully!");
  }
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

void boubbleShield ()
{
  if (boubbleShieldInv >= 1) {
    isBubbleShieldActive = true;
    boubbleShieldInv -= 1;
  } else {
    Console.WriteLine("Not enough boubble shields"); //TODO add CD timer in message here
  }
  recoverMana();
}

void regenerateBS () {
  if ((currentTurn % boubbleShieldCD == 0) && (boubbleShieldInv < boubbleShieldMax) && (currentTurn != 0)) {
    boubbleShieldInv += 1;
  }
}

Random rnd = new Random();

while (health > 0 && enemyHealth > 0) {
  Console.Clear();
  Console.WriteLine("The current turn is: " + (currentTurn+1));
  Console.WriteLine("Health: " + health + "                 Enemy health: " + enemyHealth);
  Console.WriteLine("Mana: " + mana + "                 Enemy mana: " + enemyMana);
  Console.WriteLine("Boubble shields available: " + boubbleShieldInv);
  Console.WriteLine("Elige un ataque (meele = 1, magic = 2, Boubble shield mamalon = 3): ");
  string selection = Console.ReadLine();
  if (selection == "2") { //TODO change if to switch
    if (mana >= manaCost) {
      doDamage(magicAttack);
      mana -= manaCost;
    } else {
      Console.WriteLine("Not enough mana");
    }
  } else if (selection == "1") {
    doDamage(meeleAttack);
  } else if (selection == "3"){
    boubbleShield();
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

  isBubbleShieldActive = false;
  regenerateBS();

  currentTurn += 1;
}

if (health <= 0) {
  Console.WriteLine("You died!");
} else if (enemyHealth <= 0) {
  Console.WriteLine("You win!");
}

