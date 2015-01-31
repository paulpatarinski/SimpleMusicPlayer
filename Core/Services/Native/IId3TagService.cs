using System.Threading.Tasks;
using Core.Models;

namespace Core.Services.Native
{
  public interface IId3TagService
  {
    Task<Id3Tag> GetId3TagAsync(string musicFilePath);
  }
}
