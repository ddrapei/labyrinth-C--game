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
using Combat;


using Pastel;

// creates a player
Player player = Player.GetInstance();

// those are created as logic for commands
// this set up allows to add new movement without touching player
player.RegisterMoveBehavior("move up", new PlayerMoveUp());
player.RegisterMoveBehavior("move down", new PlayerMoveDown());
player.RegisterMoveBehavior("move left", new PlayerMoveLeft());
player.RegisterMoveBehavior("move right", new PlayerMoveRight());
player.RegisterMoveBehavior("move to previous position", new PlayerMoveToPreviousPosition());
// player.RegisterMoveBehavior("move up and right", new PlayerMoveDiagonallyUpAndRight()); - uncomment to test extandability of the movement commands


/* 

instruction how to add a new movement commands:

1. create a new class that is going to implement IMoveBehavior interface inside the PlayerMovement folder
2. create a new command class that is going to implement PlayerCommand inside interface the Commands.MoveCommands folder
3. register the behaviour with the player using RegisterMoveBehavior
4. add command to the GameCommandMoveObserver
5. register the new valid command with the UnknownCommandObserver

similar approach can be used to add new EquipmentBehaviors and LevelUp behaviors, corresponding 
Dictionaries and observers should be used

it is not a good idea to add new observers, since it will require changing exising code when observes are shuffled.
so in terms of existing states: before the game, during the game, inside inventory, during combat, during puzzle
existing observers should be used.
if a new state is required then a manager or a system should be created that is going to shuffle observers inside InputManager
for that new state: InputManager.AddObserver(newStateObserver), InputManager.RemoveObserver(oldStateObserver);

alternatively, observers shuffle can be handled inside the commands themselves.

if the state requires composite commands, then if else is used to filter input and display unknown command message.
if the state requires simple commands, then a new observer should be created to handle unknown commands like UnknownCommandObserver.

*/






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

// handles combat commands
var combatObserver = new CombatObserver(game);

// handles unknown commands during combat
var combatUnknownCommandObserver = new CombatUnknownCommandObserver(game);

// handles unknown commands
var unknownCommandObserver = new UnknownCommandObserver(game);

// puzzle observers
var puzzleObserver = new PuzzleObserver(game);


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
var openInventoryCommand = new OpenInventoryCommand(InputManager, insideInventoryObserver, gameCommandMoveObserver, gameHandlerObserver, inventoryObserver, unknownCommandObserver);

// creates commands for inside inventory (for when inventory is open)
var closeInventoryCommand = new CloseInventoryCommand(InputManager, insideInventoryObserver, gameCommandMoveObserver, gameHandlerObserver, inventoryObserver, unknownCommandObserver);

// display stats command
var displayStatsCommand = new DisplayStatsCommand(playerStatsDisplay);
var displayEquippedCommand = new DisplayEquippedCommand(playerEquippedDisplay);

// creates combat commands
var attackCommand = new AttackCommand(game, InputManager);
var runAwayCommand = new RunAwayCommand(InputManager);
var fightCommand = new FightCommand(game, InputManager);


/*
instructions to add a new puzzle:
1. create a new class that is going to implement IPuzzle interface inside the Puzzles folder
2. create a new puzzle instance inside Program.cs
3. Register the puzzle with the PuzzleManager
3. add the puzzle to a room
*/



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
combatObserver.AddCommand("a", attackCommand);
combatObserver.AddCommand("run", runAwayCommand);
combatObserver.AddCommand("r", runAwayCommand);
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
unknownCommandObserver.RegisterValidCommand("pick");
unknownCommandObserver.RegisterValidCommand("fight");
unknownCommandObserver.RegisterValidCommand("attack");
unknownCommandObserver.RegisterValidCommand("look around");
unknownCommandObserver.RegisterValidCommand("enter puzzle");
unknownCommandObserver.RegisterValidCommand("talk");
unknownCommandObserver.RegisterValidCommand("interact");
unknownCommandObserver.RegisterValidCommand("speak");
// unknownCommandObserver.RegisterValidCommand("move up and right"); - uncomment to test extendability of movement commands

