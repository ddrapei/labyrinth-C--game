using System.Data.Common;
using Items;
using Items.Armour;
using Items.Potions;
using Rooms;
using PlayerMovement;
using PlayerEquipment;


using Pastel;
// Singleton player class that represents the only one player in the game

public class Player
{
    private static Player? instance = null;

    public static Player GetInstance()
    {
        {
            if (instance == null)
            {
                instance = new Player(100, 0, 0);
            }
            return instance;
        }
    }

    private int health;
    private int xcoordinate;
    private int ycoordinate;
    private int previousXcoordinate;
    private int previousYcoordinate;
    private int defense;
    private int baseAttackPower;
    private int attackPower;
    private double blockingDamageChance;
    private int experience;
    private Weapon? weaponEquipped;
    private Shield? shieldEquipped;
    private IHeadArmour? headArmourEquipped;
    private ITorsoArmour? torsoArmourEquipped;
    private ILegsArmour? legsArmourEquipped;
    private Inventory? inventory;

    // Composition: Movement behaviors
    private Dictionary<string, IMoveBehavior> movementBehaviors;
    private ITrackPosition storePreviousPosition;

    // Composition: Equipment behavious
    private Dictionary<string, PlayerEquipment.IEquipBehavior> equipmentBehaviors;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Xcoordinate
    {
        get { return xcoordinate; }
        set { xcoordinate = value; }
    }

    public int PreviousXcoordinate
    {
        get { return previousXcoordinate; }
        set { previousXcoordinate = value; }
    }
    public int PreviousYcoordinate
    {
        get { return previousYcoordinate; }
        set { previousYcoordinate = value; }
    }

    public int Ycoordinate
    {
        get { return ycoordinate; }
        set { ycoordinate = value; }
    }
    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }
    public int BaseAttackPower
    {
        get { return baseAttackPower; }
        set { baseAttackPower = value; }
    }
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    public double BlockingDamageChance
    {
        get { return blockingDamageChance; }
        set { blockingDamageChance = value; }
    }
    public int Experience
    {
        get { return experience; }
        set { experience = value; }
    }
    public Weapon? WeaponEquiped
    {
        get { return weaponEquipped; }
        set { weaponEquipped = value; }
    }
    public Shield? ShieldEquipped
    {
        get { return shieldEquipped; }
        set { shieldEquipped = value; }
    }
    public IHeadArmour? HeadArmourEquipped
    {
        get { return headArmourEquipped; }
        set { headArmourEquipped = value; }
    }
    public ITorsoArmour? TorsoArmourEquipped
    {
        get { return torsoArmourEquipped; }
        set { torsoArmourEquipped = value; }
    }
    public ILegsArmour? LegsArmourEquipped
    {
        get { return legsArmourEquipped; }
        set { legsArmourEquipped = value; }
    }
    public Inventory Inventory
    {
        get { return inventory!; }
    }
    public Dictionary<string, IMoveBehavior> MovementBehaviors
    {
        get { return movementBehaviors; }
        set { movementBehaviors = value; }
    }

    public ITrackPosition StorePreviousPosition
    {
        get { return storePreviousPosition; }
        set { storePreviousPosition = value; }
    }

    public Dictionary<string, PlayerEquipment.IEquipBehavior> EquipmentBehaviors
    {
        get { return equipmentBehaviors; }
        set { equipmentBehaviors = value; }
    }


    // --- Constructor ---
    private Player(int health, int xcoordinate, int ycoordinate)
    {
        this.health = health;
        this.xcoordinate = xcoordinate;
        this.ycoordinate = ycoordinate;
        this.previousXcoordinate = xcoordinate;
        this.previousYcoordinate = ycoordinate;
        this.defense = 0;
        this.baseAttackPower = 1;
        this.attackPower = 1;
        this.blockingDamageChance = 0.00;
        this.experience = 0;
        this.weaponEquipped = null;
        this.shieldEquipped = null;
        this.headArmourEquipped = null;
        this.torsoArmourEquipped = null;
        this.legsArmourEquipped = null;
        this.inventory = new Inventory(10);

        // initialise movements behavior
        this.movementBehaviors = new Dictionary<string, IMoveBehavior>();
        this.storePreviousPosition = new StorePreviousPosition();

        // initialise equipent behavious
        this.equipmentBehaviors = new Dictionary<string, IEquipBehavior>();
    }

    public void RegisterMoveBehavior(string commandKey, IMoveBehavior behavior)
    {
        if (!movementBehaviors.ContainsKey(commandKey))
        {
            movementBehaviors.Add(commandKey, behavior);
        }
    }

    public void RegisterEquipBehavior(string itemType, IEquipBehavior behavior)
    {
        if (!equipmentBehaviors.ContainsKey(itemType))
        {
            equipmentBehaviors.Add(itemType, behavior);
        }
    }

    // method to move to previous position (when running away)
    public bool MoveToPreviousPosition()
    {
        if (RoomChecker.GetInstance().doesRoomExist(previousXcoordinate, previousYcoordinate))
        {
            xcoordinate = previousXcoordinate;
            ycoordinate = previousYcoordinate;
            return true;
        }
        return false;
    }
    public void LookAround()
    {
        RoomChecker.GetInstance().DisplayCurrentRoom(this);
    }

    public void ResetPlayerLocation()
    {
        this.xcoordinate = 0;
        this.ycoordinate = 0;
        this.previousXcoordinate = 0;
        this.previousYcoordinate = 0;
    }
}