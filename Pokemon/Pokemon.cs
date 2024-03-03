using static System.Console;

public abstract class Pokemon
{
    public abstract void ShowStats();

    public int id { get; set; }

    public string name { get; set; }

    public List<string> type { get; set; }

    public BaseStats BaseStats { get; set; }

    public void Create(){
        int id_;
        string name_;
        List<string> types_ = new();
        BaseStats baseStats_= new()
        ;

        Write("ID:");
        int.TryParse(ReadLine(), out id_);
        Write("Nombre:");
        name_ = ReadLine();
        Write("Tiene 1 o 2 tipos?:");
        int res;
        int.TryParse(ReadLine(), out res);
        while(res <1|| res >2){
            Write("Debe ser menor a 2 o mayor a 1 la cantidad de tipos");
            int.TryParse(ReadLine(), out res);

         }

         for(int i = 1; i<= res; i++){
            Write($"Tipo {i}:");
            string typ = ReadLine();
            typ = typ.ToLower();
            types_.Add(typ);
         }

        Write("HP:");
        int hp;
        int.TryParse(ReadLine(), out hp);
        baseStats_.HP = hp;

        Write("Attack:");
        int Attack;
        int.TryParse(ReadLine(), out Attack);
        baseStats_.Attack = Attack;

        Write("Defense:");
        int Defense;
        int.TryParse(ReadLine(), out Defense);
        baseStats_.Defense = Defense;

        Write("SpAttack:");
        int SpAttack;
        int.TryParse(ReadLine(), out SpAttack);
        baseStats_.SpAttack = SpAttack;

        Write("SpDefense:");
        int SpDefense;
        int.TryParse(ReadLine(), out SpDefense);
        baseStats_.SpDefense = SpDefense;

        Write("Speed:");
        int Speed;
        int.TryParse(ReadLine(), out Speed);
        baseStats_.Speed = Speed;

         this.id = id_;
         this.name = name_.ToLower();
         this.type = types_;
         this.BaseStats = baseStats_;

    }
}