// registers valid commands with the unknown command observer during combat
combatUnknownCommandObserver.RegisterValidCommand("attack");
combatUnknownCommandObserver.RegisterValidCommand("run");
combatUnknownCommandObserver.RegisterValidCommand("flee");
combatUnknownCommandObserver.RegisterValidCommand("escape");
combatUnknownCommandObserver.RegisterValidCommand("a");
combatUnknownCommandObserver.RegisterValidCommand("r");

// observers that are required to start the game
InputManager.AddObserver(mainMenuUnknownCommandObserver);
InputManager.AddObserver(mainMenuObserver);
// all other observers are added and removed depending on the state of the game

// weapons
Weapon spoon_with_a_hole = new Weapon("Spoon with a hole", 1);
Weapon rusty_shank = new Weapon("Rusty Shank", 2);
Weapon rusty_sword = new Weapon("Rusty Sword", 3);  
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
var buckler = new Shield("Buckler", 0.05);
var leather_shield = new Shield("Leather Shield of a Primitive Men", 0.07);
var centurion_shield = new Shield("Large shield of Centurion", 0.1);
var vociferant_shield = new Shield("The vociferant shield of the Demon", 0.12);
var smaugla_shield = new Shield("The dread-wrought shield of Smaugla", 0.15);


/*
how to add new armour:
1. Create a new folder for a new armour set inside Items.Armour
2. Create concrete armour classes that implement corresponding interfaces inside that folder
3. Create a new factory class that implements IArmourFactory interface inside that folder
4. Create concrete armour factory in Program.cs
5. Create concrete armour items using the factory in Program.cs

optional, add a new perk for the set:
6. Create a new file and class that implements IPerk interface inside Perks folder
7. Create an armour set instance inside Program.cs
var newArmourSet = new ArmourSet("SetName");
8. Create an instance of the perk and add it to the set
var newPerk = new NewPerk(parameters);
newArmourSet.AddPerk(newPerk);
9. Register the set with ArmourSetManager
armourSetManager.RegisterSet(newArmourSet);
10. add concrete armour items to the rooms 

*/


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

// creating armour set manager that is going to register sets.
var armourSetManager = ArmourSetManager.GetInstance();

// creating set up for leather armour set and its perk
var leatherArmourSet = new ArmourSet("Leather");
var increaseInventoryPerk = new IncreaseInventoryPerk(5);
leatherArmourSet.AddPerk(increaseInventoryPerk);
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

/*
I acknowledge use of [Gemini] from [https://gemini.google.com/share/f4c2cd895f79]
to [generate names of the enemies, translating from russian]. I entered the prompts
on [November 18, 2025 at 10:27 AM] and [it was used to name the enemies].

I acknowledge use of [Gemini] from [https://gemini.google.com/share/44cf7a03c651] to 
[generate names of the enemies, translating from russian]. I entered the prompts on
[November 18, 2025 at 10:27 AM] and [it was used to name the enemies].
*/

// enemys
Enemy wild_boar = new Enemy("Wild Boar", 20, 5, 0, 0.1, 10);
Enemy tralalelo_tralala = new Enemy("Tralalelo Tralala", 10, 1, 0, 0.90, 10);
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
PuzzleSystem.GetInstance().Initialize(InputManager,puzzleObserver,gameCommandMoveObserver,gameHandlerObserver,inventoryObserver,unknownCommandObserver);

// register puzzle with puzzle manager
var puzzleManager = PuzzleManager.GetInstance();
puzzleManager.RegisterPuzzle("sphynx", sphynxPuzzle);
puzzleManager.RegisterPuzzle("oedipus", oedipusPuzzle);
puzzleManager.RegisterPuzzle("persephone", persephonePuzzle);
puzzleManager.RegisterPuzzle("sisyphus", sisyphusPuzzle);


/*
I acknowledge use of [Gemini] from [https://gemini.google.com/share/d50cfcdb6f67] to
[generate room descriptions]. I entered the following prompts on [November 22, 2025 at 07:24 PM]:
[ Don't generate the code. Just create a description of the room based on what is in the room. One sentence. Make it similar to dark souls and fear and hunger.] and
[it was used as a room description].
*/

/*
To add new room:
1. create a new room using RoomBuilder with what you want inside
2. registering the room with RoomChecker
*/

