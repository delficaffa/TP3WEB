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


        //TODO: SEGUIR ACA. TERMINAR DE PENSAR COMO HACER PARA QUE NO AGREGUE UN NUEVO ELEMENTO 
        public HorariosDto GetToday(int id)
        {
            var today = horarioRepository.Set()
                        .Where(c => c.EmployeeId == id && c.StartlHour.Date == DateTime.Now.Date)
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

            public void Add(HorariosDto dto)
        {
            var exist = horarioRepository.Set()
                        .FirstOrDefault(c => c.EmployeeId == dto.EmployeeId && c.StartlHour == dto.StartlHour);

            var toAdd = new Horarios
            {
                EmployeeId = dto.EmployeeId,
                StartlHour = dto.StartlHour,
                FinishHour = dto.FinishHour
            };
            if (exist == null)
            {
                horarioRepository.Add(toAdd);
            }
            else
            {
                horarioRepository.Update(exist);
            }
            horarioRepository.SaveChanges();
        }

        //public void Update(HorariosDto dto)
        //{
        //    var toUpdate = horarioRepository.Set()
        //                .FirstOrDefault(c => c.EmployeeId == dto.EmployeeId && c.StartlHour.Date == dto.StartlHour.Date);

        //    horarioRepository.SaveChanges();
        //}

        //Devuelve todos los empleados por turno
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
        public List<HorariosDto> ListarHorariosDeEmpleado(int idEmpleado, int mes)
        {
            var list = new List<HorariosDto>();
            var porMes = this.horarioRepository.Read()
                .Where(c => c.Employees.ID == idEmpleado && c.StartlHour.Month == mes);

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
