using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAO;
using WebApplication1.DataComponents;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public class PersonaService
    {
        private readonly PersonaDAO _personaDAO;

        public PersonaService(PersonaDAO personaDAO)
        {
            _personaDAO = personaDAO;
        }

        internal IList<Persona> GetPersonas()
        {
            List<Persona> result = new List<Persona>();

            foreach (var persona in _personaDAO.FindAll())
            {
                result.Add(persona);
            }

            return result;
        }

        internal Persona AddPersona(Persona persona)
        {
            var result = _personaDAO.Save(persona);

            return result;
        }

        internal void UpdatePersona(Persona persona)
        {
           
            _personaDAO.Update(persona);
        }

        internal void DeletePersona(Persona persona)
        {
            _personaDAO.Delete(persona);
        }

        internal FilterAndSearchPersonResult GetFilterdAndSearchPerson(FilterAndSearchPerson filterAndSearchPerson)
        {
            if (filterAndSearchPerson.StringSearch is null)
            {



                FilterAndSearchPersonResult filterAndSearchPersonResult = new FilterAndSearchPersonResult
                {
                    Personas = _personaDAO.FilterPerson(filterAndSearchPerson.PageNumber * filterAndSearchPerson.Limit,
                    filterAndSearchPerson.Limit, filterAndSearchPerson.OrderAsc, filterAndSearchPerson.OrderField),
                    NumeroDePaginas = _personaDAO.CountAllElement() / filterAndSearchPerson.Limit,
                    NextPage = $"http://localhost:5000/api/Persona/persona/filter?PageNumber=" + $"{filterAndSearchPerson.PageNumber + 1}" + "&Limit=2",
                                       
                };
                if (filterAndSearchPerson.PageNumber != 0)
                {
                    filterAndSearchPersonResult.PreviusPage = $"http://localhost:5000/api/Persona/persona/filter?PageNumber=" + $"{filterAndSearchPerson.PageNumber - 1}" + "&Limit=2";
                }
                return filterAndSearchPersonResult;
            }
            else
            {
                List<Persona> personas = new List<Persona>();//ver como hcer para agregar dos veces als misma persona, por id o algo
                //al dividir un numero impar redondea para arriva
                foreach(var persona in _personaDAO.FindByName(filterAndSearchPerson.StringSearch, filterAndSearchPerson.Limit / 2))
                {
                    personas.Add(persona);
                }

                foreach (var persona in _personaDAO.FindByLastName(filterAndSearchPerson.StringSearch, filterAndSearchPerson.Limit / 2))
                {
                    personas.Add(persona);
                }

                FilterAndSearchPersonResult filterAndSearchPersonResult = new FilterAndSearchPersonResult
                {
                    Personas = personas
                };

                return filterAndSearchPersonResult;
            }
        }
    }
}
