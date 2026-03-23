//Libs
using System.Numerics;
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

List<Enemy> enemies = new List<Enemy>();
for (int i = 0; i < 10; i++)
{
  var enemy = new Enemy(
    20,
    120,
    100,
    100,
    3,
    12,
    15,
    25
  );
  enemies.Add(enemy);
}

var currentEnemy = enemies[0];
var currentEnemyIndex = 0;

//Game variables
int currentTurn = 0;
var gameState = 3;
var cameraInsideLimit = new Rectangle(500, 250, 1800 - 500, 1200 - 250);
var camX = player1.PositionX;
var camY = player1.PositionY;

// Textures
var skyTexture = Raylib.LoadTexture("assets/sky.jpg");
var grassTexture = Raylib.LoadTexture("assets/grass.jpg");
var catPlayer = Raylib.LoadTexture("assets/cat.png");
var frogEnemy = Raylib.LoadTexture("assets/frog.png");
var terrain = Raylib.LoadTexture("assets/terrain.png"); // 1800, 1200
var floor = new Rectangle(100, 300, 1000, 200);
var terrainRec = new Rectangle(0, 0, 1800, 1200);
var sky = new Rectangle(0, 0, 1000, 300);
Color skyColor = new Color(0, 0, 255, 150);

//------------Functions------------//

void drawBackground () {
  Raylib.DrawTextureRec(grassTexture, floor, new Vector2(0, 300), Color.Green);
  Raylib.DrawTextureRec(skyTexture, sky, new Vector2(0, 0), skyColor);
}

void renderScreen () {
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Black);
  drawBackground();
  player1.Draw(catPlayer, gameState);
  currentEnemy.Draw(frogEnemy, gameState);

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
  if (currentEnemy.Health > 20) currentRenderColor = Color.Green; else currentRenderColor = Color.Red;
  Raylib.DrawRectangle(800 - currentEnemy.Health, 20, currentEnemy.Health, 20, currentRenderColor);

  Raylib.DrawText("Enemy mana", 800, 40, 20, Color.Blue);
  if (currentEnemy.Mana < currentEnemy.ManaCost) currentRenderColor = Color.Red; else currentRenderColor =  Color.Blue;
  Raylib.DrawRectangle(800 - currentEnemy.Mana, 40, currentEnemy.Mana, 20, currentRenderColor);

  Raylib.DrawText("Elige un ataque (meele = 1, magic = 2, Boubble shield mamalon = 3): ", 150, 150, 20, Color.Red);
  Raylib.DrawText("Current Enemy Is DEad: " + currentEnemy.IsDead + "Index: " + currentEnemyIndex, 10, 350, 50, Color.Red);

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

void drawOverworld ()
{
  camX = player1.PositionX > cameraInsideLimit.X && player1.PositionX < cameraInsideLimit.Width ? player1.PositionX : camX;
  camY = player1.PositionY > cameraInsideLimit.Y && player1.PositionY < cameraInsideLimit.Height ? player1.PositionY : camY;
  var playerRec = new Rectangle(player1.PositionX, player1.PositionY, 64, 64);
  var camera = new Camera2D(new Vector2(winWidth / 2, winHeight / 2), new Vector2(camX, camY), 0f, 1.0f);
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.SkyBlue);
  Raylib.BeginMode2D(camera);                          // Begin 2D mode with custom camera (2D)
  Raylib.DrawTextureRec(terrain, terrainRec, new System.Numerics.Vector2(0, 0), Color.White);
  player1.Draw(catPlayer, gameState);
  int i = 0;
  foreach (var enemy in enemies)
  {
    var enemyRec = new Rectangle(enemy.PositionX, enemy.PositionY, 64, 64);
    enemy.Draw(frogEnemy, gameState);
    var isCollision = Raylib.CheckCollisionRecs(playerRec, enemyRec) && !enemy.IsDead;
    if (isCollision)
    {
      currentEnemy = enemies[i];
      currentEnemyIndex = i;
      player1.PositionX = 30;
      player1.PositionY = 236;
      gameState = 0;
    }
    i++;
  }
  Raylib.EndMode2D();
  Raylib.EndDrawing();
}

void resetCharacterStats (Character character)
{
  character.Health = character.MaxHealth;
  character.Mana = character.MaxMana;
  if (character is Player player)
  {
    player.BoubbleShieldInv = 2;
  }
}

while (!Raylib.WindowShouldClose()) {
  switch (gameState) {
    case 0: renderScreen(); break;
    case 1: victoryScreen(); break;
    case 2: deathScreen(); break;
    case 3: drawOverworld(); break;
  }

  var selection = Raylib.GetKeyPressed();
  switch (selection) {
    case 49:  currentEnemy.TakeDamage(player1.Attack(AttackType.MEELE_ATTACK)); break;
    case 50:  currentEnemy.TakeDamage(player1.Attack(AttackType.MAGIC_ATTACK)); break;
    case 51:  player1.UseBoubbleShield(); break;
    default: continue;
  }

  var enemySelection = (rnd.Next(99) % 2) + 1;

  switch (enemySelection) {
    case 1:  player1.TakeDamage(currentEnemy.Attack(AttackType.MEELE_ATTACK)); break;
    case 2:  player1.TakeDamage(currentEnemy.Attack(AttackType.MAGIC_ATTACK)); break;
    default: continue;
  }

  player1.RegenerateBS(currentTurn);
  currentTurn += 1;

  if (player1.GetIsDead()) {
    resetCharacterStats(player1);
    resetCharacterStats(currentEnemy);
    gameState = 3;
  } else if (currentEnemy.GetIsDead()) {
    resetCharacterStats(player1);
    enemies[currentEnemyIndex].IsDead = true;
    gameState = 3;
  }
}

Raylib.CloseWindow();
