using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana7_.Net
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();

        static void Main(string[] args)
        {
            BonusTrack();
            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.WriteLine("{0,1} ", num);
            }
            
            Console.WriteLine("### LINQ Lambda ###");

            var lista_resultado = numbers.Where(n => n % 2 == 0);

            foreach (var num in lista_resultado)
            {
                Console.WriteLine(num);
            }


        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

            Console.WriteLine("### LINQ Lambda ###");

            var queryAllCustomersLamb = context.clientes.Select(c => c);

            foreach (var item in queryAllCustomersLamb)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);

            }

            Console.WriteLine("### LINQ Lambda ###");

            var queryLondonCustomersLamb = context.clientes.Select(c => c).Where(c => c.Ciudad == "Londres");

            foreach (var item in queryLondonCustomersLamb)
            {
                Console.WriteLine(item.Ciudad);
            }

        }

        static void Ordering()
        {
            var queryLondonCustomers3 =
                from cust in context.clientes
                where cust.Ciudad == "Londres"
                orderby cust.NombreCompañia ascending
                select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }

            Console.WriteLine("### LINQ Lambda ###");

            var queryLondonCustomer3Lamb = context.clientes.Where(c => c.Ciudad == "Londres")
                                            .OrderByDescending(c => c.NombreCompañia).ToList();

            foreach (var item in queryLondonCustomer3Lamb)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void Grouping()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine(" {0} ", customer.NombreCompañia);
                }

            }

            Console.WriteLine("### LINQ Lambda ###");

            var queryCustomerByCityLamb = context.clientes.GroupBy(c => c.Ciudad);

            foreach (var customerGroup in queryCustomerByCityLamb)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine(" {0} ", customer.NombreCompañia);
                }

            }

        }

        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }

            Console.WriteLine("### LINQ Lambda ###");

            var custQueryLamb = context.clientes.GroupBy(c => c.Ciudad).Where(c => c.Count() > 2)
                                .OrderBy(c => c.Key);

            foreach (var item in custQueryLamb)
            {
                Console.WriteLine(item.Key);
            }

        }

        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }

            Console.WriteLine("### LINQ Lambda ###");

            var innerJoinLamba = context.clientes.Join(context.Pedidos, a => a.idCliente,
               b => b.IdCliente,
               (a, b) => new { a.NombreCompañia, b.PaisDestinatario });

            foreach (var item in innerJoinLamba)
            {
                Console.WriteLine($"{item.NombreCompañia} y destinatario : {item.PaisDestinatario} ");

            }

        }

        static void BonusTrack()
        {

            var filtroFechaPedidos =
                            from dp in context.detallesdepedidos
                            join ped in context.Pedidos on dp.idpedido equals ped.IdPedido
                            join cli in context.clientes on ped.IdCliente equals cli.idCliente
                            select new { NombreCompañia = cli.NombreCompañia, Cantidad = dp.cantidad };

            foreach (var item in filtroFechaPedidos)
            {
                Console.WriteLine(item.NombreCompañia + item.Cantidad);
            }
            

        }


    }
}
