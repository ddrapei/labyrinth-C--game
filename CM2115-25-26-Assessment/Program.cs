using Observers;
using Observers.MainMenuObservers;
using Observers.InsideInventoryObservers;
using Observers.CombatObservers;
using Observers.PuzzleObservers;
using Commands;
using Commands.DisplayCommands;
using Commands.MainMenuCommands;
using Commands.MoveCommands;
using Commands.InventoryCommands;
using Commands.CombatCommands;
using Commands.PuzzleCommands;
using Items;
using Items.Armour;
using Items.Armour.LeatherArmourSet;
using Items.Armour.CrudeKnightsArmourSet;
using Items.Armour.CircusAcrobatArmourSet;
using Items.Potions;
using Perks;
using Enemies;
using Rooms;
using PlayerDisplay;
using PlayerMovement;
using PlayerEquipment;
using PlayerLevelUp;
using Puzzles;


using Pastel;

// creates a player
Player player = Player.GetInstance();

// those are created as logic for commands
// this set up allows to add new movement without touching player
// strategy pattern was used
player.RegisterMoveBehavior("move up", new PlayerMoveUp());
player.RegisterMoveBehavior("move down", new PlayerMoveDown());
player.RegisterMoveBehavior("move left", new PlayerMoveLeft());
player.RegisterMoveBehavior("move right", new PlayerMoveRight());
// player.RegisterMoveBehavior("move up and right", new PlayerMoveDiagonallyUpAndRight()); - uncomment to test extandability of the movement commands

// the same approach was implemented for the equiping behavious
player.RegisterEquipBehavior("weapon", new EquipWeaponBehavior());
player.RegisterEquipBehavior("shield", new EquipShieldBehavior());
player.RegisterEquipBehavior("head", new EquipHeadArmourBehavior());
player.RegisterEquipBehavior("torso", new EquipTorsoArmourBehavior());
player.RegisterEquipBehavior("legs", new EquipLegsArmourBehavior());
player.RegisterEquipBehavior("potion", new UseHealingPotionBehavior());

// registering levelUp behaviours
player.RegisterLevelUpBehavior("level up", new PlayerLevelUpBehavior());
player.RegisterLevelUpBehavior("check level up", new PlayerCheckLevelUpBehavior());


// creates a game
var game = new Game();

// creates a room checker
var roomChecker = RoomChecker.GetInstance();

// creates an input manager
var InputManager = new InputManager();

// creates observers for the game

// handles start the game
var mainMenuObserver = new MainMenuObserver(game);
var mainMenuUnknownCommandObserver = new MainMenuUnknownCommandObserver(game);

// handles finish the game
var gameHandlerObserver = new GameHandlerObserver(game);

// handles player movement commands
var gameCommandMoveObserver = new GameCommandMoveObserver(game);

// handles inventory commands (when inventory is closed)
var inventoryObserver = new InventoryObserver(game);

// handles commands inside inventory (when inventory is open)
var insideInventoryObserver = new InsideInventoryObserver(game);

// handles uknown commands inside inventory (when inventory is open)
var insideInventoryUnknownCommandObserver = new InsideInventoryUnknownCommandObserver(game);

// handles combat commands
var combatObserver = new CombatObserver(game);

// handles unknown commands during combat
var combatUnknownCommandObserver = new CombatUnknownCommandObserver(game);

// handles unknown commands
var unknownCommandObserver = new UnknownCommandObserver(game);

// puzzle observers
var puzzleObserver = new PuzzleObserver(game);
var unknownCommandPuzzleObserver = new UnknownCommandPuzzleObserver(game);


// Main Menu Commands
// command to start the game
var startGameCommand = new StartGameCommand(game, InputManager, mainMenuObserver, mainMenuUnknownCommandObserver, gameCommandMoveObserver, gameHandlerObserver, inventoryObserver ,unknownCommandObserver);
// command to exit the game
var exitGameCommand = new ExitGameCommand(game);
var ShowGameStatsCommand = new ShowGameStatsCommand();

// display commands
var playerStatsDisplay = new PlayerStatsDisplay();
var playerEquippedDisplay = new PlayerEquippedDisplay();
var lookAround = new LookAroundCommand();

