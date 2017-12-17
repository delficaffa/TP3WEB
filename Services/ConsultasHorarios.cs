using DataAccess;
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

        /// <summary>
        /// Obtiene los horarios de HOY de un empleado segun su ID. Devuelve null si no se ingreso ningun horario hoy.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HorariosDto GetToday(int id)
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

            var today = new HorariosDto();
                
                today = horarioRepository.Set()
                        .Where(c => c.EmployeeId == id && c.StartlHour >= startDateTime && c.StartlHour <= endDateTime)
                        .Select(c => new HorariosDto
                        {
                            Id = c.ID,
                            EmployeeId = c.EmployeeId,
                            StartlHour = c.StartlHour,
                            FinishHour = c.FinishHour
                        })
                        .FirstOrDefault();

            return today;
        }

        /// <summary>
        /// Agrega un horario si todavia no se ha agregado ningun horario hoy. Si ya se agrego un horario el dia de hoy lo modifica.
        /// </summary>
        /// <param name="dto"></param>
            public void Add(HorariosDto dto)
        {
            var exist = GetToday(dto.EmployeeId);
                        
            if (exist == null)
            {
                var toAdd = new Horarios
                {
                    EmployeeId = dto.EmployeeId,
                    StartlHour = dto.StartlHour,
                    FinishHour = dto.FinishHour
                };
                horarioRepository.Add(toAdd);
            }
            else
            {
                var toAdd = new Horarios
                {
                    ID = exist.Id,
                    EmployeeId = exist.EmployeeId,
                    StartlHour = dto.StartlHour,
                    FinishHour = dto.FinishHour
                };
                   horarioRepository.Update(toAdd);
            }
            horarioRepository.SaveChanges();
        }
        
        /// <summary>
        /// Devuelve la lista de los horarios de HOY segun un turno especifico.
        /// </summary>
        /// <param name="numTurn"></param>
        /// <returns></returns>
        public List<HorariosDto> ListarPorTurno(int numTurn)
        {
            var turn = new EnumTurns();

            switch (numTurn)
            {
                case 0:
                    turn = EnumTurns.Morning;
                    break;
                case 1:
                    turn = EnumTurns.Late;
                    break;
                case 2:
                    turn = EnumTurns.Night;
                    break;
            }

            var list = new List<HorariosDto>();
            var porTurno = this.horarioRepository.Read()
                .Where(c => c.Employees.Turn == turn);

            foreach (var empleado in porTurno)
            {
                if (empleado.StartlHour.Date == DateTime.Today)
                {
                    list.Add(new HorariosDto()
                    {
                        Id = empleado.ID,
                        EmployeeId = empleado.EmployeeId,
                        StartlHour = empleado.StartlHour,
                        FinishHour = empleado.FinishHour

                    });
                }
            };
            return list;
        }

        //Devuelve los horarios trabajados por empleado por mes
        public List<HorariosDto> ListarHorariosDeEmpleado(int idEmpleado, int mes, int año)
        {
            var list = new List<HorariosDto>();
            var porMes = this.horarioRepository.Read()
                .Where(c => c.Employees.ID == idEmpleado && c.StartlHour.Month == mes && c.StartlHour.Year == año);

            foreach (var horario in porMes)
            {
                list.Add(new HorariosDto()
                {
                    Id = horario.ID,
                    EmployeeId = horario.EmployeeId,
                    StartlHour = horario.StartlHour,
                    FinishHour = horario.FinishHour

                });
            };
            return list;
        }

    }
}
