using DataAccess;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
        // Los errores son porque no tengo la base de datos..
    public class Consultas
    {

        private Repository<Employee> employesRepository;

        public Consultas()
        {
            employesRepository = new Repository<Employee>();
        }

        //SI ES QUE TIENE QUE DEVOLVER EL ID CAMBIAR EL VOID POR UN INT
        public void Agregar(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                Country = employeeDto.Country,
                Date = employeeDto.Date,
                Turn = employeeDto.Turn
            };

            employesRepository.Add(employee);
            employesRepository.SaveChanges();

        }

        public List<EmployeeDto> Listar()
        {
            var list = new List<EmployeeDto>();
            var all = employesRepository.Read();

            foreach (var employe in all)
            {
                list.Add(new EmployeeDto()
                {
                    Id = employe.ID,
                    Name = employe.Name,
                    Surname = employe.Surname,
                    Country = employe.Country,
                    Date = employe.Date,
                    Turn = employe.Turn
                });
            }
            return list;

        }

        public void Eliminar(int id)    //SI ES QUE SE BUSCA POR ID
        {
            try
            {
                var deleteEmployee = employesRepository.GetById(id);
                if (deleteEmployee == null)
                {
                    throw new NullReferenceException();
                }
                else
                {

                    employesRepository.Remove(deleteEmployee);
                    employesRepository.SaveChanges();
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


        //public OrderDTO FillData(Orders order)
        //{
        //    return new OrderDTO()
        //    {
        //        OrderID = order.OrderID,
        //        CustomerID = order.CustomerID,
        //        EmployeeID = order.EmployeeID,
        //        OrderDate = order.OrderDate,
        //        RequiredDate = order.RequiredDate,
        //        ShipName = order.ShipName,
        //        ShipAddress = order.ShipAddress,
        //        ShipCity = order.ShipCity,
        //        ShipRegion = order.ShipRegion,
        //        ShipPostalCode = order.ShipPostalCode,
        //        ShipCountry = order.ShipCountry
        //    };
        //}

        public Employee FillData(EmployeeDto employee)
        {
            var newEmployee = employesRepository.GetById(employee.Id);//SI ES QUE SE BUSCA POR ID

            newEmployee.Name = employee.Name;
            newEmployee.Surname = employee.Surname;
            newEmployee.Country = employee.Country;
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

