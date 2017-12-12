using DataAccess;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
       
    public class ConsultasEmployees
    {

        private Repository<Employee> employesRepository;

        public ConsultasEmployees()
        {
            employesRepository = new Repository<Employee>();
        }

        
        public int Agregar(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                Country = employeeDto.CountryID,
                Date = employeeDto.Date,
                Turn = employeeDto.Turn
            };

            this.employesRepository.Add(employee);
            this.employesRepository.SaveChanges();

            return employee.ID;
        }

        public List<EmployeeDto> Listar()
        {
            var list = new List<EmployeeDto>();
            var all = this.employesRepository.Read();

            foreach (var employe in all)
            {
                list.Add(new EmployeeDto()
                {
                    Id = employe.ID,
                    Name = employe.Name,
                    Surname = employe.Surname,
                    CountryName = employe.Country1.Name,
                    Date = employe.Date,
                    Turn = employe.Turn
                });
            }
            return list;

        }

        public List<EmployeeDto> ListarPorTurno(int turno)
        {
            var list = new List<EmployeeDto>();

            switch (turno)
            {
                case 0:
                     list = this.employesRepository.Set()
                        .Where(c => c.Turn == EnumTurns.Morning).Select(
                         c => new EmployeeDto
                         {
                             Id = c.ID,
                             Name = c.Name,
                             Surname = c.Surname,
                             CountryName = c.Country1.Name,
                             Date = c.Date,
                             Turn = c.Turn
                         }).ToList();
                    break;
                case 1:
                    list = this.employesRepository.Set()
                        .Where(c => c.Turn == EnumTurns.Late).Select(
                         c => new EmployeeDto
                         {
                             Id = c.ID,
                             Name = c.Name,
                             Surname = c.Surname,
                             CountryName = c.Country1.Name,
                             Date = c.Date,
                             Turn = c.Turn
                         }).ToList(); 
                    break;
                case 2:
                    list = this.employesRepository.Set()
                        .Where(c => c.Turn == EnumTurns.Night).Select(
                         c => new EmployeeDto
                         {
                             Id = c.ID,
                             Name = c.Name,
                             Surname = c.Surname,
                             CountryName = c.Country1.Name,
                             Date = c.Date,
                             Turn = c.Turn
                         }).ToList();
                    break;
            }

           
            return list;

        }

        public void Eliminar(int id)    
        {
            try
            {
                var deleteEmployee = this.employesRepository.GetById(id);
                if (deleteEmployee == null)
                {
                    throw new NullReferenceException();
                }
                else
                {

                    this.employesRepository.Remove(deleteEmployee);
                    this.employesRepository.SaveChanges();
                    Console.WriteLine("Empleado eliminado Correctamente");

                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($"No existe un empleado con el ID: {id}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("No se pudo eliminar el empleado..");
            }

        }

        public Employee FillData(EmployeeDto employee)
        {
            var newEmployee = this.employesRepository.GetById(employee.Id);

            newEmployee.Name = employee.Name;
            newEmployee.Surname = employee.Surname;
            newEmployee.Country = employee.CountryID;
            newEmployee.Date = employee.Date;
            newEmployee.Turn = employee.Turn;

            return newEmployee;

        }

        public void Modificar(EmployeeDto employeeEdited)
        {

            this.employesRepository.Update(FillData(employeeEdited));
            employesRepository.SaveChanges();

        }

    }
}

