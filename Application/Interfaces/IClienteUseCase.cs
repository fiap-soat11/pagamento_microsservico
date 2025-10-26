using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public interface IClienteUseCase
    {
        Task<Cliente> AtualizarCliente(Cliente entity, Cliente novoCliente);
        Task<Cliente> ExcluirCliente(Cliente entity);
        Task<Cliente> IncluirCliente(Cliente novoCliente);
    }
}
