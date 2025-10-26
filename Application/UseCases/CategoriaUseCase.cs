using Application.Configurations;
using Domain;

namespace Application.UseCases
{
    public class CategoriaUseCase : ICategoriaUseCase
    {
        
        public async Task AtualizarCategoria(Categoria categoria)
        {
           // _categoriaRepository.Atualizar(categoria);
        }

        public async Task<Categoria> BuscarCategoriaPorId(int IdCategoria)
        {
            //return _categoriaRepository.BuscarPorId(IdCategoria.ToString());
            return new Categoria();
        }

        public async Task<Categoria> BuscarCategoriaPorNome(string nome)
        {
            //return _categoriaRepository.Buscar(x => x.Nome.Equals(nome)).FirstOrDefault();
            return new Categoria { Nome = nome };
        }

        public async Task<IEnumerable<Categoria>> ListarTodos()
        {
           // return _categoriaRepository.ListarTodos();
           return new List<Categoria>();
        }

        public async Task ExcluirCategoria(string nome)
        {
            /*
            if (!_categoriaRepository.Existe(x => x.Nome == nome))
                throw new BusinessException("Nome da categoria não está cadastrado");

            _categoriaRepository.Excluir(nome);
            */


        }

        public async Task IncluirCategoria(Categoria categoria)
        {
            /*
            if(_categoriaRepository.Existe(x => x.Nome == categoria.Nome))
                throw new BusinessException("Nome da categoria já existe");

            _categoriaRepository.Inserir(categoria);
            */
        }

        public async Task FinalizarCategoria(Categoria categoria)
        {
          //  _categoriaRepository.Atualizar(categoria);
        }

        public async Task CancelarCategoria(Categoria categoria)
        {
            //_categoriaRepository.Atualizar(categoria);
        }
    }
}