using Andreani.ARQ.Core.Interface;
using Application.Services;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.UseCase.Queries.GetAll
{
    public abstract class BaseGetAllReglasQuery : BaseReglaQuery
    {
        internal readonly IReadOnlyQuery _query;

        protected BaseGetAllReglasQuery(IReadOnlyQuery query, IOperacionService operacionService) :base(operacionService)
        {
            _query = query;
        }

        public async Task<List<BaseEntity>> GetAllAsync(int start, int length, ReglasEnum rule)
        {
            return rule switch
            {
                ReglasEnum.ValoresSeguros => FilterEntities(await _query.GetAllAsync<ReglaValoresSeguro>("ReglaValoresSeguros"), start, length),
                ReglasEnum.ValoresUnitarios => FilterEntities(await _query.GetAllAsync<ReglaValoresUnitarios>("ReglaValoresUnitarios"), start, length),
                ReglasEnum.CantidadArticulos => FilterEntities(await _query.GetAllAsync<ReglaCantidadArticulo>("ReglaCantidadArticulos"), start, length),
                ReglasEnum.CantidadBulto => FilterEntities(await _query.GetAllAsync<ReglaCantidadBulto>("ReglaCantidadBulto"), start, length),
                ReglasEnum.PorColaborador => FilterEntities(await _query.GetAllAsync<ReglaPorColaborador>("ReglaPorColaborador"), start, length),
                ReglasEnum.PorContrato => FilterEntities(await _query.GetAllAsync<ReglaPorContrato>("ReglaPorContrato"), start, length),
                ReglasEnum.CantidadPedidos => FilterEntities(await _query.GetAllAsync<ReglaCantidadPedido>("ReglaCantidadPedidos"), start, length),
                ReglasEnum.Destinatario => FilterEntities(await _query.GetAllAsync<ReglaDestinatario>("ReglaDestinatario"), start, length),
                _ => throw new InvalidOperationException($"The type {rule.GetFriendlyName()} is not supported"),
            };
        }

        private static List<BaseEntity> FilterEntities(IEnumerable<BaseEntity> enumerable, int start, int length)
            => enumerable.Skip(start).Take(length).ToList();
    }
}