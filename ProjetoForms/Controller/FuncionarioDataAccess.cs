using System;

using System.Data;
using System.Data.SqlClient;
using ProjetoForms.Modelo;


namespace ProjetoForms.Controller
{
   public  class FuncionarioDataAccess
    {
        private static SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-GABRIEL;Initial Catalog=dbProjetoForms;Integrated Security=True"); // string de coneção
        public static DataTable ConsultarFuncionario()
        {
         

            SqlDataAdapter da = new SqlDataAdapter("Select * from Funcionario", con); // pega os dados da tabala funcionario

            DataSet ds = new DataSet(); // cria dataset

            da.Fill(ds); // preenche o dataset com os dados do banco

            return ds.Tables[0]; // retorna a primeira tabela do banco   

        }

        public static bool SalvarFuncionario(Funcionario funcionario)
        {
            //comando para ser usado no banco
            string sql = " INSERT INTO [Funcionario] (Nome, Email, Salario, Sexo, TipoContrato, DataCadastro) VALUES(@Nome, @Email, @Salario, @Sexo, @TipoContrato, @DataCadastro)"; 

            SqlCommand comando = new SqlCommand(sql,con);

            comando.Parameters.Add("@Nome", funcionario.Nome);
            comando.Parameters.Add("@Email", funcionario.Email);
            comando.Parameters.Add("@Salario", funcionario.Salario );
            comando.Parameters.Add("@Sexo", funcionario.Sexo);
            comando.Parameters.Add("@TipoContrato", funcionario.TipoContrato );
            comando.Parameters.Add("@DataCadastro", funcionario.DataCadastro );

            con.Open(); // abre o banco

            if(comando.ExecuteNonQuery() > 0)// ele retorna um inteiro equivalente ao numero de linhas que foi afetada
            {
                con.Close(); // fecha
                return true;
               
            }
            else
            {
                con.Close(); // fecha
                return false;
            }
          


        }
        public static Funcionario PegarFuncionario(int id ) 
        {
            string sql = " SELECT * FROM  [Funcionario] where Id= @id"; // dou um select no banco de dados e retorno os dados do id que foi passado

            SqlCommand comando = new SqlCommand(sql, con); // realiza o comando no banco

            comando.Parameters.Add("@id", id); // adiciono o id passado no metodo 



            con.Open(); // abre o banco

            SqlDataReader reposta = comando.ExecuteReader();

            Funcionario funcionario = new Funcionario();    

            while(reposta.Read()) // percorre o select do banco
            {
                funcionario.Id = reposta.GetInt32(0); // retorna o id 
                funcionario.Nome = reposta.GetString(1); // retorna o nome
                funcionario.Email = reposta.GetString(2); // retorna o email
                funcionario.Salario = reposta.GetDouble(3); // retorna o salario
                funcionario.Sexo = reposta.GetString(4); // retorna o sexo
                funcionario.TipoContrato= reposta.GetString(5); //  retorna o tipo de contrato
                funcionario.DataCadastro = reposta.GetDateTime(6); // retorna a data de cadastro

                if(reposta.IsDBNull(7))
                {
                    funcionario.DataAtualizacao = null ;
                }
                else
                {
                    funcionario.DataAtualizacao = reposta.GetDateTime(7);
                }
                

            }

   
            con.Close(); // fecha

            return funcionario; // retorno o obj com os dados
        }

        public static bool AtualizarFuncionario(Funcionario funcionario)
        {


            //comando para ser usado no banco
            string sql = " UPDATE [Funcionario] SET Nome = @Nome,Email= @Email,Salario = @Salario, Sexo = @Sexo,TipoContrato = @TipoContrato," +
                "DataAtualizacao = @DataAtualizacao WHERE iD = @id";

            SqlCommand comando = new SqlCommand(sql, con);

            comando.Parameters.Add("@id", funcionario.Id);
            comando.Parameters.Add("@Nome", funcionario.Nome);
            comando.Parameters.Add("@Email", funcionario.Email);
            comando.Parameters.Add("@Salario", funcionario.Salario);
            comando.Parameters.Add("@Sexo", funcionario.Sexo);
            comando.Parameters.Add("@TipoContrato", funcionario.TipoContrato);
            comando.Parameters.Add("@DataAtualizacao", funcionario.DataAtualizacao);

            con.Open(); // abre o banco

            if (comando.ExecuteNonQuery() > 0)// ele retorna um inteiro equivalente ao numero de linhas que foi afetada
            {
                con.Close(); // fecha
                return true;

            }
            else
            {
                con.Close(); // fecha
                return false;
            }

            
        }
        public static bool ExcluirFuncionario(int id)
        {
            string sql = " DELETE FROM  [Funcionario] where Id= @id"; // dou um select no banco de dados e retorno os dados do id que foi passado

            SqlCommand comando = new SqlCommand(sql, con); // realiza o comando no banco

            comando.Parameters.Add("@id", id); // adiciono o id passado no metodo 



            con.Open(); // abre o banco

            if (comando.ExecuteNonQuery() > 0)// ele retorna um inteiro equivalente ao numero de linhas que foi afetada
            {
                con.Close(); // fecha
                return true;

            }
            else
            {
                con.Close(); // fecha
                return false;
            }


      
        }
    }
}
