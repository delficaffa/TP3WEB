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
        private Repository<Horarios> horarioRepository;

        public ConsultasHorarios()
        {
            horarioRepository = new Repository<Horarios>();
        }

        public List<HorariosDto> ListarPorTurno(EnumTurns turn)
        {
            var list = new List<HorariosDto>();
            var all = this.horarioRepository.Read();

            foreach (var horario in all)
            {
                list.Add(new HorariosDto()
                {
                    Id = horario.ID,
                    Employee = horario.Employees,
                    StartlHour = horario.StartlHour,
                    FinishHour = horario.FinishHour

                });
            }
            list.Where(c => c.Employee.Turn == turn);

            return list;
        }
    }
}
  