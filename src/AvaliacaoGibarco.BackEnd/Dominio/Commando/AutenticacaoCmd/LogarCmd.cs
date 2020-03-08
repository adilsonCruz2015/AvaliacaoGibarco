namespace AvaliacaoGibarco.BackEnd.Dominio.Commando.AutenticacaoCmd
{
    public class LogarCmd
    {
        public string Login { get; set; }

        public string Senha { get; set; }

        public int ExpiraEm { get; set; }
    }
}
