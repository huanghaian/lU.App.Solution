using System.Threading.Tasks;

namespace UI.Mobie.BasicCore
{
    public interface IAppSetting
    {
        Task<string> GetAysnc(string key);
        Task SetAsync(string key,string value);
        Task ClearAsync();
    }
}