// those are just commands
// creates commands for player movement
var moveDown = new MoveDownCommand();
var moveUp = new MoveUpCommand();
var moveLeft = new MoveLeftCommand();
var moveRight = new MoveRightCommand();
// var moveDiagonallyUpAndRight = new MoveDiagonallyUpAndRight(); - test movement command to easily test extandability

// creates inventory commands (for when inventory is closed)
var pickUpItemCommand = new PickUpItemCommand();
var openInventoryCommand = new OpenInventoryCommand(InputManager, insideInventoryObserver, insideInventoryUnknownCommandObserver, gameCommandMoveObserver, gameHandlerObserver, inventoryObserver, unknownCommandObserver);

// creates commands for inside inventory (for when inventory is open)
var closeInventoryCommand = new CloseInventoryCommand(InputManager, insideInventoryObserver, insideInventoryUnknownCommandObserver, gameCommandMoveObserver, gameHandlerObserver, inventoryObserver, unknownCommandObserver);

// display stats command
var displayStatsCommand = new DisplayStatsCommand(playerStatsDisplay);
var displayEquippedCommand = new DisplayEquippedCommand(playerEquippedDisplay);

// creates combat commands
var attackCommand = new AttackCommand(game, InputManager);
var runAwayCommand = new RunAwayCommand(InputManager);
var fightCommand = new FightCommand(game, InputManager);

// creating puzzles
var sphynxPuzzle = new SphynxPuzzle();
var oedipusPuzzle = new OedipusPuzzle();
var persephonePuzzle = new PersephonePuzzle();
var sisyphusPuzzle = new SisyphusPuzzle();



// puzzle commands
var enterPuzzleCommand = new EnterPuzzleCommand();
var answerRiddleCommand = new AnswerRiddleCommand();  

// registers start game command with its observer 
mainMenuObserver.AddCommand("start", startGameCommand);
mainMenuObserver.AddCommand("start game", startGameCommand);
mainMenuObserver.AddCommand("stats", ShowGameStatsCommand);
mainMenuObserver.AddCommand("statistics", ShowGameStatsCommand);
mainMenuObserver.AddCommand("exit", exitGameCommand);


// commands to exit the game
gameHandlerObserver.AddCommand("exit", exitGameCommand);
gameHandlerObserver.AddCommand("exit game", exitGameCommand);
gameHandlerObserver.AddCommand("finish", exitGameCommand);

// display stats
gameHandlerObserver.AddCommand("stats", displayStatsCommand);
gameHandlerObserver.AddCommand("equipped", displayEquippedCommand);
gameHandlerObserver.AddCommand("look around", lookAround);

// fight command
gameHandlerObserver.AddCommand("fight", fightCommand);
gameHandlerObserver.AddCommand("attack", fightCommand);

// puzzle commands


// Register the universal answer command with puzzle observer
puzzleObserver.AddCommand("answer", answerRiddleCommand);
puzzleObserver.AddCommand("say", answerRiddleCommand);

// registers movement commands
gameCommandMoveObserver.AddCommand("move up", moveUp);
gameCommandMoveObserver.AddCommand("move down", moveDown);
gameCommandMoveObserver.AddCommand("move left", moveLeft);
gameCommandMoveObserver.AddCommand("move right", moveRight);
// gameCommandMoveObserver.AddCommand("move up and right", moveDiagonallyUpAndRight); -- uncomment to test extendability of movement commands


// registers inventory commands (work when inventory is closed)
inventoryObserver.AddCommand("pick up", pickUpItemCommand);
inventoryObserver.AddCommand("pick", pickUpItemCommand);
inventoryObserver.AddCommand("inventory", openInventoryCommand);
inventoryObserver.AddCommand("inv", openInventoryCommand);
inventoryObserver.AddCommand("i", openInventoryCommand);


// registers inside inventory commands (work when inventory is open)
insideInventoryObserver.AddCommand("close", closeInventoryCommand);
insideInventoryObserver.AddCommand("exit", closeInventoryCommand);

// registers combat commands
combatObserver.AddCommand("attack", attackCommand);
combatObserver.AddCommand("run", runAwayCommand);
combatObserver.AddCommand("flee", runAwayCommand);
combatObserver.AddCommand("escape", runAwayCommand);


