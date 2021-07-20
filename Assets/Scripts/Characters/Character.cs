using System;

public class Character
{
    public  Health health = new Health();
    public  Level level = new Level();
    public  Mana mana = new Mana();
    public  MagicDamage magicDamage = new MagicDamage();
    public  MagicDefense magicDefense = new MagicDefense();
    public  PhysicalDamage physicalDamage = new PhysicalDamage();
    public  PhysicalDefense physicalDefense = new PhysicalDefense();
    public string Name { get; set; }
    public string ID { get; private set ; }

    public void Ondie()
    {
        if (health.Current <= 0)
        {
           //Do stuff
        } 
    }
    public void OnTakeDamge()
    {

    }
    public void GenerateID()
    {
        ID = Guid.NewGuid().ToString();
    }
}
