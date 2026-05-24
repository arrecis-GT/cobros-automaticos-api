using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Infraestructure.Repositories;

namespace CobrosAutomaticosApi.Application.Services
{
    public class CobroService: ICobroService
    {
        private readonly CobroRepository _repository;
        public CobroService(CobroRepository repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponse> CreateCobro(CreateCobroRequest request)
        {
            var result = await _repository.CreateCobro(request.ClienteId, request.Monto, request.Moneda, request.ReferenciaExterna);

            if (!result)
            {
                new BaseResponse
                {
                    StatusCode = 500
                };
            }

            return new BaseResponse
            {
                StatusCode = 200
            };

        }
    }
}