// registers valid commands with the unknown command observer in the main menu
mainMenuUnknownCommandObserver.RegisterValidCommand("start");
mainMenuUnknownCommandObserver.RegisterValidCommand("start game");
mainMenuUnknownCommandObserver.RegisterValidCommand("stats");
mainMenuUnknownCommandObserver.RegisterValidCommand("statistics");
mainMenuUnknownCommandObserver.RegisterValidCommand("exit");

// registers valid commands with the unknown command observer in the main game
unknownCommandObserver.RegisterValidCommand("exit");
unknownCommandObserver.RegisterValidCommand("exit game");
unknownCommandObserver.RegisterValidCommand("finish");
unknownCommandObserver.RegisterValidCommand("move up");
unknownCommandObserver.RegisterValidCommand("move down");
unknownCommandObserver.RegisterValidCommand("move left");
unknownCommandObserver.RegisterValidCommand("move right");
unknownCommandObserver.RegisterValidCommand("stats");
unknownCommandObserver.RegisterValidCommand("equipped");
unknownCommandObserver.RegisterValidCommand("inventory");
unknownCommandObserver.RegisterValidCommand("inv");
unknownCommandObserver.RegisterValidCommand("i");
unknownCommandObserver.RegisterValidCommand("pick up");
unknownCommandObserver.RegisterValidCommand("fight");
unknownCommandObserver.RegisterValidCommand("attack");
unknownCommandObserver.RegisterValidCommand("look around");
unknownCommandObserver.RegisterValidCommand("enter puzzle");
unknownCommandObserver.RegisterValidCommand("talk");
unknownCommandObserver.RegisterValidCommand("interact");
unknownCommandObserver.RegisterValidCommand("speak");
// unknownCommandObserver.RegisterValidCommand("move up and right"); - uncomment to test extendability of movement commands

// registers valid commands with the unknown command observer in the inventory
insideInventoryUnknownCommandObserver.RegisterValidCommand("equip");
insideInventoryUnknownCommandObserver.RegisterValidCommand("drop");
insideInventoryUnknownCommandObserver.RegisterValidCommand("use"); 
insideInventoryUnknownCommandObserver.RegisterValidCommand("close");

// registers valid commands with the unknown command observer during combat
combatUnknownCommandObserver.RegisterValidCommand("attack");
combatUnknownCommandObserver.RegisterValidCommand("run");
combatUnknownCommandObserver.RegisterValidCommand("flee");
combatUnknownCommandObserver.RegisterValidCommand("escape");

// observers that are required to start the game
InputManager.AddObserver(mainMenuUnknownCommandObserver);
InputManager.AddObserver(mainMenuObserver);
// all other observers are added and removed depending on the state of the game

// weapons
Weapon spoon_with_a_hole = new Weapon("Spoon with a hole", 1);
Weapon rusty_shank = new Weapon("Rusty Shank", 2);
Weapon rusty_sword = new Weapon("Rusty Sword", 1000);  
Weapon wooden_club = new Weapon("Wooden Club", 7);
Weapon iron_sword = new Weapon("Iron Sword", 15);
Weapon steel_axe = new Weapon("Steel Axe", 25);
Weapon enchanted_bow = new Weapon("Enchanted Bow", 20);
Weapon roman_pilum = new Weapon("Roman Pilum", 27);
Weapon flaming_sword = new Weapon("Flaming Sword", 40);
Weapon ice_dagger = new Weapon("Ice Dagger", 30);
Weapon thunder_hammer = new Weapon("Thunder Hammer", 50);
Weapon malevolent_falchion = new Weapon("Malevolent Falchion", 46);
Weapon hebron_sickle = new Weapon("Hebron, the Accursed Sickle", 200);
Weapon bethlehem_spear = new Weapon("Bethlehem, Spear of the Traitor", 320);
Weapon headsman_of_memphis = new Weapon("Headsman of Memphis", 400);
Weapon jericho_sword = new Weapon("Jericho, the Sanctified Sword", 690);



