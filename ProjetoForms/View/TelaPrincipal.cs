using Microsoft.Identity.Client;
using ProjetoForms.Controller;
using ProjetoForms.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProjetoForms
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
            AtualizarTabela();

        }
        public void AtualizarTabela() // atualiza o dataGridView
        {

            dgvTabelaFuncionario.DataSource = FuncionarioDataAccess.ConsultarFuncionario(); //passsa a consulta para o datagridview
        }

        private void TelaPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void NovoAction(object sender, EventArgs e)
        {
           
            new CadastroFuncionario(this).Show(); // abre a tela de cadastro com os campos vazios
           
        }

        private void EditarAction(object sender, EventArgs e)
        {
            int  id = (int)dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value; // pega o id do  dataGridView Selecionado

            new CadastroFuncionario(this,id).Show(); // vai pra tela de cadastro com os campos preenchidos pelo id do dataGridView Selecionado
        }

        private void ExcluirAction(object sender, EventArgs e)
        {
            int id = (int)dgvTabelaFuncionario.SelectedRows[0].Cells[0].Value; // pega o id do  dataGridView Selecionado
            FuncionarioDataAccess.ExcluirFuncionario(id);
            AtualizarTabela(); //atualizada a tabela depois de excluir
        }

        private void dgvTabelaFuncionario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
