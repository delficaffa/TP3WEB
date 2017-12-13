using DataAccess;
using DataAccess.Entities;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ConsultasHorarios
    {
        private Repository<Horarios> shiftRepository;

        public ConsultasHorarios()
        {
            shiftRepository = new Repository<Horarios>();
        }

        public List<HorariosDto> ListarPorTurno(EnumTurns turn)
        {
            var list = new List<HorariosDto>();
            var all = this.shiftRepository.Read();

            foreach (var horario in all)
            {
                list.Add(new HorariosDto()
                {
                    Id = horario.ID,
                    Turn = horario.Turn,
                    StartlHour = horario.StartlHour,
                    FinishHour = horario.FinishHour

                });
            }
            list.Where(c => c.Turn == turn);

            return list;
        }
    }
}
  