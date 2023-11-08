﻿using Ipet.Data.Context;
using Ipet.Domain.Intefaces;
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ipet.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context) { }


        public async Task<List<Produto>> GetProdutosByTag(string[] tags, List<Produto> todosProdutos)
        {
            if (tags == null || tags.Length == 0)
            {
                return todosProdutos;
            }

            var produtos = DbSet.Include(p => p.Hashtags).AsQueryable();

            foreach (var tag in tags)
            {
                produtos = produtos.Where(p => p.Hashtags.Any(h => h.Tag == tag));
            }

            return await produtos.Distinct().ToListAsync();
        }
    }
}