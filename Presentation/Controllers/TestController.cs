using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController: ControllerBase
{
    #region Поля

    

    #endregion

    #region Конструктор

    public TestController() { }

    #endregion

    #region Api - методы

    [HttpPost]
    public IActionResult TestMethod(string message)
    {
        return Ok("Функционала для тестирования нет.");
    }

    #endregion
    
    
}