// potions
Potion open_healing_potion = new HealingPotion("Open Healing Potion", 5);
Potion small_healing_potion = new HealingPotion("Small Healing Potion", 10);
Potion medium_healing_potion = new HealingPotion("Medium Healing Potion", 25);
Potion large_healing_potion = new HealingPotion("Large Healing Potion", 50);
Potion elixir_of_life = new HealingPotion("Elixir of Life", 100);
Potion sinister_potion = new HealingPotion("Accursed Philtre", -1000000);

// shields
var buckler = new Shield("Buckler", 0.1);

// armour factory set up for armour creation

var leatherArmourFactory = new LeatherArmourFactory();
var crudeKnightsArmourFactory = new CrudeKnightsArmourFactory();
var circusAcrobatArmourFactory = new CircusAcrobatArmourFactory();

// creating concrete armour items for leather armour set
var leatherHelmet = (Item)leatherArmourFactory.CreateHeadArmour();
var leatherChestArmour = (Item)leatherArmourFactory.CreateTorsoArmour();
var leatherLegsArmour = (Item)leatherArmourFactory.CreateLegsArmour();

// creating concrete armour items for Crude Knight's armour set
var crudeKnightsHelmet = (Item)crudeKnightsArmourFactory.CreateHeadArmour();
var crudeKnightsTorsoArmour = (Item)crudeKnightsArmourFactory.CreateTorsoArmour();
var crudeKnightsLegsArmour = (Item)crudeKnightsArmourFactory.CreateLegsArmour();

// creating concrete armour items for Circus Acrobat armour set
var circusAcrobatHelmet = (Item)circusAcrobatArmourFactory.CreateHeadArmour();
var circusAcrobatTorsoArmour = (Item)circusAcrobatArmourFactory.CreateTorsoArmour();
var circusAcrobatLegsArmour = (Item)circusAcrobatArmourFactory.CreateLegsArmour();

// creating set up for leather armour set and its perk
var leatherArmourSet = new ArmourSet("Leather");
var increaseInventoryPerk = new IncreaseInventoryPerk(5);
leatherArmourSet.AddPerk(increaseInventoryPerk);
var armourSetManager = ArmourSetManager.GetInstance();
armourSetManager.RegisterSet(leatherArmourSet);

// creating set up for Crude Knigt's armour set and its perk
var CrudeKnightsArmourSet = new ArmourSet("CrudeKnights");
var increaseDefensePerk = new IncreaseDefensePerk(10);
CrudeKnightsArmourSet.AddPerk(increaseDefensePerk);
armourSetManager.RegisterSet(CrudeKnightsArmourSet);

// creating set up for Circus Acrobat armour set and its perk
var CircusAcrobatArmourSet = new ArmourSet("CircusAcrobat");
var increaseBlockingDamageChancePerk = new IncreaseBlockingDamageChancePerk(0.80);
CircusAcrobatArmourSet.AddPerk(increaseBlockingDamageChancePerk);
armourSetManager.RegisterSet(CircusAcrobatArmourSet);

// enemys
Enemy wild_boar = new Enemy("Wild Boar", 20, 5, 2, 0.1, 10);
Enemy tralalelo_tralala = new Enemy("Tralalelo Tralala", 10, 1, 1, 0.90, 10);
Enemy shriveled_slug = new Enemy("Shriveled Slug", 200, 3, 0, 0.01, 50);
Enemy nasty_beaver = new Enemy("Nasty Beaver", 10, 4, 1, 0.2, 50);
Enemy old_hermit = new Enemy("Old Hermit", 7, 2, 1, 0.3, 30);
Enemy caustic_horsefly = new Enemy("Caustic Horsefly", 15, 10, 1, 0.7, 45);
Enemy deranged_brigand = new Enemy("Deranged Brigand", 100, 14, 5, 0.8, 80);
Enemy spiked_beast = new Enemy("Spiked Beast", 80, 25, 5, 0.3, 130);
Enemy carnivorous_plant = new Enemy("Carnivorous Plant", 80, 2, 20, 0.01, 200);
Enemy broodmother_gargoyle = new Enemy("Broodmother Gargoyle", 200, 45, 22, 0.5, 1000);
Enemy ancient_atelaid_machine = new Enemy("Ancient Atelaid Machine", 150, 25, 5, 0.6, 200);
Enemy howling_barrel = new Enemy("Howling Barrel", 80, 3, 20, 0.01, 40);
Enemy hell_boar = new Enemy("Hell Boar", 50, 10, 1, 0.3, 80);
Enemy infernal_living_log = new Enemy("Infernal Living Log", 50, 2, 10, 0.04, 50);
Enemy weary_knight = new Enemy("Weary Knight", 300, 50, 15, 0.80, 250);
Enemy sanctified_remains = new Enemy("Sanctified Remains", 120, 20, 1, 0.80, 60);





