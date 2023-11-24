using MainLib.Api.Controller;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Constant;

namespace Api.Controllers.CommonData;

/// <summary>
/// Контроллер для основных данных
/// </summary>
[Route("common-data")]
public class CommonDataController : PublicController
{
    /// <summary>
    /// Получить список жанров
    /// </summary>
    [HttpGet("genre/list")]
    public async Task<IActionResult> GetGenreListAsync()
    {
        return Ok(new
        {
            ServiceConstant.GenreList
        });
    }
    
    /// <summary>
    /// Получить список жанров
    /// </summary>
    [HttpGet("language/list")]
    public async Task<IActionResult> GetLanguageListAsync()
    {
        return Ok(new
        {
            ServiceConstant.LanguageList
        });
    }
    
    /// <summary>
    /// Получить список жанров
    /// </summary>
    [HttpGet("vibe/list")]
    public async Task<IActionResult> GetVibeListAsync()
    {
        return Ok(new
        {
            ServiceConstant.VibeList
        });
    }
}