// rooms setup
Room room0 = new RoomBuilder(0, 0)
    .SetDescription("Stagnant air hangs heavy in this silent stone chamber, offering a peace that feels dangerously like decay. A rough table sits in the gloom, bearing a place setting that seems to mock the very concept of sustenance.")
    .AddItem(spoon_with_a_hole)
    .Build();

Room room1 = new RoomBuilder(0, 1)
    .SetDescription("The stench of musk fills the air, accompanied by the restless scraping of hooves. A battered shield lies discarded in the filth.")
    .AddItem(leather_shield)
    .AddEnemy(wild_boar)
    .Build();

Room room2 = new RoomBuilder(1, 0)
    .SetDescription("The sound of wet, grinding teeth fills this damp hollow, where a discarded helm lies amongst the splintered remains of previous intruders.")
    .AddItem(leatherHelmet)
    .AddEnemy(nasty_beaver)
    .Build();

Room room3 = new RoomBuilder(2, 0)
    .SetDescription("An uncorked vial sits forgotten in the gloom, offering a salvation that came too late for its previous owner.")
    .AddItem(open_healing_potion)
    .Build();

Room room4 = new RoomBuilder(0, -1)
    .SetDescription("A shriveled, writhing horror guards a discarded leather jerkin, leaving a trail of dry rot across the floor.")
    .AddItem(leatherChestArmour)
    .AddEnemy(shriveled_slug)
    .Build();

Room room5 = new RoomBuilder(-1, 0)
    .SetDescription("A modest vial of crimson fluid sits amidst the suffocating silence, offering a fleeting delay to the inevitable.")
    .AddItem(small_healing_potion)
    .Build();

Room room6 = new RoomBuilder(1, 1)
    .SetDescription("The seventh room")
    .AddItem(rusty_shank)
    .AddEnemy(deranged_brigand)
    .Build();

Room room7 = new RoomBuilder(1, 2)
    .SetDescription("A manic, humming figure dances disturbingly in the shadows near a set of discarded leather leggings.")
    .AddItem(leatherLegsArmour)
    .AddEnemy(tralalelo_tralala)
    .Build();
    
Room room8 = new RoomBuilder(0, 2)
    .SetDescription("A jagged, corroded blade lies in the dust, whispering of a violent struggle fueled by utter desperation.")
    .AddItem(rusty_shank)
    .Build();

Room room9 = new RoomBuilder(0, 3)
    .SetDescription("A withered recluse cowers in the shadows, clutching an unstoppered vial with jealous madness.")
    .AddItem(open_healing_potion)
    .AddEnemy(old_hermit)
    .Build();

Room room10 = new RoomBuilder(1, 3)
    .SetDescription("The air vibrates with a sickening drone as a bloated insect drips searing ichor onto the melting stones.")
    .AddEnemy(caustic_horsefly)
    .Build();

Room room11 = new RoomBuilder(2, 3)
    .SetDescription("A heavy blade lies forgotten in the filth, its edge consumed by the slow, red cancer of rust.")
    .AddItem(rusty_sword)
    .Build();  

Room room12 = new RoomBuilder(3, 3)
    .SetDescription("A voracious flora constricts a crude helm, flourishing beneath the silent judgment of a stone sphinx.")
    .AddPuzzle(sphynxPuzzle)
    .AddItem(crudeKnightsHelmet)
    .AddEnemy(carnivorous_plant)
    .Build();

Room room13 = new RoomBuilder(-2, 0)
    .SetDescription("A rampant beast guards a flask of vital fluid, pacing restlessly before a tragic riddle carved in stone.")
    .AddPuzzle(oedipusPuzzle)
    .AddItem(medium_healing_potion)
    .AddEnemy(wild_boar)
    .Build();

Room room14 = new RoomBuilder(-2, 1)
    .SetDescription("Under the silent judgment of a stone sphinx, a desecrated saint in tattered vestments rises from the dust to guard a solitary flask of crimson vitality.")
    .AddPuzzle(sphynxPuzzle)
    .AddItem(large_healing_potion)
    .AddEnemy(sanctified_remains)
    .Build();  

Room room15 = new RoomBuilder(-2, 2)
    .SetDescription("A feral monstrosity bristling with jagged spines stalks the shadows, circling the rusted, discarded husk of a crude iron breastplate.")
    .AddItem(crudeKnightsTorsoArmour)
    .AddEnemy(spiked_beast)
    .Build();

