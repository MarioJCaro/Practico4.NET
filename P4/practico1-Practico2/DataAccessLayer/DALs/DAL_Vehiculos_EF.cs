using DataAccessLayer.EFModels;
using DataAccessLayer.IDALs;
using Microsoft.Data.SqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Vehiculos_EF : IDAL_Vehiculos
    {
        private DBContextCore _dbContext;


        public DAL_Vehiculos_EF(DBContextCore dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(string matricula)
        {
            var vehiculo = _dbContext.Vehiculos.FirstOrDefault(v => v.Matricula == matricula);
            if (vehiculo != null)
            {
                _dbContext.Vehiculos.Remove(vehiculo);
                _dbContext.SaveChanges();
            }
        }



        public List<Vehiculo> Get()
        {
            return _dbContext.Vehiculos
                             .Select(v => new Vehiculo
                             {
                                 Matricula = v.Matricula,
                                 Marca = v.Marca,
                                 Modelo = v.Modelo,
                                 DocumentoPropietario = v.Persona.Documento
                                 
                             })
                             .ToList();
        }

        public Vehiculo Get(string matricula)
        {
            var vehiculo = _dbContext.Vehiculos.FirstOrDefault(v => v.Matricula == matricula);
            if (vehiculo != null)
            {
                return new Vehiculo
                {
                    Matricula = vehiculo.Matricula,
                    Marca = vehiculo.Marca,
                    Modelo = vehiculo.Modelo,
                    DocumentoPropietario = vehiculo.Persona.Documento
                    
                };
            }
            return null;
        }

        public void Insert(Vehiculo vehiculo, string documentoPersona)
        {
            
            var persona = _dbContext.Personas.FirstOrDefault(p => p.Documento == documentoPersona);

            
            if (persona == null)
            {
                throw new Exception("La persona con el documento especificado no existe.");
            }

            
            var nuevoVehiculo = new Vehiculos
            {
                Matricula = vehiculo.Matricula,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Persona = persona 
            };

            
            _dbContext.Vehiculos.Add(nuevoVehiculo);
            _dbContext.SaveChanges();
        }



        public void Update(Vehiculo vehiculo)
        {
            var vehiculoExistente = _dbContext.Vehiculos.FirstOrDefault(v => v.Matricula == vehiculo.Matricula);
            if (vehiculoExistente != null)
            {
                vehiculoExistente.Marca = vehiculo.Marca;
                vehiculoExistente.Modelo = vehiculo.Modelo;
                
                _dbContext.SaveChanges();
            }
        }

        public List<Vehiculo> GetByOwnerDocument(string documentoPersona)
        {
            var persona = _dbContext.Personas.FirstOrDefault(p => p.Documento == documentoPersona);
            if (persona != null)
            {
                return _dbContext.Vehiculos
                                 .Where(v => v.PersonaId == persona.Id)
                                 .Select(v => new Vehiculo
                                 {
                                     Marca = v.Marca,
                                     Modelo = v.Modelo,
                                     Matricula = v.Matricula,
                                     DocumentoPropietario = persona.Documento
                                 }).ToList();
            }
            return new List<Vehiculo>();
        }

    }


}
