using BusinessLayer.BLs;
using BusinessLayer.IBLs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticoClase1
{
    public class VehiculoCommands
    {
        IBL_Vehiculos _vehiculosBL;

        public VehiculoCommands(IBL_Vehiculos vehiculosBL)
        {
            _vehiculosBL = vehiculosBL;
        }

        public void AddVehiculo()
        {
            // Pedimos los datos del vehículo.
            Vehiculo vehiculo = new Vehiculo();
            Console.WriteLine("Ingrese la marca del vehículo: ");
            vehiculo.Marca = Console.ReadLine();
            Console.WriteLine("Ingrese el modelo del vehículo: ");
            vehiculo.Modelo = Console.ReadLine();
            Console.WriteLine("Ingrese la matrícula del vehículo: ");
            vehiculo.Matricula = Console.ReadLine();
            Console.WriteLine("Ingrese el documento del propietario del vehículo: ");
            vehiculo.DocumentoPropietario = Console.ReadLine();

            _vehiculosBL.Insert(vehiculo, vehiculo.DocumentoPropietario);

            _vehiculosBL.Get(vehiculo.Matricula).Print();
        }

        public void ListVehiculos()
        {
            List<Vehiculo> vehiculos = _vehiculosBL.Get();

            Console.WriteLine("Listado de vehículos:");
            Console.WriteLine("| Marca | Modelo | Matrícula | Propietario |");

            foreach (Vehiculo vehiculo in vehiculos)
            {
                vehiculo.PrintTable();
            }
        }

        public void RemoveVehiculo()
        {
            Console.WriteLine("Ingrese la matrícula del vehículo a eliminar: ");
            string matricula = Console.ReadLine();

            _vehiculosBL.Delete(matricula);

            Console.WriteLine("Vehículo eliminado correctamente.");
        }
    }
}
