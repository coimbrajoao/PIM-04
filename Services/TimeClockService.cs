using Course.Data;
using Course.Data.Dtos;
using Course.Models;
using Course.Repository;
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


        public async Task<TimeClock> Delete(int id)
        {
            try
            {
                var timeClock = await _folhaContext.TimeClocks.FindAsync(id);
                if (timeClock == null)
                {
                    throw new ApplicationException("Frequencia não encontrada");
                }

                _folhaContext.Remove(timeClock);
                await _folhaContext.SaveChangesAsync();


                return timeClock;
            }
            catch (Exception ex)
            {
                throw new Exception("Não tem nenhum movimento de espelho de ponto para este usuário");
            }
        }

        public async Task<TimeClock> Update(int id, TimeClockDto timeClockDto)
        {
            try
            {
                var timeClock = await _folhaContext.TimeClocks.FindAsync(id);
                if (timeClock == null)
                {
                    throw new ApplicationException("Frequencia não encontrada");
                }

                timeClock.TimeOffset = (DateTimeOffset)timeClockDto.TimeOffset;

                try
                {
                    await _folhaContext.SaveChangesAsync();
                    return timeClock;
                }
                catch (Exception)
                {
                    throw new Exception("Falha ao editar o usuario");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Nao tem usuario selecionado para realizar a Edição");
            }
        }
    }
}