// combat system init
CombatSystem.GetInstance().Initialize(InputManager,combatObserver,combatUnknownCommandObserver,gameCommandMoveObserver,gameHandlerObserver,inventoryObserver,unknownCommandObserver);

// puzzle sustem init
PuzzleSystem.GetInstance().Initialize(InputManager,puzzleObserver,unknownCommandPuzzleObserver,gameCommandMoveObserver,gameHandlerObserver,inventoryObserver,unknownCommandObserver);

// register puzzle with puzzle manager
var puzzleManager = PuzzleManager.GetInstance();
puzzleManager.RegisterPuzzle("sphynx", sphynxPuzzle);
puzzleManager.RegisterPuzzle("oedipus", oedipusPuzzle);
puzzleManager.RegisterPuzzle("persephone", persephonePuzzle);
puzzleManager.RegisterPuzzle("sisyphus", sisyphusPuzzle);


// rooms setup
Room room0 = new RoomBuilder(0, 0)
    .SetDescription("The first room")
    .AddItem(circusAcrobatHelmet)
    .Build();

Room room1 = new RoomBuilder(0, 1)
    .SetDescription("The second room")
    .AddItem(sinister_potion)
    .AddEnemy(broodmother_gargoyle)
    .Build();

Room room2 = new RoomBuilder(1, 0)
    .SetDescription("The third room")
    .AddItem(circusAcrobatTorsoArmour)
    .Build();

Room room3 = new RoomBuilder(2, 0)
    .SetDescription("The fourth room")
    .AddItem(circusAcrobatLegsArmour)
    .Build();

Room room4 = new RoomBuilder(0, -1)
    .SetDescription("The fifth room")
    .Build();

Room room5 = new RoomBuilder(-1, 0)
    .SetDescription("The sixth room")
    .AddItem(small_healing_potion)
    .Build();

Room room6 = new RoomBuilder(1, 1)
    .SetDescription("The seventh room")
    .AddItem(small_healing_potion)
    .Build();   


// adding rooms to the room checker
roomChecker.AddRoom(room0);
roomChecker.AddRoom(room1);
roomChecker.AddRoom(room2);
roomChecker.AddRoom(room3);
roomChecker.AddRoom(room4);
roomChecker.AddRoom(room5);
roomChecker.AddRoom(room6);



// begginning of the game
Console.WriteLine("Welcome to the game");
Console.WriteLine("LABYRINTH OF SUFFERING".Pastel("#3d2307"));
Console.WriteLine("Type:");
Console.WriteLine("Start".Pastel("#eb34c0") + " - to start a game");
Console.WriteLine("Exit".Pastel("#ff0000") + " - to exit a game");
Console.WriteLine("Statistics".Pastel("#03fcf8") + " - to show your game statistics");



// main game loop
while (!game.IsFinished)
{
    InputManager.ProcessInput();

        if (player.Health <= 0 && !game.IsFinished)
    {
        Console.WriteLine("");
        Console.WriteLine("▓▓▓▓GAME OVER▓▓▓▓".Pastel("#ff0000"));
        Console.WriteLine(" ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓".Pastel("#ff0000"));
        Console.WriteLine(" ▓▓▓▓▓▓  ▓▓▓ ▓▓▓".Pastel("#ff0000"));
        Console.WriteLine("  ▓▓▓▓   ▓▓   ▓ ".Pastel("#ff0000"));
        Console.WriteLine("   ▓▓     ▓     ".Pastel("#ff0000"));
        Console.WriteLine("   ▓            ".Pastel("#ff0000"));
        Console.WriteLine("You have been defeated...".Pastel("#ff0000"));
        Console.WriteLine("");
        game.IsFinished = true;
    }
}