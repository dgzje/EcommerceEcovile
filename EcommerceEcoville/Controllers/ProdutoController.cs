using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceEcoville.DAL;
using EcommerceEcoville.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceEcoville.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoDAO _produtoDAO;
        public ProdutoController(ProdutoDAO produtoDAO)
        {
            _produtoDAO = produtoDAO;
        }

        //Métodos dentro de um controller são de chamados
        //de actions
        public IActionResult Index()
        {
            ViewBag.Produtos = _produtoDAO.Listar();
            ViewBag.DataHora = DateTime.Now;
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(string txtNome,
            string txtDescricao, string txtPreco, 
            string txtQuantidade)
        {
            Produto p = new Produto
            {
                Nome = txtNome,
                Descricao = txtDescricao,
                Preco = Convert.ToDouble(txtPreco),
                Quantidade = Convert.ToInt32(txtQuantidade)
            };
            _produtoDAO.Cadastrar(p);
            return RedirectToAction("Index");
        }

        public IActionResult Remover(int? id)
        {
            if (id != null)
            {
                _produtoDAO.Remover(id);
            }
            else
            {
                //Redirecionar para uma página de erro
            }
            return RedirectToAction("Index");
        }
        public IActionResult Alterar(int? id)
        {
            ViewBag.Produto = _produtoDAO.BuscarProdutoPorId(id);
            return View();
        }
        [HttpPost]
        public IActionResult Alterar(string txtNome,
            string txtDescricao, string txtPreco,
            string txtQuantidade, string txtId,
            string hdnId)
        {
            Produto p = _produtoDAO.BuscarProdutoPorId
                (Convert.ToInt32(hdnId));
            p.Nome = txtNome;
            p.Descricao = txtDescricao;
            p.Preco = Convert.ToDouble(txtPreco);
            p.Quantidade = Convert.ToInt32(txtQuantidade);
            _produtoDAO.Alterar(p);
            return RedirectToAction("Index");
        }
    }
}