using System.Data.Common;
using Items;
using Items.Armour;
// Singleton player class that represents the only one player in the game

public class Player
{
    private static Player instance = null;

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

    private Player() { }

    private int health;
    private int xcoordinate;
    private int ycoordinate;
    private int defense;
    private int baseAttackPower;
    private int attackPower;
    private Weapon weaponEquipped;
    private IHeadArmour headArmourEquipped;
    private ITorsoArmour torsoArmourEquipped;
    private ILegsArmour legsArmourEquipped;
    private Inventory inventory;

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
    public int BaseAtttackPower
    {
        get { return baseAttackPower; }
        set { baseAttackPower = value; }
    }
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }
    public Weapon WeaponEquiped
    {
        get { return weaponEquipped; }
        set { weaponEquipped = value; }
    }
    public IHeadArmour HeadArmourEquipped
    {
        get { return headArmourEquipped; }
        set { headArmourEquipped = value; }
    }
    public ITorsoArmour TorsoArmourEquipped
    {
        get { return torsoArmourEquipped; }
        set { torsoArmourEquipped = value; }
    }
    public ILegsArmour LegsArmourEquipped
    {
        get { return legsArmourEquipped; }
        set { legsArmourEquipped = value; }
    }

    public Inventory Inventory
    {
        get { return inventory; }
    }

    // --- Constructor ---
    private Player(int health, int xcoordinate, int ycoordinate)
    {
        this.health = health;
        this.xcoordinate = xcoordinate;
        this.ycoordinate = ycoordinate;
        this.defense = 0;
        this.baseAttackPower = 1;
        this.attackPower = 1;
        this.weaponEquipped = null;
        this.headArmourEquipped = null;
        this.torsoArmourEquipped = null;
        this.legsArmourEquipped = null;
        this.inventory = new Inventory(10);
    }

    // methods for player movement
    public void MoveUp()
    {   
        // new coordinate is created and added
        int newYcoordinate = ycoordinate + 1;

        if (RoomChecker.GetInstance().doesRoomExist(xcoordinate, newYcoordinate))
        {   
            // only if a room exists the new coordinatate is assigned to the player's coordinate
            ycoordinate = newYcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(this);
        }
        else
        {
            // if a room in that direction doesn't exist, nothing is happening
            // the player stays at the same place
            // the message displays that the player can not move there
            Console.WriteLine("There is no room in that direction.");
        }
    }

    public void MoveDown()
    {
        int newYcoordinate = ycoordinate - 1;

        if (RoomChecker.GetInstance().doesRoomExist(xcoordinate, newYcoordinate))
        {
            ycoordinate = newYcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(this);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.");
        }
    }

    public void MoveLeft()
    {
        int newXcoordinate = xcoordinate - 1;

        if (RoomChecker.GetInstance().doesRoomExist(newXcoordinate, ycoordinate))
        {
            xcoordinate = newXcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(this);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.");
        }
    }
    public void MoveRight()
    {
        int newXcoordinate = xcoordinate + 1;

        if (RoomChecker.GetInstance().doesRoomExist(newXcoordinate, ycoordinate))
        {
            xcoordinate = newXcoordinate;
            Console.WriteLine($"Player moved up. Current position: ({xcoordinate}, {ycoordinate})");
            RoomChecker.GetInstance().DisplayCurrentRoom(this);
        }
        else
        {
            Console.WriteLine("There is no room in that direction.");
        }
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
                Console.WriteLine("You placed the item in the room " + this.WeaponEquiped.Name + " and equipped " + weapon.Name);
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
            Console.WriteLine("Your damage now is: " + this.AttackPower);
            return true;
        }
    }

    // method to equip head armour
    public bool EquipHeadArmour(IHeadArmour headArmour)
    {

        IHeadArmour oldArmour = this.headArmourEquipped;

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
                Console.WriteLine("You placed your " + ((Item)oldArmour).Name);
            }
            else
            {
                Console.WriteLine("Cannot equip " + ((Item)headArmour).Name + " - no space to place your current armour " + ((Item)oldArmour).Name);
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + ((Item)headArmour).Name);
        }

        // equips armour if the checks are successful
        this.headArmourEquipped = headArmour;
        this.defense += ((Armour)headArmour).Defense;
        Console.WriteLine("Your defense now is: " + this.Defense);
        return true;
    }

    // method to equip torso armour
    public bool EquipTorsoArmour(ITorsoArmour torsoArmour)
    {
        ITorsoArmour oldArmour = this.torsoArmourEquipped;

        if (oldArmour != null)
        {
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(this);
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = (Item)oldArmour;
                this.defense -= ((Armour)oldArmour).Defense;
                Console.WriteLine("You placed your " + ((Item)oldArmour).Name);
            }
            else
            {
                Console.WriteLine("Cannot equip " + ((Item)torsoArmour).Name + " - no space to place your current armour " + ((Item)oldArmour).Name);
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + ((Item)torsoArmour).Name);
        }

        this.torsoArmourEquipped = torsoArmour;
        this.defense += ((Armour)torsoArmour).Defense;
        Console.WriteLine("Your defense now is: " + this.Defense);
        return true;
    }
    
        // method to equip legs armour
        public bool EquipLegsArmour (ILegsArmour legsArmour)
    {
        ILegsArmour oldArmour = this.legsArmourEquipped;

        if (oldArmour != null)
        {
            Room currentRoom = RoomChecker.GetInstance().GetCurrentRoom(this);
            if (currentRoom != null && currentRoom.Item == null)
            {
                currentRoom.Item = (Item)oldArmour;
                this.defense -= ((Armour)oldArmour).Defense;
                Console.WriteLine("You placed your " + ((Item)oldArmour).Name);
            }
            else
            {
                Console.WriteLine("Cannot equip " + ((Item)legsArmour).Name + " - no space to place your current armour " + ((Item)oldArmour).Name);
                return false;
            }
        }
        else
        {
            Console.WriteLine("You equipped " + ((Item)legsArmour).Name);
        }

        this.legsArmourEquipped = legsArmour;
        this.defense += ((Armour)legsArmour).Defense;
        Console.WriteLine("Your defense now is: " + this.Defense);
        return true;
    }
}