using Domain.Dtos;
using Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Domain.Enums
{
    public enum ReglasEnum
    {
        [Display(Name = "Cantidad de Articulos")]
        CantidadArticulos,

        [Display(Name = "Cantidad de Bultos")]
        CantidadBulto,

        [Display(Name = "Cantidad de Pedidos")]
        CantidadPedidos,

        [Display(Name = "Destinatario")]
        Destinatario,

        [Display(Name = "Por Colaborador")]
        PorColaborador,

        [Display(Name = "Por Contrato")]
        PorContrato,

        [Display(Name = "Valores Seguros")]
        ValoresSeguros,

        [Display(Name = "Valores Unitarios")]
        ValoresUnitarios
    }

    public static class ReglasEnumExtension
    {
        public static string GetFriendlyName<TEnumType>(this TEnumType enumValue) where TEnumType : struct
        {
            return enumValue.GetAttribute<TEnumType, DisplayAttribute>()?.Name ?? enumValue.ToString();
        }

        private static TAttribute GetAttribute<TEnumType, TAttribute>(this TEnumType enumValue) where TEnumType : struct
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<TAttribute>();

        }

        public static Rule[] Get => Enum.GetValues(typeof(ReglasEnum))
                                            .Cast<ReglasEnum>()
                                            .Select(x => new Rule()
                                            {
                                                Value = (int)x,
                                                Friendlyname = x.GetFriendlyName()

                                            })
                                            .ToArray();

        public class Rule
        {
            public int Value { get; set; }

            public string Friendlyname { get; set; }
        }
    }
}
