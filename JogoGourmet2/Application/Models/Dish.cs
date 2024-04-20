namespace JogoGourmet.Application.Models
{
    public class Dish
    {
        public string Name { get; private set; }
        public string Characteristic { get; private set; }

        public Dish(string name, string characteristic)
        {
            Name = name;
            Characteristic = characteristic;
        }
    }
}


