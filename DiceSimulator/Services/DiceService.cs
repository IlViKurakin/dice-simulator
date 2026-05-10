namespace DiceSimulator.Services
{
    public class DiceService
    {
        private readonly List<Models.DiceRoll> _history;
        private readonly Random _random;
        private int _nextId;

        public DiceService()
        {
            _history = new List<Models.DiceRoll>();
            _random = new Random();
            _nextId = 1;
        }

        // Бросок указанног количества кубиков
        public Models.DiceRoll RollDice(int NumberOfDice)
        {
            var results = new int[NumberOfDice];
            var sum = 0;

            // Бросок каждого кубика
            for (int i = 0; i < NumberOfDice; i++)
            {
                results[i] = _random.Next(1, 7);
                sum += results[i];
            }

            // Создание записи о броске
            var roll = new Models.DiceRoll
            {
                Id = _nextId++,
                NumberOfDice = NumberOfDice,
                Result = string.Join(", ", results),
                Sum = sum,
                RollTime = DateTime.Now
            };
            // Сохранение в истории
            _history.Add(roll);

            return roll;
        }

        // Получить записи последних бросков
        public List<Models.DiceRoll> GetHistory(int count = 20)
        {
            return _history
                .OrderByDescending(r => r.RollTime)
                .Take(count)
                .ToList();
        }
    }
}