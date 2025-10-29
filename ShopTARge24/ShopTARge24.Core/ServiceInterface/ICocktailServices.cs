using ShopTARge24.Core.Dto.CocktailDtos;


namespace ShopTARge24.Core.ServiceInterface
{
    public interface ICocktailServices
    {
        Task<CocktailRootDto> GetCocktails(CocktailResultDto dto);
    }
}
