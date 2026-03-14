//Libs
using System.Net;
using System.Reflection.Metadata;
using Raylib_cs;

//Player variables
int health = 100;
int mana = 75;
int maxMana = 75;
int meeleAttack = 5;
int magicAttack = 10;
int manaRecovery = 10;
int manaCost = 25;
int boubbleShieldInv = 2;
int boubbleShieldMax = 3;
int boubbleShieldCD = 3;
bool isBubbleShieldActive = false;

//Enemy variables
int enemyHealth = 120;
int enemyMana = 100;
int enemyMaxMana = 100;
int enemyMeeleAttack = 3;
int enemyMagicAttack = 12;
int enemyManaRecovery = 15;

//Game variables
int currentTurn = 0;
var gameState = 0;

//Game settings and variables
int winHeight = 500;
int winWidth = 1000;
int fpsTarget = 60;
Random rnd = new Random();
Raylib.InitWindow(winWidth, winHeight, "RPG - Poketemu");
Raylib.SetTargetFPS(fpsTarget);

//------------Functions------------//
void doDamage(int attack) {
  if (attack == meeleAttack) {
    enemyHealth -= attack;
    recoverMana();
  } else if ((attack == magicAttack) && (mana >= manaCost)) {
    enemyHealth -= attack;
    mana -= manaCost;
  }
}

void takeDamage (int attack) {
  if (!isBubbleShieldActive){
    if (attack == enemyMeeleAttack) {
      health -= attack;
      enemyRecoverMana();
    } else if ((attack == enemyMagicAttack) && (enemyMana >= manaCost)) {
      health -= attack;
      enemyMana -= manaCost;
    }
  } else if (attack == enemyMagicAttack){
    enemyMana -= manaCost;
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

void boubbleShield () {
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

  Color currentRenderColor = Color.Green;

  Raylib.DrawText("Health:", 20, 20, 20, currentRenderColor);
  if (health > 20) currentRenderColor = Color.Green ; else currentRenderColor = Color.Red;
  Raylib.DrawRectangle(100, 20, health, 20, currentRenderColor);

  Raylib.DrawText("Mana:", 20, 40, 20, Color.Green);
  if (mana < manaCost) currentRenderColor =  Color.Red; else currentRenderColor = Color.Blue;
  Raylib.DrawRectangle(100, 40, mana, 20, currentRenderColor);

  if (boubbleShieldInv > 0) currentRenderColor = Color.Green; else currentRenderColor = Color.Red;
  Raylib.DrawText("Shield: " + boubbleShieldInv, 20, 60, 20, currentRenderColor);

  Raylib.DrawText("Enemy Health", 800, 20, 20, Color.Blue);
  if (enemyHealth > 20) currentRenderColor = Color.Green; else currentRenderColor = Color.Red;
  Raylib.DrawRectangle(800 - enemyHealth, 20, enemyHealth, 20, currentRenderColor);

  Raylib.DrawText("Enemy mana", 800, 40, 20, Color.Blue);
  if (enemyMana < manaCost) currentRenderColor = Color.Red; else currentRenderColor =  Color.Blue;
  Raylib.DrawRectangle(800 - enemyMana, 40, enemyMana, 20, currentRenderColor);

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

while (!Raylib.WindowShouldClose()) {
  switch (gameState) {
    case 0: renderScreen();  break;
    case 1: victoryScreen(); break;
    case 2: deathScreen();   break;
  }

  var selection = Raylib.GetKeyPressed();
  switch (selection) {
    case 49:  doDamage(meeleAttack); break;
    case 50:  doDamage(magicAttack); break;
    case 51:  boubbleShield();       break;
    default: continue;
  }

  var enemySelection = (rnd.Next(99) % 2) + 1;

  switch (enemySelection) {
    case 1:  takeDamage(enemyMeeleAttack); break;
    case 2:  takeDamage(enemyMagicAttack); break;
    default: continue;
  }

  isBubbleShieldActive = false;
  regenerateBS();
  currentTurn += 1;

  if (health <= 0) {
    gameState = 2;
  } else if (enemyHealth <= 0) {
    gameState = 1;
  }
}

Raylib.CloseWindow();
