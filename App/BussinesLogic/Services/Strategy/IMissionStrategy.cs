using System.Threading.Tasks;

namespace BussinesLogic.Services.Strategy
{
    public interface IMissionStrategy
    {
        Task<bool> CheckComplete(int playerID);
    }
}
