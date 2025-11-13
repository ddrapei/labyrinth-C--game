using Observers;
using Observers.MainMenuObservers;
using Observers.InsideInventoryObservers;
using Observers.CombatObservers;
using Commands;
using Commands.DisplayCommands;
using Commands.MainMenuCommands;
using Commands.MoveCommands;
using Commands.InventoryCommands;
using Commands.CombatCommands;
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

using Pastel;

// creates a player
Player player = Player.GetInstance();

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

// command to start the game
var startGameCommand = new StartGameCommand(game, InputManager, mainMenuObserver, mainMenuUnknownCommandObserver, gameCommandMoveObserver, gameHandlerObserver, inventoryObserver ,unknownCommandObserver);

// command to exit the game
var exitGameCommand = new ExitGameCommand(game);

// display commands
var playerStatsDisplay = new PlayerStatsDisplay();
var playerEquippedDisplay = new PlayerEquippedDisplay();
var lookAround = new LookAroundCommand();


// creates commands for player movement
var moveDown = new MoveDownCommand(player);
var moveUp = new MoveUpCommand(player);
var moveLeft = new MoveLeftCommand(player);
var moveRight = new MoveRightCommand(player);

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

// registers start game command with its observer 
mainMenuObserver.AddCommand("start", startGameCommand);
mainMenuObserver.AddCommand("start game", startGameCommand);
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

// registers movement commands
gameCommandMoveObserver.AddCommand("move up", moveUp);
gameCommandMoveObserver.AddCommand("move down", moveDown);
gameCommandMoveObserver.AddCommand("move left", moveLeft);
gameCommandMoveObserver.AddCommand("move right", moveRight);

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
var spoon_with_a_hole = new Weapon("Spoon with a hole", 3);
Weapon rusty_sword = new Weapon("Rusty Sword", 7);  

// potions
var small_healing_potion = new HealingPotion("Small Healing Potion", 10);

// shields
var buckler = new Shield("Buckler", 0.1);

// armour factory set up for armour creation
void CreateArmour(ArmourFactory factory)
{
    var headarmour = factory.CreateHeadArmour();
    var torsoarmour = factory.CreateTorsoArmour();
    var legsarmour = factory.CreateLegsArmour();
}

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

CombatSystem.GetInstance().Initialize(InputManager,combatObserver,combatUnknownCommandObserver,gameCommandMoveObserver,gameHandlerObserver,inventoryObserver,unknownCommandObserver
);

// creating room builder
RoomBuilder builder = new RoomBuilder(0, 0);

// rooms setup
Room room0 = builder
    .SetDescription("The first room")
    .AddItem(rusty_sword)
    .Build();

Room room1 = new RoomBuilder(0, 1)
    .SetDescription("The second room")
    .AddItem(spoon_with_a_hole)
    .AddEnemy(wild_boar)
    .Build();

Room room2 = new RoomBuilder(1, 0)
    .SetDescription("The third room")
    .AddItem(circusAcrobatTorsoArmour)
    .Build();

Room room3 = new RoomBuilder(2, 0)
    .SetDescription("The fourth room")
    .AddItem(circusAcrobatLegsArmour)
    .Build();

// adding rooms to the room checker
roomChecker.AddRoom(room0);
roomChecker.AddRoom(room1);
roomChecker.AddRoom(room2);
roomChecker.AddRoom(room3);

// begginning of the game
Console.WriteLine("Welcome to the game");
Console.WriteLine("LABYRINTH OF SUFFERING".Pastel("#3d2307"));
Console.WriteLine("Type:");
Console.WriteLine("Start".Pastel("#eb34c0") + " - to start a game");
Console.WriteLine("Exit".Pastel("#ff0000") + " - to exit a game");


// main game loop
while (!game.IsFinished)
{
    InputManager.ProcessInput();
}