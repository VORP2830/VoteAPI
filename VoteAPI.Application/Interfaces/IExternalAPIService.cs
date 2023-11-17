namespace VoteAPI.Application.Interfaces
{
    public interface IExternalAPIService
    {
        Task<bool> CheckCPFIsValid(string cpf);   
    }
}