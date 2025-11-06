using Observers;
using Commands;
using Items;
using Items.Armour;

// creates a player
Player player = Player.GetInstance();

// creates a game
var game = new Game();

// creates a room checker
var roomChecker = RoomChecker.GetInstance();

// creates an input manager
var InputManager = new InputManager();

// creates observers for the game
// handles start and finish the game
var gameHandlerObserver = new GameHandlerObserver(game);

// handles player movement commands
var gameCommandMoveObserver = new GameCommandMoveObserver(game);

// handles inventory commands (when inventory is closed)
var inventoryObserver = new InventoryObserver(game);

// handles commands inside inventory (when inventory is open)
var insideInventoryObserver = new InsideInventoryObserver(game);

// handles unknown commands
var unknownCommandObserver = new UnknownCommandObserver(game);

// creates commands for player movement
var moveUp = new MoveUpCommand(player);
var moveDown = new MoveDownCommand(player);
var moveLeft = new MoveLeftCommand(player);
var moveRight = new MoveRightCommand(player);

// creates inventory commands (for when inventory is closed)
var pickUpItemCommand = new PickUpItemCommand();
var openInventoryCommand = new OpenInventoryCommand(InputManager, insideInventoryObserver);

// creates commands for inside inventory (for when inventory is open)
var closeInventoryCommand = new CloseInventoryCommand(InputManager, insideInventoryObserver);

// registers movement commands
gameCommandMoveObserver.AddCommand("move up", moveUp);
gameCommandMoveObserver.AddCommand("move down", moveDown);
gameCommandMoveObserver.AddCommand("move left", moveLeft);
gameCommandMoveObserver.AddCommand("move right", moveRight);

// registers inventory commands (work when inventory is closed)
inventoryObserver.AddCommand("pick up", pickUpItemCommand);
inventoryObserver.AddCommand("inventory", openInventoryCommand);

// registers inside inventory commands (work when inventory is open)
insideInventoryObserver.AddCommand("close", closeInventoryCommand);
insideInventoryObserver.AddCommand("exit", closeInventoryCommand);

// registers valid commands with the unknown command observer
unknownCommandObserver.RegisterValidCommand("start");
unknownCommandObserver.RegisterValidCommand("exit");
unknownCommandObserver.RegisterValidCommand("move up");
unknownCommandObserver.RegisterValidCommand("move down");
unknownCommandObserver.RegisterValidCommand("move left");
unknownCommandObserver.RegisterValidCommand("move right");
unknownCommandObserver.RegisterValidCommand("inventory");
unknownCommandObserver.RegisterValidCommand("pick up");

// adds observers to the input manager
InputManager.AddObserver(gameHandlerObserver);
InputManager.AddObserver(gameCommandMoveObserver);
InputManager.AddObserver(inventoryObserver);
InputManager.AddObserver(unknownCommandObserver);
// insideInventoryObserver is added/removed dynamically inside OpenInventoryCommand and CloseInventoryCommand

// weapons
var spoon_with_a_hole = new Weapon("Spoon with a hole", 3);

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

// creating room builder
RoomBuilder builder = new RoomBuilder(0, 0);

// rooms setup
Room room0 = builder
    .SetDescription("The first room")
    .AddItem(leatherHelmet)
    .Build();

Room room1 = new RoomBuilder(0, 1)
    .SetDescription("The second room")
    .AddItem(leatherChestArmour)
    .Build();

Room room2 = new RoomBuilder(1, 0)
    .SetDescription("The third room")
    .AddItem(leatherLegsArmour)
    .Build();

Room room3 = new RoomBuilder(2, 0)
    .SetDescription("The fourth room")
    .Build();

// adding rooms to the room checker
roomChecker.AddRoom(room0);
roomChecker.AddRoom(room1);
roomChecker.AddRoom(room2);
roomChecker.AddRoom(room3);

// main game loop
while (!game.IsFinished)
{
    InputManager.ProcessInput();
}