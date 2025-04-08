using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiAutenticacao.Models.Pedido
{
    // 🔹 Representa o pedido finalizado de um usuário
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        // 🔗 Usuário que fez o pedido
        public string UserId { get; set; }

        // 🕒 Data/hora em que o pedido foi finalizado
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        // 📦 Lista de itens comprados neste pedido
        public List<PedidoItem> Itens { get; set; }
    }
}
