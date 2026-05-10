namespace DiceSimulator.Models
{
    // Класс, который сохраняет информацию об одном броске кубика
    public class DiceRoll
    {
        public int Id { get; set; } // Номер броска
        public int NumberOfDice { get; set; } // Количество брошенных кубиков
        public string Result { get; set; } // Результаты
        public int Sum { get; set; } // Сумма всех выпавших значений
        public DateTime RollTime { get; set; } // Время броска
    }
}