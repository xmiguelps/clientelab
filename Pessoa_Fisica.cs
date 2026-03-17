using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClienteLab;

namespace ClienteLab
{
    public class Pessoa_Fisica : Cliente
    {
        public string Cpf {get; set;}

        public string Rg {get; set;}

        public Pessoa_Fisica(string nome, string endereco, string cpf, string rg)
        {
            Nome = nome;
            Endereco = endereco;
            Cpf = cpf;
            Rg = rg;
        }

        public Pessoa_Fisica()
        {
            Id = -1;
            Nome = "";
            Endereco = "";
            Valor_Compra = -1;
            Valor_Imposto = -1;
            Total = -1;
            Cpf = "";
            Rg = "";
        }
    }
} 