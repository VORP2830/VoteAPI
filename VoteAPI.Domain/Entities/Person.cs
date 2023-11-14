using System.Text.RegularExpressions;
using VoteAPI.Domain.Exceptions;

namespace VoteAPI.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; protected set; }
        public string CPF { get; protected set; }
        protected Person() { }
        public Person(string name, string cpf)
        {
            ValidateDomain(name, cpf);
            Active = true;
        }
        private void ValidateDomain(string name, string cpf)
        {
            VoteAPIException.When(string.IsNullOrEmpty(name), "Nome é obrigatório");
            VoteAPIException.When(name.Length < 3, "Nome deve ter no mínimo 3 caracteres");
            cpf = Regex.Replace(cpf, "[^0-9]", "");
            VoteAPIException.When(cpf.Length != 11, "CPF deve ter 11 números");
            Name = name.Trim();
            CPF = cpf.Trim();
        }
    }
}