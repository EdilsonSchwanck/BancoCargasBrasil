using AutoMapper;
using CargasBrasilDB.Domin.DTOs.CadastroAprovadoDTO;
using CargasBrasilDB.Domin.DTOs.DriverDTO;
using CargasBrasilDB.Domin.DTOs.LoginDTO;
using CargasBrasilDB.Domin.DTOs.TokenDTOs;
using CargasBrasilDB.Domin.Entities;

namespace CargasBrasilDB.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Driver, DriverDTO>();
            CreateMap<DriverDTO, Driver>();

            CreateMap<TokenDTO, Driver>();

            CreateMap<Driver, LoginResponse>();
            CreateMap<LoginResponse, Driver>();


            CreateMap<Driver, CadastroResponseDTO>();
            CreateMap<CadastroResponseDTO, Driver>();



        }
    }
}
