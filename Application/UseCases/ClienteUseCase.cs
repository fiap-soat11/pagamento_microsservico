using Application.Configurations;
using Domain;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ClienteUseCase : IClienteUseCase
    {
        public async Task<Cliente> AtualizarCliente(Cliente entity, Cliente novoCliente)
        {
            if(entity == null || novoCliente == null)
            {
                throw new ArgumentNullException("Cliente ou novo cliente não podem ser nulos.");
            }
            if (entity.Cpf != novoCliente.Cpf)
            {
                throw new ArgumentException("CPF do cliente não pode ser alterado.");
            }
            entity.Nome = novoCliente.Nome;
            entity.Email = novoCliente.Email;
            entity.Ativo = novoCliente.Ativo;
            return entity;
        }

        public async Task<Cliente> ExcluirCliente(Cliente entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Cliente não pode ser nulo.");
            }
            if (string.IsNullOrEmpty(entity.Cpf))
            {
                throw new ArgumentException("CPF do cliente não pode ser nulo ou vazio.");
            }

            entity.Ativo = false;
            return entity;
        }

        public async Task<Cliente> IncluirCliente(Cliente novoCliente)
        {
            if (novoCliente == null)
            {
                throw new ArgumentNullException("Novo cliente não pode ser nulo.");
            }
            if (string.IsNullOrEmpty(novoCliente.Cpf))
            {
                throw new ArgumentException("CPF do cliente não pode ser nulo ou vazio.");
            }
            if (string.IsNullOrEmpty(novoCliente.Nome))
            {
                throw new ArgumentException("Nome do cliente não pode ser nulo ou vazio.");
            }
            if (string.IsNullOrEmpty(novoCliente.Email))
            {
                throw new ArgumentException("Email do cliente não pode ser nulo ou vazio.");
            }
            return novoCliente;
        }
    }
}
