using Observers;
using Observers.MainMenuObservers;
using Observers.InsideInventoryObservers;
using Commands;
using Commands.MainMenuCommands;
using Commands.MoveCommands;
using Commands.InventoryCommands;
using Items;
using Items.Armour;
using Perks;

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

// handles unknown commands
var unknownCommandObserver = new UnknownCommandObserver(game);

// command to start the game
var startGameCommand = new StartGameCommand(game, InputManager, mainMenuObserver, mainMenuUnknownCommandObserver, gameCommandMoveObserver, gameHandlerObserver, inventoryObserver ,unknownCommandObserver);

// command to exit the game
var exitGameCommand = new ExitGameCommand(game);

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

// registers start game command with its observer 
mainMenuObserver.AddCommand("start", startGameCommand);
mainMenuObserver.AddCommand("start game", startGameCommand);
mainMenuObserver.AddCommand("exit", exitGameCommand);


// commands to exit the game
gameHandlerObserver.AddCommand("exit", exitGameCommand);
gameHandlerObserver.AddCommand("exit game", exitGameCommand);
gameHandlerObserver.AddCommand("finish", exitGameCommand);


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
unknownCommandObserver.RegisterValidCommand("inventory");
unknownCommandObserver.RegisterValidCommand("inv");
unknownCommandObserver.RegisterValidCommand("i");
unknownCommandObserver.RegisterValidCommand("pick up");

// registers valid commands with the unknown command observer in the inventory
insideInventoryUnknownCommandObserver.RegisterValidCommand("equip");
insideInventoryUnknownCommandObserver.RegisterValidCommand("drop");
insideInventoryUnknownCommandObserver.RegisterValidCommand("close");

// observers that are required to start the game
InputManager.AddObserver(mainMenuUnknownCommandObserver);
InputManager.AddObserver(mainMenuObserver);
// all other observers are added and removed depending on the state of the game

// weapons
var spoon_with_a_hole = new Weapon("Spoon with a hole", 3);

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

// creating concrete armour items
var leatherHelmet = (Item)leatherArmourFactory.CreateHeadArmour();
var leatherChestArmour = (Item)leatherArmourFactory.CreateTorsoArmour();
var leatherLegsArmour = (Item)leatherArmourFactory.CreateLegsArmour();

// creating set up for armour set and subsequent perk
var leatherArmourSet = new ArmourSet("Leather");
var increaseInventoryPerk = new IncreaseInventoryPerk(5);
leatherArmourSet.AddPerk(increaseInventoryPerk);
var armourSetManager = ArmourSetManager.GetInstance();
armourSetManager.RegisterSet(leatherArmourSet);

// creating room builder
RoomBuilder builder = new RoomBuilder(0, 0);

// rooms setup
Room room0 = builder
    .SetDescription("The first room")
    .AddItem(buckler)
    .Build();

Room room1 = new RoomBuilder(0, 1)
    .SetDescription("The second room")
    .AddItem(spoon_with_a_hole)
    .Build();

Room room2 = new RoomBuilder(1, 0)
    .SetDescription("The third room")
    .AddItem(leatherHelmet)
    .Build();

Room room3 = new RoomBuilder(2, 0)
    .SetDescription("The fourth room")
    .AddItem(leatherLegsArmour)
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