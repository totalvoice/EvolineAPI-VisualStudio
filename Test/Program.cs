using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class Program {
        static void Main(string[] args) {

            Evoline.API.EvolineAPI api = new Evoline.API.EvolineAPI("{{access-token}}");
            api.debugOn();
            dynamic saldo = api.consultaSaldo();
            Console.WriteLine(saldo.dados.saldo);


            dynamic chamada = api.enviaChamada("**********", "**********", true);
            Console.WriteLine(chamada);

            Console.ReadLine();
        }
    }
}
