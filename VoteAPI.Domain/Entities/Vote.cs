using VoteAPI.Domain.Exceptions;

namespace VoteAPI.Domain.Entities
{
    public class Vote : BaseEntity
    {
        public long PersonId { get; protected set; }
        public Person Person { get; protected set; }
        public long ScheduleId { get; protected set; }
        public Schedule Schedule { get; protected set; }
        public string VoteOption { get; protected set; }
        protected Vote() { }
        public Vote(long personId, long scheduleId, string voteOption)
        {
            ValidateDomain(personId, scheduleId, voteOption);
            Active = true;
        }
        private void ValidateDomain(long personId, long scheduleId, string voteOption)
        {
            VoteAPIException.When(personId <= 0, "O ID da pessoa votante é inválido");
            VoteAPIException.When(scheduleId <= 0, "O ID da pauta a ser votada é inválido");
            VoteAPIException.When(string.IsNullOrEmpty(voteOption), "A opção de voto é obrigatória e não pode estar em branco");
            VoteAPIException.When(voteOption.Length != 1 || (voteOption != "S" && voteOption != "N"), "Opção de voto inválida. Deve ser 'S' para Sim ou 'N' para Não");
            PersonId = personId;
            ScheduleId = scheduleId;
            VoteOption = voteOption;
        }
    }
}