Room room16 = new RoomBuilder(-1, 2)
    .SetDescription("In the oppressive silence, a weathered centurion's shield leans against the rot-slicked masonry, a silent monument to a forgotten legion's demise.")
    .AddItem(centurion_shield)
    .Build();

Room room17 = new RoomBuilder(-1, 3)
    .SetDescription("Beneath a cryptic altar to the stolen goddess of spring, a pair of crude, heavy greaves lies abandoned in the damp, suffocating gloom.")
    .AddItem(crudeKnightsLegsArmour)
    .AddPuzzle(persephonePuzzle)
    .Build();

Room room18 = new RoomBuilder(-1, 4)
    .SetDescription("A rusting Atelaid construct, grinding with the heavy weight of a forgotten age, looms over a flask of crimson respite in the oil-stained dark.")
    .AddItem(large_healing_potion)
    .AddEnemy(ancient_atelaid_machine)
    .Build();

Room room19 = new RoomBuilder(1, 4)
    .SetDescription("A frenzied boar paws at the gore-slicked stones, guarding a grotesque shield that appears to be frozen in an eternal, deafening shriek.")
    .AddItem(vociferant_shield)
    .AddEnemy(wild_boar)
    .Build();

Room room20 = new RoomBuilder(2, 4)
    .SetDescription("A withered hermit mutters madly in the filth, jealously guarding the peeling, painted grin of a forgotten acrobat's helm.")
    .AddItem(circusAcrobatHelmet)
    .AddEnemy(old_hermit)
    .Build();

Room room21 = new RoomBuilder(3, 4)
    .SetDescription("Amidst a slurry of wet rot and splintered wood, a bloated, vicious rodent bares yellowed incisors over a vial of mending fluid.")
    .AddItem(medium_healing_potion)
    .AddEnemy(nasty_beaver)
    .Build();

Room room22 = new RoomBuilder(4, 4)
    .SetDescription("Bowed by the weight of rusted plate and endless fatigue, a broken sentinel leans upon his sword, silently guarding a vial that pulses with the impossible light of eternal life.")
    .AddItem(elixir_of_life)
    .AddEnemy(weary_knight)
    .Build();
    
Room room23 = new RoomBuilder(4, 5)
    .SetDescription("A charred, living timber crackles with infernal malice, shedding dying embers upon the gaudy, torn vestments of a forgotten acrobat.")
    .AddItem(circusAcrobatTorsoArmour)
    .AddEnemy(infernal_living_log)
    .Build();

Room room24 = new RoomBuilder(0, -2)
    .SetDescription("A vicious, sludge-slicked rodent bares its yellowed teeth in the gloom, jealously guarding a flask of restorative amber.")
    .AddItem(medium_healing_potion)
    .AddEnemy(nasty_beaver)
    .Build();

Room room25 = new RoomBuilder(0, -3)
    .SetDescription("A vicious, sludge-slicked rodent bares its yellowed teeth in the gloom, jealously guarding a flask of restorative amber.")
    .AddItem(steel_axe)
    .AddPuzzle(oedipusPuzzle)
    .Build();

Room room26 = new RoomBuilder(1, -3)
    .SetDescription("In the deafening silence of the crypt, a solitary vessel pulses with a blinding, golden heartbeat, mocking the surrounding rot with the forbidden essence of eternity.")
    .AddItem(elixir_of_life)
    .Build();

Room room27 = new RoomBuilder(2, -3)
    .SetDescription("The stagnant air vibrates with a sickening drone as a bloated, insectoid horror drips sizzling bile onto the ancient stones.")
    .AddEnemy(caustic_horsefly)
    .Build();

Room room28 = new RoomBuilder(3, -3)
    .SetDescription("An innocuous wooden cask violently vibrates with the shrieks of the damned, guarding a heavy shield forged from the scales of an avaricious drake.")
    .AddEnemy(howling_barrel)
    .AddItem(smaugla_shield)
    .Build();

Room room29 = new RoomBuilder(4, -3)
    .SetDescription("Abandoned in the breathless dark, a rusted sickle lies upon the stones, a silent crescent moon yearning for a harvest of blood.")
    .AddItem(hebron_sickle)
    .Build();

