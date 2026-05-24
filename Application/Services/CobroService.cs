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
                return new BaseResponse
                {
                    StatusCode = 409
                };
            }

            return new BaseResponse
            {
                StatusCode = 200
            };

        }

        public async Task<ListarCobroResponse> ListarCobros(int ClienteId, DateOnly? FechaInicio = null, DateOnly? FechaFin = null)
        {
            var result = await _repository.ListarCobros(ClienteId, FechaInicio, FechaFin);

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

        public async Task<BaseResponse> ProcesarCobroIndividual(ProcesarCobroIndividualRequest request)
        {
            var result = await _repository.ProcesarCobroIndividual(request.UsuarioId, request.CobroId);

            if (!result)
            {
                return new BaseResponse
                {
                    StatusCode = 409
                };
            }

            return new BaseResponse
            {
                StatusCode = 200
            };
        }
    }
}
