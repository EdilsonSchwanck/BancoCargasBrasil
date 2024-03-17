using AutoMapper;
using CargasBrasilDB.Domin.DTOs.CadastroAprovadoDTO;
using CargasBrasilDB.Domin.DTOs.DeleteDTO;
using CargasBrasilDB.Domin.DTOs.DriverDTO;
using CargasBrasilDB.Domin.DTOs.LoginDTO;
using CargasBrasilDB.Domin.DTOs.TokenDTOs;
using CargasBrasilDB.Domin.Entities;
using CargasBrasilDB.Domin.Interfaces;
using CargasBrasilDB.Token;
using CargasBrasilDB.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargasBrasilDB.Controllers
{
    [ApiController]
    [Route("api/")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository driverRepository;
        private readonly IMapper _mapper;

        public DriverController(IDriverRepository _driverRepository, IMapper mapper)
        {
            driverRepository = _driverRepository;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("CadastrarMotorista")]
        public JsonResult DriverRegister([FromBody] DriverDTO item)
        {
            var driver = driverRepository.GetAll().FirstOrDefault(x => x.Cpf == item.Cpf);
            if (driver != null)
            {

                return ResponseApi.SucesseResponse("Esse CPF já esta em uso!");

            }
            else
            {
                var service = _mapper.Map<Driver>(item);
                service.Senha = Griptography.Hash(service.Senha);
                driverRepository.Save(service);

                return ResponseApi.SucesseResponse("Cadastro Realizado com sucesso! aguarde aluns minutos que nossa equipe ira validar seus documentos");
            }


        }

        [HttpPost]
        [Route("GenerateToken")]
        public JsonResult GenerateToken([FromBody] TokenDTO item)
        {
            var existingUser = driverRepository.GetAll().FirstOrDefault(x => x.Cpf == item.Cpf);
            if (existingUser == null)
            {
                return ResponseApi.FailedResponse("Falha ao Gerar Token");

            }
            else
            {
                var token = JWTToken.GerarToken(item.Cpf);
                return ResponseApi.SucesseResponseObject(token, "Token gerado com sucesso");
            }
        }



        // [Authorize]
        [HttpPost]
        [Route("Login")]
        public JsonResult Login([FromBody] LoginDTO item)
        {
            var cpfToken = driverRepository.GetAll().FirstOrDefault(x => x.Cpf == item.Cpf && Griptography.Hash(item.Senha) == x.Senha);
            if (cpfToken != null)
            {
                var retorno = _mapper.Map<LoginResponse>(cpfToken);

                // retorno.SessionKey = JWTToken.GerarToken(cpnjToken.Cnpj);

                return ResponseApi.SucesseResponseObject(retorno, "Login realizado com sucesso");


            }
            else
                return ResponseApi.FailedResponse("Não foi possivel realizar o login");
        }

        
        [HttpDelete]
        [Route("ExcluirUsuario")]
        public IActionResult DeleteUsuario(DeleteDriveDTO item) 
        {
            var deleteDriver = driverRepository.GetAll().FirstOrDefault(x => x.Cpf == item.Cpf && Griptography.Hash(item.Senha) == x.Senha);

            if (deleteDriver != null)
            {
                driverRepository.Delete(deleteDriver);
                return ResponseApi.SucesseResponse("Motorista excluido com sucesso");
            }
            else
            {
                return ResponseApi.SucesseResponse("Não foi possivel excluir o Motorista");
            }
            
        }




        //web backOficer

        [HttpPost]
        [Route("AprovarCadastro")]
        public JsonResult UpdadeUser([FromBody] CadastroAprovadoDTO item)
        {
            var existingUser = driverRepository.GetAll().FirstOrDefault(x => x.Cpf == item.cpf);

            if (existingUser == null)
            {
                return ResponseApi.FailedResponse("CNPJ não dacastrado");

            }
            else
            {
                existingUser.CadastroAprovado = item.CadastroAprovado;

                // Chame o método Update do repositório para salvar as alterações
                driverRepository.Update(existingUser);
                return ResponseApi.SucesseResponse("Dados do usuário atualizados");

            }
        }


        [HttpGet]
        [Route("ConsultarFormulario")]
        public JsonResult ConsultarClientes()
        {
            var drivers = driverRepository.GetAll()
                .Where(x => x.CadastroAprovado == false)
                .ToList();

            if (drivers.Any())
            {
                var retorno = _mapper.Map<List<CadastroResponseDTO>>(drivers);
                return ResponseApi.SucesseResponseObject(retorno, "Consulta realizada com sucesso");
            }
            else
            {
                return ResponseApi.FailedResponse("Não foi possível consultar os clientes");
            }
        }


    }
}
