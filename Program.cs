//Libs
using Raylib_cs;

//Game settings and variables
int winHeight = 500;
int winWidth = 1000;
Raylib.InitWindow(winWidth, winHeight, "RPG - Poketemu");
int fpsTarget = 60;
Random rnd = new Random();
Raylib.SetTargetFPS(fpsTarget);

//Class variables
Player player1 = new(
  100,
  75,
  75,
  5,
  10,
  10,
  25,
  2,
  3,
  3,
  false
);

Enemy enemy = new (
  120,
  100,
  100,
  3,
  12,
  15,
  25
);

//Game variables
int currentTurn = 0;
var gameState = 0;

// Textures
var skyTexture = Raylib.LoadTexture("assets/sky.jpg");
var grassTexture = Raylib.LoadTexture("assets/grass.jpg");
var catPlayer = Raylib.LoadTexture("assets/cat.png");
var frogEnemy = Raylib.LoadTexture("assets/frog.png");
var floor = new Rectangle(100, 300, 1000, 200);
var sky = new Rectangle(0, 0, 1000, 300);
Color skyColor = new Color(0, 0, 255, 150);

//------------Functions------------//

void drawBackground () {
  Raylib.DrawTextureRec(grassTexture, floor, new System.Numerics.Vector2(0, 300), Color.Green);
  Raylib.DrawTextureRec(skyTexture, sky, new System.Numerics.Vector2(0, 0), skyColor);
}

void renderScreen () {
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Black);
  drawBackground();
  player1.Draw(catPlayer);
  enemy.Draw(frogEnemy);

  Color currentRenderColor = Color.Green;

  Raylib.DrawText("Health:", 20, 20, 20, currentRenderColor);
  if (player1.Health > 20) currentRenderColor = Color.Green ; else currentRenderColor = Color.Red;
  Raylib.DrawRectangle(100, 20, player1.Health, 20, currentRenderColor);

  Raylib.DrawText("Mana:", 20, 40, 20, Color.Green);
  if (player1.Mana < player1.ManaCost) currentRenderColor =  Color.Red; else currentRenderColor = Color.Blue;
  Raylib.DrawRectangle(100, 40, player1.Mana, 20, currentRenderColor);

  if (player1.BoubbleShieldInv > 0) currentRenderColor = Color.Green; else currentRenderColor = Color.Red;
  Raylib.DrawText("Shield: " + player1.BoubbleShieldInv, 20, 60, 20, currentRenderColor);

  Raylib.DrawText("Enemy Health", 800, 20, 20, Color.Blue);
  if (enemy.Health > 20) currentRenderColor = Color.Green; else currentRenderColor = Color.Red;
  Raylib.DrawRectangle(800 - enemy.Health, 20, enemy.Health, 20, currentRenderColor);

  Raylib.DrawText("Enemy mana", 800, 40, 20, Color.Blue);
  if (enemy.Mana < enemy.ManaCost) currentRenderColor = Color.Red; else currentRenderColor =  Color.Blue;
  Raylib.DrawRectangle(800 - enemy.Mana, 40, enemy.Mana, 20, currentRenderColor);

  Raylib.DrawText("Elige un ataque (meele = 1, magic = 2, Boubble shield mamalon = 3): ", 150, 150, 20, Color.Red);

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
    case 0: renderScreen(); break;
    case 1: victoryScreen(); break;
    case 2: deathScreen(); break;
  }

  var selection = Raylib.GetKeyPressed();
  switch (selection) {
    case 49:  enemy.TakeDamage(player1.Attack(AttackType.MEELE_ATTACK)); break;
    case 50:  enemy.TakeDamage(player1.Attack(AttackType.MAGIC_ATTACK)); break;
    case 51:  player1.UseBoubbleShield(); break;
    default: continue;
  }

  var enemySelection = (rnd.Next(99) % 2) + 1;

  switch (enemySelection) {
    case 1:  player1.TakeDamage(enemy.Attack(AttackType.MEELE_ATTACK)); break;
    case 2:  player1.TakeDamage(enemy.Attack(AttackType.MAGIC_ATTACK)); break;
    default: continue;
  }

  player1.RegenerateBS(currentTurn);
  currentTurn += 1;

  if (player1.IsDead()) {
    gameState = 2;
  } else if (enemy.IsDead()) {
    gameState = 1;
  }
}

Raylib.CloseWindow();
