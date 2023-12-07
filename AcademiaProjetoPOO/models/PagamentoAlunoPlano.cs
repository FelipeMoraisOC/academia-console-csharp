

using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    [Table("PagamentoAlunosPlanos")]
    public class PagamentoAlunoPlano
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PagamentoAlunoPlanoId { get; set; }

        [ForeignKey("AlunoPlanoId")]
        public AlunoPlano AlunoPlano { get; set;}
        public int AlunoPlanoId{get; set;}
        public DateTime Data {get; set;}
        public decimal ValorPago {get; set;}

        public PagamentoAlunoPlano(int? pagamentoAlunoPlanoId, int alunoPlanoId, DateTime data, decimal valorPago)
        {
            PagamentoAlunoPlanoId = pagamentoAlunoPlanoId;
            AlunoPlanoId = alunoPlanoId;;
            Data = data;
            ValorPago = valorPago;
        }
    }

}