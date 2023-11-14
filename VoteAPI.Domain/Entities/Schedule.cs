using VoteAPI.Domain.Exceptions;

namespace VoteAPI.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        public string Description { get; protected set; }
        public DateTime StartingDate { get; protected set; }
        public DateTime FinishingDate { get; protected set; }
        protected Schedule() { }
        public Schedule(string description, DateTime startingDate, DateTime finishingDate)
        {
            ValidateDomain(description, startingDate, finishingDate);
            Active = true;
        }
        private void ValidateDomain(string description, DateTime startingDate, DateTime finishingDate)
        {
            VoteAPIException.When(string.IsNullOrEmpty(description), "Descrição é obrigatória");
            VoteAPIException.When(description.Length < 5, "Descrição deve ter no mínimo 5 caracteres");
            VoteAPIException.When(startingDate > DateTime.Now, "Não é possível abrir uma pauta retroativa");
            VoteAPIException.When(finishingDate > DateTime.Now, "Não é possível fechar uma pauta retroativa");
            VoteAPIException.When(finishingDate < startingDate, "A data de encerramento não pode ser anterior à data de início da pauta");
            Description = description.Trim();
            StartingDate = startingDate;
            FinishingDate = finishingDate;
        }
    }
}