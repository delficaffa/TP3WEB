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

        public EmployeeDto GetById(int id)
        {
            var employee = employesRepository.Set()
                            .Select(
                         c => new EmployeeDto
                         {
                             Id = c.ID,
                             Name = c.Name,
                             Surname = c.Surname,
                             CountryName = c.Country1.Name,
                             Date = c.Date,
                             Turn = c.Turn,
                             Price = c.Price
                         }).FirstOrDefault(c => c.Id == id);
            return employee;
        }
        
        public int Agregar(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                Country = employeeDto.CountryID,
                Date = employeeDto.Date,
                Turn = employeeDto.Turn,
                Price = employeeDto.Price
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
                    CountryID = employe.Country,
                    Date = employe.Date,
                    Turn = employe.Turn,
                    Price = employe.Price
                });
            }
            return list;

        }

        public List<EmployeeDto> ListarPorTurno(int turno)
        {
            var list = new List<EmployeeDto>();
            EnumTurns turn = new EnumTurns();
            switch (turno)
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

            list = this.employesRepository.Set()
                        .Where(c => c.Turn == turn).Select(
                         c => new EmployeeDto
                         {
                             Id = c.ID,
                             Name = c.Name,
                             Surname = c.Surname,
                             CountryName = c.Country1.Name,
                             Date = c.Date,
                             Turn = c.Turn,
                             Price = c.Price
                         }).ToList();

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
            newEmployee.Price = employee.Price;

            return newEmployee;

        }

        public void Modificar(EmployeeDto employeeEdited)
        {

            this.employesRepository.Update(FillData(employeeEdited));
            employesRepository.SaveChanges();

        }

    }
}

