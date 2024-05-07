using System;
using System.Collections.Generic;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}

public class ItemCarrinho
{
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }

    public decimal Subtotal
    {
        get { return Produto.Preco * Quantidade; }
    }
}

public class CarrinhoCompras
{
    private List<ItemCarrinho> itens = new List<ItemCarrinho>();

    public void AdicionarItem(Produto produto, int quantidade)
    {
        var itemExistente = itens.Find(i => i.Produto.Id == produto.Id);
        if (itemExistente != null)
        {
            itemExistente.Quantidade += quantidade;
        }
        else
        {
            itens.Add(new ItemCarrinho { Produto = produto, Quantidade = quantidade });
        }
    }

    public void RemoverItem(Produto produto)
    {
        itens.RemoveAll(i => i.Produto.Id == produto.Id);
    }

    public decimal CalcularTotalPedido()
    {
        return itens.Sum(i => i.Subtotal);
    }

    public void LimparCarrinho()
    {
        itens.Clear();
    }

    public List<ItemCarrinho> GetItensCarrinho()
    {
        return itens;
    }
}

public static class CarrinhoComprasUtil
{
    public static decimal CalcularTotalPedidoComDesconto(CarrinhoCompras carrinho, decimal desconto)
    {
        var total = carrinho.CalcularTotalPedido();
        return total - (total * desconto);
    }

    public static bool ProcessarPagamento(CarrinhoCompras carrinho, string dadosPagamento)
    {
        // Simulação de processamento de pagamento (substitua por sua lógica real)
        Console.WriteLine($"Processando pagamento para o valor total de: {carrinho.CalcularTotalPedido()}");
        Console.WriteLine($"Dados de pagamento: {dadosPagamento}");
        // Simulando sucesso no pagamento
        return true;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Criando produtos
        var produto1 = new Produto { Id = 1, Nome = "Camisa", Preco = 29.90m };
        var produto2 = new Produto { Id = 2, Nome = "Calça", Preco = 49.90m };

        // Criando carrinho de compras
        var carrinho = new CarrinhoCompras();

        // Adicionando itens ao carrinho
        carrinho.AdicionarItem(produto1, 2);
        carrinho.AdicionarItem(produto2, 1);

        // Exibindo itens do carrinho
        Console.WriteLine("Itens do Carrinho:");
        foreach (var item in carrinho.GetItensCarrinho())
        {
            Console.WriteLine($" - {item.Produto.Nome} x {item.Quantidade}: {item.Subtotal}");
        }

        // Calculando o total do pedido
        var totalPedido = carrinho.CalcularTotalPedido();
        Console.WriteLine($"\nTotal do pedido sem desconto: {totalPedido}");

        // Aplicando desconto de 10%
        var desconto = 0.10m;
        var totalComDesconto = CarrinhoComprasUtil.CalcularTotalPedidoComDesconto(carrinho, desconto);
        Console.WriteLine($"Total do pedido com desconto de {desconto * 100}%: {totalComDesconto}");

        // Processando pagamento
        var dadosPagamento = "dadosPagamentoExemplo";
        var pagamentoProcessado = CarrinhoComprasUtil.ProcessarPagamento(carrinho, dadosPagamento);
        if (pagamentoProcessado)
        {
            Console.WriteLine("\nPagamento efetuado com sucesso!");
            // Limpar carrinho após pagamento
            carrinho.LimparCarrinho();
        }
        else
        {
            Console.WriteLine("\nFalha ao processar pagamento. Tente novamente.");
        }
    }
}
