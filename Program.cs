using ClienteLab;
using Google.Protobuf;
using Org.BouncyCastle.Crypto.Digests;
using System.Globalization;
using ZstdSharp.Unsafe;

int num = 0;
SistemaDAO sys = new SistemaDAO();

while (num != 5)
{
Console.WriteLine("===========================================");
Console.WriteLine("       SISTEMA DE GESTÃO - CLIENTLAB       ");
Console.WriteLine("===========================================");
Console.WriteLine("1. Cadastrar Novo Cliente");
Console.WriteLine("2. Registrar Nova Venda(Cálculo de Imposto)");
Console.WriteLine("3. Consultar Histórico de Vendas");
Console.WriteLine("4. Gerar Relatório Consolidado (CSV)");
Console.WriteLine("5. Sair do Sistema");
Console.WriteLine("===========================================");
Console.Write("Escolha um opção: ");
num = int.Parse(Console.ReadLine());
string nome = "";
string endereco = "";
switch (num)
{
    case 1:
        Console.WriteLine("--- CADASTRO DE CLIENTE ---");
        Console.Write("Pessoa Física (f) ou Jurídica (j)? ");
        string escolha_cad = Console.ReadLine();
                switch (escolha_cad)
                {
                    case "f":
                        Console.Write("Informar Nome: ");
                        nome = Console.ReadLine();
                        Console.Write("Informar Endereco: ");
                        endereco = Console.ReadLine();
                        Console.Write("Informar CPF: ");
                        string cpf = Console.ReadLine();
                        Console.Write("Informar RG: ");
                        string rg = Console.ReadLine();
                        Pessoa_Fisica clientef = new Pessoa_Fisica(nome, endereco, cpf, rg);
                        
                        sys.CadastrarClientePF(clientef);
                        break;

                    case "j":
                        Console.Write("Informar Nome: ");
                        nome = Console.ReadLine();
                        Console.Write("Informar Endereco: ");
                        endereco = Console.ReadLine();
                        Console.Write("Informar CNPJ: ");
                        string cnpj = Console.ReadLine();
                        Console.Write("Informar IE: ");
                        string ie = Console.ReadLine();
                        Pessoa_Juridica clientej = new Pessoa_Juridica(nome, endereco, cnpj, ie);
                        
                        sys.CadastrarClientePJ(clientej);
                        break;

                    default:
                        Console.WriteLine("Digite algo que corresponda as duas opções");
                        break;
                }
        break;
    
    case 2:
        Console.WriteLine("--- REGISTRO DE VENDA ---");
        Console.Write("A venda será para Pessoa Física (f) ou Jurídica (j)? ");
        string escolha_vend = Console.ReadLine();  
                double valor_compra = -1;
                int id = -1;
                while (id < 0)
                {
                    Console.Write("Informe o ID do Cliente: ");
                    id = int.Parse(Console.ReadLine());
                    if (id < 0)
                    {
                        Console.WriteLine("O ID não pode ser negativo, Tente Novamente");
                        Console.WriteLine("\n----------------------------");
                    }
                }
                while (valor_compra < 0)
                {
                    Console.Write("Informe Valor da Compra: ");
                    valor_compra = double.Parse(Console.ReadLine());
                    if (valor_compra < 0)
                    {
                        Console.WriteLine("O Valor da compra não pode ser negativo, Tente Novamente");
                        Console.WriteLine("\n----------------------------");
                    }
                }
                sys.RegistrarVenda(escolha_vend, id, valor_compra);
        
        break;

    case 3:
        Console.WriteLine("--- HISTORICO GERAL DE VENDAS ---");
        sys.ConsultarRegistros();
        break;
    
    case 4:
        Console.WriteLine("--- EXPORTAÇÃO DE RELATORIO ---");
        Console.WriteLine("Conectando ao banco de dados...");
        Console.WriteLine("Coletando histórico de Pessoa Física e Jurídica...");
        Console.WriteLine("Formatando dados...");
        sys.GerarRelatorioCSV();
        break;

    case 5:
        break;

    default:
        Console.WriteLine("Insira um numero que corresponda a lista");
        break;
    }
}
Console.WriteLine("Saindo...");