using APIVenda.Data;

namespace APIVenda.Aplication
{
    public class ControleEstoqueApp
    {
        private readonly ControleEstoqueApp _controleEstoqueApp;
        public ControleEstoqueApp(DataContext context)
        {
            _controleEstoqueApp = new ControleEstoqueApp(context);
        }
    }
}