Room room30 = new RoomBuilder(5, -3)
    .SetDescription("In the shadow of a trial dedicated to endless, futile toil, a desiccated slug writhes blindly in the dust, guarding a pulsing vial of eternity it is too wretched to consume.")
    .AddItem(elixir_of_life)
    .AddEnemy(shriveled_slug)
    .AddPuzzle(sisyphusPuzzle)
    .Build();

Room room31 = new RoomBuilder(5, -2)
    .SetDescription("A broken sentinel, his spirit eroding faster than his rusted plate, stands a ragged vigil over a blade said to have once shattered the walls of a city.")
    .AddItem(jericho_sword)
    .AddEnemy(weary_knight)
    .Build();

Room room32 = new RoomBuilder(0, 4)
    .SetDescription("In the humid, choking dark, a ravenous floral monstrosity drips digestive ichor upon the rusted shaft of a forgotten legionnaire's spear.")
    .AddItem(roman_pilum)
    .AddEnemy(carnivorous_plant)
    .Build();

Room room33 = new RoomBuilder(0, 5)
    .SetDescription("A heavy flask of crimson vitality rests undisturbed in the suffocating silence, offering a singular, suspicious mercy amidst the surrounding rot.")
    .AddItem(large_healing_potion)
    .Build();

Room room34 = new RoomBuilder(0, 6)
    .SetDescription("A flask of viscous, swirling gloom rests upon the cold stones, radiating a palpable malice that suggests this draught offers not salvation, but ruin.")
    .AddItem(sinister_potion)
    .Build();  

Room room35 = new RoomBuilder(1, 6)
    .SetDescription("The colossal, curved blade of a foreign executioner rests in the dust, a silent monument to the brutal justice of a distant, sand-swept empire.")
    .AddItem(headsman_of_memphis)
    .Build();

Room room36 = new RoomBuilder(0, -4)
    .SetDescription("A frothing beast, mad with hunger, paws the cold stone, oblivious to the vial of golden immortality pulsing softly in the muck beneath its hooves.")
    .AddItem(elixir_of_life)
    .AddEnemy(wild_boar)
    .Build();

Room room37 = new RoomBuilder(1, -4)
    .SetDescription("A bloated insectoid horror drips searing bile onto the stones, circling a wicked, curved blade that radiates a palpable, cold hatred.")
    .AddItem(malevolent_falchion)
    .AddEnemy(caustic_horsefly)
    .Build();

Room room38 = new RoomBuilder(2, -4)
    .SetDescription("The most dangerous room of that labyrinth.")
    .AddItem(elixir_of_life)
    .AddEnemy(broodmother_gargoyle)
    .Build();


// adding rooms to the room checker
roomChecker.AddRoom(room0);
roomChecker.AddRoom(room1);
roomChecker.AddRoom(room2);
roomChecker.AddRoom(room3);
roomChecker.AddRoom(room4);
roomChecker.AddRoom(room5);
roomChecker.AddRoom(room6);
roomChecker.AddRoom(room7);
roomChecker.AddRoom(room8);
roomChecker.AddRoom(room9);
roomChecker.AddRoom(room10);
roomChecker.AddRoom(room11);
roomChecker.AddRoom(room12);
roomChecker.AddRoom(room13);
roomChecker.AddRoom(room14);
roomChecker.AddRoom(room15);
roomChecker.AddRoom(room16);
roomChecker.AddRoom(room17);
roomChecker.AddRoom(room18);
roomChecker.AddRoom(room19);
roomChecker.AddRoom(room20);
roomChecker.AddRoom(room21);
roomChecker.AddRoom(room22);
roomChecker.AddRoom(room23);
roomChecker.AddRoom(room24);
roomChecker.AddRoom(room24);
roomChecker.AddRoom(room25);
roomChecker.AddRoom(room26);
roomChecker.AddRoom(room27);
roomChecker.AddRoom(room28);
roomChecker.AddRoom(room29);
roomChecker.AddRoom(room30);
roomChecker.AddRoom(room31);
roomChecker.AddRoom(room32);
roomChecker.AddRoom(room33);
roomChecker.AddRoom(room34);
roomChecker.AddRoom(room35);
roomChecker.AddRoom(room36);
roomChecker.AddRoom(room37);
roomChecker.AddRoom(room38);





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
        GameStatistics.AddDeath();
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