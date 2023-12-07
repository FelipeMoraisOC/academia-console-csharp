using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    [Table("TreinosExercicios")]
    public class TreinoExercicio
    {

        public Treino Treino {get; set;}
        public int TreinoId {get; set;}

        public Exercicio Exercicio {get; set;}
        public int ExercicioId{get; set;}


        public TreinoExercicio( int treinoId, int exercicioId)
        {
            TreinoId = treinoId;
            ExercicioId = exercicioId;
        }
    }
}