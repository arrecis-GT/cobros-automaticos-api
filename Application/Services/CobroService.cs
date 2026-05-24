using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Domain.Entities;
using CobrosAutomaticosApi.Infraestructure.Repositories;

namespace CobrosAutomaticosApi.Application.Services
{
    public class CobroService : ICobroService
    {
        private readonly CobroRepository _repository;
        public CobroService(CobroRepository repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponse> CrearCobro(CreateCobroRequest request)
        {
            var result = await _repository.CrearCobro(request.ClienteId, request.Monto, request.Moneda, request.ReferenciaExterna);

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

        public async Task<ListarCobroResponse> ListarCobros(int ClienteId)
        {
            var result = await _repository.ListarCobros(ClienteId);

            if (result.Count == 0)
            {
                return new ListarCobroResponse
                {
                    StatusCode = 404
                };
            }

            return new ListarCobroResponse
            {
                StatusCode = 200,
                Cobros = result
            };
        }
    }
}
