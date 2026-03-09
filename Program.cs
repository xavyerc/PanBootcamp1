using Raylib_cs;

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
var gameState = 0;

void doDamage(int attack) {
 enemyHealth -= attack; // enemyHealth = enemyHealth - attack;
 recoverMana();
}

void takeDamage (int attack) {
  if (!isBubbleShieldActive){
    health -= attack; // health = health - attack;
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
  }
  recoverMana();
}

void regenerateBS () {
  if ((currentTurn % boubbleShieldCD == 0) && (boubbleShieldInv < boubbleShieldMax) && (currentTurn != 0)) {
    boubbleShieldInv += 1;
  }
}

void renderScreen () {
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Black);
  Raylib.DrawText("Health:", 20, 20, 20, Color.Red);
  Raylib.DrawRectangle(100, 20, health, 20, Color.Green);
  Raylib.DrawText("Mana:", 20, 40, 20, Color.Red);
  Raylib.DrawRectangle(100, 40, mana, 20, Color.Blue);
  Raylib.DrawText("Shield: " + boubbleShieldInv, 20, 60, 20, Color.Red);


  Raylib.DrawText("Enemy Health", 800, 20, 20, Color.Red);
  Raylib.DrawRectangle(800 - enemyHealth, 20, enemyHealth, 20, Color.Green);
  Raylib.DrawText("Enemy mana", 800, 40, 20, Color.Red);
  Raylib.DrawRectangle(800 - enemyMana, 40, enemyMana, 20, Color.Blue);

  Raylib.DrawText("Elige un ataque (meele = 1, magic = 2, Boubble shield mamalon = 3): ", 150, 250, 20, Color.Red);
  Raylib.EndDrawing();
}

void victoryScreen () {
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Black);
  Raylib.DrawText("Supreme Victory", 350, 250, 20, Color.Green);
  Raylib.EndDrawing();

}

void deathScreen () {
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Black);
  Raylib.DrawText("You Died", 100, 200, 200, Color.Red);
  Raylib.EndDrawing();
}

Random rnd = new Random();

Raylib.InitWindow(1000, 500, "RPG - Poketemu");
Raylib.SetTargetFPS(60);
float timer = 0f;
int x = 1000;

while (!Raylib.WindowShouldClose())
{
  switch (gameState) {
    case 0: renderScreen(); break;
    case 1: victoryScreen(); break;
    case 2: deathScreen(); break;
  }

    var selection = Raylib.GetKeyPressed();
    if (selection == '2') { //TODO change if to switch
    if (mana >= manaCost) {
      doDamage(magicAttack);
      mana -= manaCost;
    } else {
    }
  } else if (selection == '1') {
    doDamage(meeleAttack);
  } else if (selection == '3'){
    boubbleShield();
  } else {
    continue;
  }

  var enemySelection = (rnd.Next(99) % 2) + 1;

  if (enemySelection == 2) {
    if (enemyMana >= manaCost) {
      takeDamage(enemyMagicAttack);
      enemyMana -= manaCost;
    } else {
    }
  } else if (enemySelection == 1) {
    enemyRecoverMana();
    takeDamage(enemyMeeleAttack);
  }

  isBubbleShieldActive = false;
  regenerateBS();

  currentTurn += 1;

  if (health <= 0) {
    gameState = 2;
  } else if (enemyHealth <= 0) {
    gameState = 1;
  }

  //   timer += Raylib.GetFrameTime();

  // if (timer >= 0.5f) {
  //   x -= 10;
  //   timer = 0f;
  }

Raylib.CloseWindow();

