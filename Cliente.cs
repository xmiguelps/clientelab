using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteLab
{
    public abstract class Cliente
    {
        public int Id {get; set;}
        
        public string Nome {get; set;}
        
        public string Endereco {get; set;}

        public double Valor_Compra {get; set;}

        public double Valor_Imposto {get; set;}

        public double Total {get; set;}

        public virtual double Pagar_Imposto(double valor)
        {
            valor = valor * 0.10;
            return valor;
        }
    }
}