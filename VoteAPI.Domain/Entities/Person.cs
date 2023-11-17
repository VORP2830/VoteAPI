using System.Text.RegularExpressions;
using VoteAPI.Domain.Exceptions;

namespace VoteAPI.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string CPF { get; protected set; }
        protected Person() { }
        public Person(string cpf)
        {
            ValidateDomain(cpf);
            Active = true;
        }
        private void ValidateDomain(string cpf)
        {
            cpf = Regex.Replace(cpf, "[^0-9]", "");
            VoteAPIException.When(cpf.Length != 11, "CPF deve ter 11 números");
            CPF = cpf.Trim();
        }
    }
}