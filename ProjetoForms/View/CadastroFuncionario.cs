using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ProjetoForms.Modelo;
using ProjetoForms.Controller;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.CodeDom;

namespace ProjetoForms.View
{
    public partial class CadastroFuncionario : Form
    {

        private TelaPrincipal  telaPrincipal;
        private Funcionario func;
        public CadastroFuncionario(TelaPrincipal tela) // construtor
        {
            telaPrincipal = tela; // vai voltar sempre pra tela principal depois de realizar uma função do crud
            InitializeComponent();
        }
        public CadastroFuncionario(TelaPrincipal tela, int id) // construtor com id do dataGridView
        {

            telaPrincipal = tela;
            InitializeComponent();

            func = FuncionarioDataAccess.PegarFuncionario(id);
            EditarFuncionario(func);


        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CadastroFuncionario_Load(object sender, EventArgs e)
        {

        }

        private void EditarFuncionario(Funcionario funcionario)
        {
            txtNome.Text = funcionario.Nome.Trim(); // trim remove os espaços em branco a frente do dado da string
            txtEmail.Text = funcionario.Email.Trim();
            txtSalario.Text = funcionario.Salario.ToString("f2",CultureInfo.InvariantCulture);

            if (funcionario.Sexo == "M") 
            {
                rbMasculino.Checked = true;
            }
            else
            {
                rbFeminino.Checked = true;
            } 

            if(funcionario.TipoContrato =="CLT") // valida o radio Button de Tipo Contrato
            {
                rbCLT.Checked = true;
            }
            else if(funcionario.TipoContrato == "PJ")
            {
                rbPJ.Checked = true;
            }
            else
            {
                rbAutonomo.Checked = true;
            }
      
        }

        private void SalvarAction(object sender, EventArgs e)
        {
            Funcionario funcionario;

            if (func != null) // valida se é uma atualização ou um dado novo
            {
                //atualização
                funcionario = func;
                funcionario.DataAtualizacao = DateTime.Now;
            }
            else
            {
                // cadastro novo
                funcionario = new Funcionario();
                funcionario.DataCadastro = DateTime.Now;
            }

            //mover os  dados para a classe funcionario
            funcionario.Nome = txtNome.Text.Trim();
            funcionario.Email = txtEmail.Text.Trim();
            funcionario.Salario = double.Parse(txtSalario.Text.Trim());
            funcionario.Sexo = (rbMasculino.Checked) ? "M": "F".Trim(); // o checked for masculino (estiver true na prioridade) adiciona M se nao F
            funcionario.TipoContrato = (rbCLT.Checked) ? "CLT" : (rbPJ.Checked) ? "PJ": "AUT".Trim(); // valida a opção escolida no radion button salvando dado refrente a escolha como clt, pj, aut
          


            // validação dos dados
            List<ValidationResult> listErros = new List<ValidationResult>();//lista para capturar erros e armazenar
            ValidationContext contect = new ValidationContext(funcionario); // passa o obj que sera validado
            bool validator = Validator.TryValidateObject(funcionario, contect, listErros, true); // valida todos os erros

            try //validação ok
            {

                bool resultado;
                if (func != null) // se for um dado existente é uma atualização
                {
                    resultado = FuncionarioDataAccess.AtualizarFuncionario(funcionario);
                    if(resultado)
                    {
                        MessageBox.Show("ATUALIZADO COM SUCESSO");

                        telaPrincipal.AtualizarTabela(); //volta a tela principal
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("ERRO AO ATUALIZAR");
                    }
                }
                else // se não é um novo dado
                {
                    resultado = FuncionarioDataAccess.SalvarFuncionario(funcionario);
                    if(resultado)
                    {
                        MessageBox.Show("SALVO COM SUCESSO");

                        telaPrincipal.AtualizarTabela(); //volta a tela principal
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("ERRO AO SALVAR");
                    }
                }
            
          
            }
            catch    // validação deu erro
            {
            
              StringBuilder sb = new StringBuilder(); // vai armazenar as mensagens de erro

             foreach(ValidationResult erro in listErros) // percorre a lista de erro e captura os erros
             {
                    sb.Append(erro.ErrorMessage + "\n"); // recebe as mensagens de erro

             }
               lblErros.Text = sb.ToString(); // passas as mensagens de erro para label
            }

             
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void AtualizarAction(object sender, EventArgs e)
        {
            Funcionario funcionario = new Funcionario();

            funcionario.Nome = txtNome.Text;
            funcionario.Email = txtEmail.Text;
            funcionario.Salario = double.Parse(txtSalario.Text);
            funcionario.Sexo = (rbMasculino.Checked) ? "M" : "F"; // o checked for masculino (estiver true na prioridade) adiciona M se nao F
            funcionario.TipoContrato = (rbCLT.Checked) ? "CLT" : (rbPJ.Checked) ? "PJ" : "AUT"; // valida a opção escolida no radion button salvando dado refrente a escolha como clt, pj, aut
            funcionario.DataAtualizacao = DateTime.Now;


            FuncionarioDataAccess.AtualizarFuncionario(funcionario);
        }
    }
}
