namespace WinFormsLeituraImportacaoTxt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] linhas = System.IO.File.ReadAllLines(@"C:\Users\fabri\source\repos\WinFormsLeituraImportacaoTxt\WinFormsLeituraImportacaoTxt\desafio1.txt");
            
            Pessoa p = null;

            foreach (string linha in linhas)
            {
                string[] dados = linha.Split('-');

                if (dados[0] == "X")
                {
                    continue;
                }

                if (dados[0] == "Z")
                {
                    if(p != null)
                    {
                        p.gravar();
                    }

                    p = new Pessoa();

                    p.nome = dados[1];
                    p.fone = dados[2];
                    p.cidade = dados[3];
                    p.rg = dados[4];
                    p.cpf = dados[5];
                }

                if(dados[0] == "Y")
                {
                    Aluno a = new Aluno();
                    a.nome = p.nome;
                    a.cpf = p.cpf;
                    a.fone = p.fone;
                    a.cidade = p.cidade;
                    a.rg = p.rg;
                    a.cpf = p.cpf;
                    a.matrícula = dados[1];

                    Curso c = new Curso();
                    c.id = int.Parse(dados[2]);
                    c.nome = dados[3];

                    a.curso = c;

                    a.gravar();

                    p = null;
                }
            }

            if (p != null)
            {
                p.gravar();
            }
        }
    }
}