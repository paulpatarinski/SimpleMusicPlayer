using System.Threading.Tasks;
using Core.Models;
using Core.Models.EventArgs;

namespace Core.Services.Native
{
  public interface IId3TagService
  {
    Task<Id3Tag> GetId3TagAsync(File file);
  }
}
