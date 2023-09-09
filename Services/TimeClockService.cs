using Course.Data;
using Course.Data.Dtos;
using Course.Models;
using Microsoft.EntityFrameworkCore;

namespace Course.Services
{
    public class TimeClockService
    {
        private FolhaContext _folhaContext;

        public TimeClockService(FolhaContext folhaContext)//dependencia do banco
        {
            _folhaContext = folhaContext;
        }

        public async Task<DateTime> Time(TimeClockDto clockDto)//metodo para adcionar hora
        {
            var Autenticacao = await _folhaContext.Users.AnyAsync(x => x.Id == clockDto.UserId);//autenticando o usuario
            if (Autenticacao == null)
            {
                throw new Exception("Usuário não autenticado");
            }

            var timeclock = new TimeClock//instanciando objeto para poder armazenar.
            {
                UserId = clockDto.UserId,
                TimeOffset = DateTimeOffset.Now
            };
             _folhaContext.TimeClocks.Add(timeclock);

            await _folhaContext.SaveChangesAsync();
            return DateTime.Now;
        }

        public async Task<PagedResult<TimeClock>> GetPagedResultAsync(int PageNumber, int PageSize, int userId)
        {
            var query = _folhaContext.TimeClocks.Where(x => x.UserId == userId);

            var count = await query.CountAsync();
            var items = await query.OrderBy(x => x.IdTimeclock).ToListAsync();
            Console.WriteLine(items);
                // .Skip((PageNumber - 1) * PageSize)
                // .Take(PageSize)
                // .ToListAsync();
            

            var pagedResult = new PagedResult<TimeClock>()
            {
                TotalCount = count,
                PageNumber = PageNumber,
                PageSize = PageSize,
                Items = items
            };

            return pagedResult;
        }

    }
}
