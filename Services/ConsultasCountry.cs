using DataAccess;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ConsultasCountry
    {
        private Repository<Country> countryRepository;

        public ConsultasCountry()
        {
            countryRepository = new Repository<Country>();
        }


        public void Agregar(CountryDto countryDto)
        {
            var country = new Country()
            {
                Name = countryDto.Name                
            };

            this.countryRepository.Add(country);
            this.countryRepository.SaveChanges();

        }

        public List<CountryDto> Listar()
        {
            var list = new List<CountryDto>();
            var all = this.countryRepository.Read();

            foreach (var country in all)
            {
                list.Add(new CountryDto()
                {
                    CountryID = country.CountryID,
                    Name = country.Name
                });
            }
            return list;
        }

        public void Eliminar(int id)
        {
            try
            {
                var deleteCountry = countryRepository.GetById(id);
                if (deleteCountry == null)
                {
                    throw new NullReferenceException();
                }
                else
                {

                    this.countryRepository.Remove(deleteCountry);
                    this.countryRepository.SaveChanges();
                    Console.WriteLine("Pais eliminado Correctamente");

                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine($"No existe un pais llamado : {id}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("No se pudo eliminar el pais..");
            }

        }

        public Country FillData(CountryDto country)
        {
            var newCountry = this.countryRepository.GetById(country.CountryID);

            newCountry.Name = country.Name;
           
            return newCountry;

        }

        public void Modificar(CountryDto countryEdited)
        {

            this.countryRepository.Update(FillData(countryEdited));
            countryRepository.SaveChanges();

        }

    }
}

