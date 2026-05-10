using Microsoft.AspNetCore.Mvc;
using DiceSimulator.Services;

namespace DiceSimulator.Controllers
{
    public class DiceController : Controller
    {
        private readonly DiceService _diceService;

        // Внедрение сервиса через конструктор
        public DiceController(DiceService diceService)
        {
            _diceService = diceService;
        }

        // Главная страница
        public IActionResult Index()
        {
            // Показ последних 5 бросков на главной странице
            ViewBag.RecentRolls = _diceService.GetHistory(5);
            return View();
        }

        // Обработка броска кубиков
        public IActionResult Roll(int NumberOfDice)
        {
            // Проверка числа кубиков (от 1 до 10)
            if (NumberOfDice < 1 || NumberOfDice > 10)
            {
                TempData["Error"] = "Пожалуйста, выберети от 1 до 10 кубиков";
                return RedirectToAction("Index");
            }

            // Выполнение броска
            var result = _diceService.RollDice(NumberOfDice);

            // Передача результата на Web-страницу
            return View("Result", result);
        }

        //Страница истории
        public IActionResult History()
        {
            var history = _diceService.GetHistory();
            return View(history);
        }
    }
}