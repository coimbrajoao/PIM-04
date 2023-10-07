﻿using Course.Data.Dtos;
using Course.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClockController : Controller
    {
        private TimeClockService _timeClockService;

        public ClockController(TimeClockService timeClockService)
        {
            _timeClockService = timeClockService;
        }

        [HttpPost]
        
        public async Task<IActionResult> AddTime(TimeClockDto dto)//metodo para adicionar horario no relogio
        {
            var resul = await _timeClockService.Time(dto);
            return Ok(resul);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ViewTimeClock(int id)
        {
            try
            {
            int Pagesize = 1;
            int PageNumb = 10;
            var result = await _timeClockService.GetPagedResultAsync(PageNumb,Pagesize, id);
            return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTimeClock(int id)
        {
            try
            {
                var result = await _timeClockService.Delete(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
