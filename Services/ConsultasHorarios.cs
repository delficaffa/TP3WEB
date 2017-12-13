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

        //Devuelve todos los empleados por turno
        public List<HorariosDto> ListarPorTurno(EnumTurns turn)
        {
            var list = new List<HorariosDto>();
            var porTurno = this.horarioRepository.Read()
                .Where(c => c.Employees.Turn == turn);

            foreach (var empleado in porTurno)
            {
                list.Add(new HorariosDto()
                {
                    Id = empleado.ID,
                    EmployeeId = empleado.EmployeeId,
                    StartlHour = empleado.StartlHour,
                    FinishHour = empleado.FinishHour

                });
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
  