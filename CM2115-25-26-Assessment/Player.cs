using System.Data.Common;
using Items;
using Items.Armour;
using Items.Potions;
using Rooms;
using PlayerMovement;

using Pastel;
// Singleton player class that represents the only one player in the game

public class Player : IMoveBehavior
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
    private IMoveBehavior moveUpBehavior;
    private IMoveBehavior moveDownBehavior;
    private IMoveBehavior moveLeftBehavior;
    private IMoveBehavior moveRightBehavior;

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
        get { return inventory; }
    }

    public IMoveBehavior MoveUpBehavior
    {
        get { return moveUpBehavior; }
        set { moveUpBehavior = value; }
    }

    public IMoveBehavior MoveDownBehavior
    {
        get { return moveDownBehavior; }
        set { moveDownBehavior = value; }
    }

    public IMoveBehavior MoveLeftBehavior
    {
        get { return moveLeftBehavior; }
        set { moveLeftBehavior = value; }
    }

    public IMoveBehavior MoveRightBehavior
    {
        get { return moveRightBehavior; }
        set { moveRightBehavior = value; }
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
    }


    public void Move(Player player)
    {
        // default move method for composition
    }

    // method to store previous position (for running away from combat)
    public void StorePreviousPosition()
    {
        this.PreviousXcoordinate = xcoordinate;
        this.PreviousYcoordinate = ycoordinate;
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

    // method to equip weapon
    public bool EquipWeapon(Weapon weapon)
    {
        if (this.weaponEquipped != null)
        {
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(this);
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = this.weaponEquipped;
                Console.WriteLine("You placed the item in the room " + this.WeaponEquiped?.Name.Pastel("#ff9d00") + " and equipped " + weapon.Name.Pastel("#ff9d00"));
                return true;
            }
            else
            {
                Console.WriteLine("Cannot equip the weapon");
                return false;
            }
        }
        else
        {
            this.weaponEquipped = weapon;
            this.attackPower = weapon.Damage;
            this.baseAttackPower = weapon.Damage;
            Console.WriteLine("Your damage now is: " + this.AttackPower.ToString().Pastel("#ff0000"));
            return true;
        }
    }

    // method to equip shield
    public bool EquipShield(Shield shield)
    {
        if (this.shieldEquipped != null)
        {
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(this);
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = this.shieldEquipped;
                Console.WriteLine("You placed the item in the room " + this.shieldEquipped.Name.Pastel("#00e5ff") + " and equipped " + shieldEquipped.Name.Pastel("#00e5ff"));
                return true;
            }
            else
            {
                Console.WriteLine("Cannot equip the shield");
                return false;
            }
        }
        else
        {
            this.shieldEquipped = shield;
            this.blockingDamageChance = shield.BlockingDamageChance;
            Console.WriteLine("Your blocking change now is: " + this.BlockingDamageChance.ToString().Pastel("#00e5ff"));
            return true;
        }
    }

    // method to equip head armour
    public bool EquipHeadArmour(IHeadArmour headArmour)
    {

        IHeadArmour? oldArmour = this.headArmourEquipped;

        // checks if something is equipped
        if (oldArmour != null)
        {
            // takes current room
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(this);

            // verifies that the room exist and there is currently no item in the room
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = (Item)oldArmour;
                this.defense -= ((Armour)oldArmour).Defense;
                Console.WriteLine("You placed your " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
            }
            else
            {
                Console.WriteLine("Cannot equip " + ((Item)headArmour).Name.Pastel("#ff9d00") + " - no space to place your current armour " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + ((Item)headArmour).Name.Pastel("#ff9d00"));
        }

        // equips armour if the checks are successful
        this.headArmourEquipped = headArmour;
        this.defense += ((Armour)headArmour).Defense;
        Console.WriteLine("Your defense now is: " + this.Defense.ToString().Pastel("#1900ff"));
        ArmourSetManager.GetInstance().CheckAndUpdateSetBonuses(this);
        return true;
    }

    // method to equip torso armour
    public bool EquipTorsoArmour(ITorsoArmour torsoArmour)
    {
        ITorsoArmour? oldArmour = this.torsoArmourEquipped;

        if (oldArmour != null)
        {
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(this);
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = (Item)oldArmour;
                this.defense -= ((Armour)oldArmour).Defense;
                Console.WriteLine("You placed your " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
            }
            else
            {
                Console.WriteLine("Cannot equip " + ((Item)torsoArmour).Name.Pastel("#ff9d00") + " - no space to place your current armour " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + ((Item)torsoArmour).Name.Pastel("#ff9d00"));
        }

        this.torsoArmourEquipped = torsoArmour;
        this.defense += ((Armour)torsoArmour).Defense;
        Console.WriteLine("Your defense now is: " + this.Defense.ToString().Pastel("#1900ff"));
        ArmourSetManager.GetInstance().CheckAndUpdateSetBonuses(this);
        return true;
    }

    // method to equip legs armour
    public bool EquipLegsArmour(ILegsArmour legsArmour)
    {
        ILegsArmour? oldArmour = this.legsArmourEquipped;

        if (oldArmour != null)
        {
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(this);
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = (Item)oldArmour;
                this.defense -= ((Armour)oldArmour).Defense;
                Console.WriteLine("You placed your " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
            }
            else
            {
                Console.WriteLine("Cannot equip " + ((Item)legsArmour).Name.Pastel("#ff9d00") + " - no space to place your current armour " + ((Item)oldArmour).Name.Pastel("#ff9d00"));
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + ((Item)legsArmour).Name.Pastel("#ff9d00"));
        }

        this.legsArmourEquipped = legsArmour;
        this.defense += ((Armour)legsArmour).Defense;
        Console.WriteLine("Your defense now is: " + this.Defense.ToString().Pastel("#1900ff"));
        ArmourSetManager.GetInstance().CheckAndUpdateSetBonuses(this);
        return true;
    }

    // method to use healing potion
    public bool UseHealingPotion(HealingPotion healingPotion)
    {
        this.health = this.health + healingPotion.HealingPower;
        if (this.health <= 0)
        {
            Console.WriteLine("");
            Console.WriteLine("You have used " + healingPotion.Name.Pastel("#7CFC00") + " and it was a " + "DEADLY POISON".Pastel("#fc0303") + "!");
            Console.WriteLine("Your heart stopped beating");
            Console.WriteLine("");
        }
        else
        {
            Console.WriteLine("");
            Console.WriteLine("You have used " + healingPotion.Name.Pastel("#7CFC00") + ", your health is increased by " + healingPotion.HealingPower.ToString().Pastel("#7CFC00"));
            Console.WriteLine("Your health is " + this.health.ToString().Pastel("#7CFC00"));
            Console.WriteLine("");
        }
        return true;
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