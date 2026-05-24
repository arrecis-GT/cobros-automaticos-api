using CobrosAutomaticosApi.Application.DTOs;
using CobrosAutomaticosApi.Application.Interfaces;
using CobrosAutomaticosApi.Infraestructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Intrinsics.Arm;

namespace CobrosAutomaticosApi.Application.Services
{
    public class ClientService: IClientService
    {

        private readonly ClienteRespository _repository;

        public ClientService(ClienteRespository repository)
        {
            _repository = repository;
        }


        public async Task<bool> FindClientByDpi(string Dpi)
        {
            return await _repository.FindClientByDpi(Dpi);
        }

        public async Task<BaseResponse> CrearClient(CreateClientRequest request)
        {

            var result = await _repository.CrearClient(request.Dpi, request.Nombre, request.Email, request.Telefono);

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
