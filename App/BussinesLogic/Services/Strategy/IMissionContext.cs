using System.Threading.Tasks;

namespace BussinesLogic.Services.Strategy
{
    public interface IMissionContext
    {
        void SetStrategy(IMissionStrategy strategy);
        Task<bool> CheckComplete(int playerID);
    }
}
