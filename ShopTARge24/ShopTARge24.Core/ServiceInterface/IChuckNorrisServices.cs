using ShopTARge24.Core.Dto.ChuckNorris;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IChuckNorrisServices
    {
        Task<ChuckNorrisRootDto> ChuckNorrisResultHttpClient();
        Task<ChuckNorrisResultDto> ChuckNorrisResult(ChuckNorrisResultDto dto);
    }
}
